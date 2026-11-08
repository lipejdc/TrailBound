import { ActivityFilters } from '../types/filters';
import { SportType, Tour } from '../types/tour';

// Empty = same origin, so the Vite proxy forwards /api to the .NET API.
const API_URL: string = import.meta.env.VITE_API_URL ?? '';

// ---------- API shapes (adjust to your real JSON) ----------

interface ActivityDto {
  id: number;
  title: string;
  description?: string | null;
  activityType?: string | null;
  date: string;
  duration: string;               // .NET TimeSpan, e.g. "02:30:00" or "1.02:00:00"
  distanceInMeters: number;
  elevationGain?: number | null;
  startLocation?: string | null;
  komootUrl?: string | null;
  coverImageUrl?: string | null;
}

export interface PaginatedResult<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

// ---------- Query building ----------

const pad = (n: number) => String(n).padStart(2, '0');

// hours (1.5) -> TimeSpan string "[d.]hh:mm:ss"
function toTimeSpan(hours: number): string {
  const totalMinutes = Math.round(hours * 60);
  const days = Math.floor(totalMinutes / 1440);
  const h = Math.floor((totalMinutes % 1440) / 60);
  const m = totalMinutes % 60;
  return `${days > 0 ? `${days}.` : ''}${pad(h)}:${pad(m)}:00`;
}

export function buildActivityQuery(
  f: ActivityFilters,
  pageNumber: number,
  pageSize: number
): URLSearchParams {
  const p = new URLSearchParams();
  p.set('pageNumber', String(pageNumber));
  p.set('pageSize', String(pageSize));

  if (f.search.trim()) p.set('searchFilter', f.search.trim());
  if (f.sport) p.set('activityTypeFilter', f.sport);

  // UTC on purpose: Npgsql only accepts DateTimeOffset with offset 0
  if (f.startDate) p.set('startDateFilter', `${f.startDate}T00:00:00Z`);
  if (f.endDate) p.set('endDateFilter', `${f.endDate}T23:59:59Z`);

  if (f.minDurationHours !== '') p.set('minDurationFilter', toTimeSpan(Number(f.minDurationHours)));
  if (f.maxDurationHours !== '') p.set('maxDurationFilter', toTimeSpan(Number(f.maxDurationHours)));

  if (f.minDistanceKm !== '')
    p.set('minDistanceFilter', String(Math.round(Number(f.minDistanceKm) * 1000)));
  if (f.maxDistanceKm !== '')
    p.set('maxDistanceFilter', String(Math.round(Number(f.maxDistanceKm) * 1000)));

  return p;
}

// ---------- Mapping Activity -> Tour ----------

// "[-][d.]hh:mm:ss[.fffffff]" -> minutes
function timeSpanToMinutes(value?: string | null): number {
  if (!value) return 0;
  const m = /^(-)?(?:(\d+)\.)?(\d+):(\d+):(\d+)(?:\.\d+)?$/.exec(value);
  if (!m) return 0;
  const [, neg, d = '0', h, min, s] = m;
  const minutes = Number(d) * 1440 + Number(h) * 60 + Number(min) + Number(s) / 60;
  return Math.round(neg ? -minutes : minutes);
}

// Komoot sport name -> UI sport
function toSportType(activityType?: string | null): SportType {
  const t = (activityType ?? '').toLowerCase();
  if (t.includes('mtb')) return 'mtb';
  if (t.includes('jog') || t.includes('run')) return 'running';
  if (t.includes('bike') || t.includes('bicycle')) return 'cycling';
  return 'hiking';
}

function toTour(a: ActivityDto): Tour {
  return {
    id: String(a.id),
    name: a.title,
    description: a.description ?? '',
    sport: toSportType(a.activityType),
    difficulty: 'moderate',
    date: new Date(a.date),
    distanceKm: a.distanceInMeters / 1000,
    elevationGainM: a.elevationGain ?? 0,
    durationMinutes: timeSpanToMinutes(a.duration),
    startLocation: a.startLocation ?? '',
    komootUrl: a.komootUrl ?? 'https://www.komoot.com',
    coverImageUrl: a.coverImageUrl ?? '',
  };
}

// ---------- Fetch ----------

export async function fetchActivities(
  filters: ActivityFilters,
  pageNumber: number,
  pageSize: number,
  signal?: AbortSignal
): Promise<PaginatedResult<Tour>> {
  const body = {
    pageNumber,
    pageSize,
    title: filters.search.trim() || null,
    activityType: filters.sport || null,
    startDate: filters.startDate ? `${filters.startDate}T00:00:00Z` : null,
    endDate: filters.endDate ? `${filters.endDate}T23:59:59Z` : null,
    minimalDuration:
      filters.minDurationHours !== '' ? toTimeSpan(Number(filters.minDurationHours)) : null,
    maximalDuration:
      filters.maxDurationHours !== '' ? toTimeSpan(Number(filters.maxDurationHours)) : null,
    minimalDistanceInMeters:
      filters.minDistanceKm !== '' ? Math.round(Number(filters.minDistanceKm) * 1000) : null,
    maximalDistanceInMeters:
      filters.maxDistanceKm !== '' ? Math.round(Number(filters.maxDistanceKm) * 1000) : null,
  };

  const res = await fetch(`${API_URL}/api/activity/activities`, {   // <- your real route
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(body),
    signal,
  });
  if (!res.ok) throw new Error(`Request failed (${res.status})`);

  const data: PaginatedResult<ActivityDto> = await res.json();
  return { ...data, items: data.items.map(toTour) };
}