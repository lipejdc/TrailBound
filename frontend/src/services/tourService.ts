import { Tour, Difficulty, SportType } from '../types/tour';

// Point this at your .NET backend
const API_BASE = import.meta.env.VITE_API_URL ?? 'http://localhost:5000';

export interface TourFilters {
  sport?: SportType;
  difficulty?: Difficulty;
}

export async function fetchTours(filters?: TourFilters): Promise<Tour[]> {
  const params = new URLSearchParams();
  if (filters?.sport) params.set('sport', filters.sport);
  if (filters?.difficulty) params.set('difficulty', filters.difficulty);

  const query = params.toString() ? `?${params}` : '';
  const res = await fetch(`${API_BASE}/api/tours${query}`);

  if (!res.ok) throw new Error(`Failed to fetch tours: ${res.statusText}`);
  return res.json();
}

export async function fetchTour(id: string): Promise<Tour> {
  const res = await fetch(`${API_BASE}/api/tours/${id}`);
  if (!res.ok) throw new Error(`Tour not found: ${res.statusText}`);
  return res.json();
}

// --- Mock data for development (remove when backend is ready) ---
export const MOCK_TOURS: Tour[] = [
  {
    id: '1',
    name: 'Schwarzwald Höhenweg',
    sport: 'hiking',
    difficulty: 'moderate',
    distanceKm: 18.4,
    elevationGainM: 720,
    durationMinutes: 285,
    coverImageUrl: 'https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=800&q=80',
    startLocation: 'Freudenstadt',
    description: 'A classic ridge traverse through Black Forest pines with sweeping views over the Rhine plain.',
    komootUrl: 'https://www.komoot.com/tour/1',
    date: '2024-09-14',
  },
  {
    id: '2',
    name: 'Albsteig Etappe 3',
    sport: 'hiking',
    difficulty: 'easy',
    distanceKm: 12.1,
    elevationGainM: 310,
    durationMinutes: 195,
    coverImageUrl: 'https://images.unsplash.com/photo-1464822759023-fed622ff2c3b?w=800&q=80',
    startLocation: 'Bad Urach',
    description: 'Meadows, limestone bluffs and the waterfall at the Uracher Wasserfall. Perfect for a half-day.',
    komootUrl: 'https://www.komoot.com/tour/2',
    date: '2024-08-03',
  },
  {
    id: '3',
    name: 'Zugspitze Gipfeltour',
    sport: 'hiking',
    difficulty: 'expert',
    distanceKm: 22.8,
    elevationGainM: 2190,
    durationMinutes: 540,
    coverImageUrl: 'https://images.unsplash.com/photo-1551632811-561732d1e306?w=800&q=80',
    startLocation: 'Grainau',
    description: 'Germany\'s highest summit via the Reintal valley — long, demanding, unforgettable.',
    komootUrl: 'https://www.komoot.com/tour/3',
    date: '2024-07-20',
  },
  {
    id: '4',
    name: 'Kaiserstuhl Panoramaweg',
    sport: 'hiking',
    difficulty: 'easy',
    distanceKm: 9.6,
    elevationGainM: 210,
    durationMinutes: 150,
    coverImageUrl: 'https://images.unsplash.com/photo-1469474968028-56623f02e42e?w=800&q=80',
    startLocation: 'Breisach',
    description: 'Vineyard paths above the Rhine with views to the Vosges and Black Forest on a clear day.',
    komootUrl: 'https://www.komoot.com/tour/4',
    date: '2024-06-08',
  },
  {
    id: '5',
    name: 'Wutachschlucht Rundweg',
    sport: 'hiking',
    difficulty: 'moderate',
    distanceKm: 15.7,
    elevationGainM: 480,
    durationMinutes: 245,
    coverImageUrl: 'https://images.unsplash.com/photo-1448375240586-882707db888b?w=800&q=80',
    startLocation: 'Bonndorf',
    description: 'Through the wild gorge of the Wutach — mossy rocks, river crossings, primeval atmosphere.',
    komootUrl: 'https://www.komoot.com/tour/5',
    date: '2024-05-18',
  },
  {
    id: '6',
    name: 'Feldberg Gipfelrunde',
    sport: 'running',
    difficulty: 'hard',
    distanceKm: 13.2,
    elevationGainM: 860,
    durationMinutes: 130,
    coverImageUrl: 'https://images.unsplash.com/photo-1513836279014-a89f7d76ef86?w=800&q=80',
    startLocation: 'Feldberg',
    description: 'Trail run around the highest peak in the Black Forest — technical, fast, rewarding.',
    komootUrl: 'https://www.komoot.com/tour/6',
    date: '2024-04-27',
  },
];
