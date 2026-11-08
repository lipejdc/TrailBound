import { SportType } from './tour';

export interface ActivityFilters {
  search: string;
  sport: SportType | '';
  startDate: string;          // yyyy-MM-dd from <input type="date">
  endDate: string;
  minDurationHours: string;
  maxDurationHours: string;
  minDistanceKm: string;
  maxDistanceKm: string;
}

export const EMPTY_FILTERS: ActivityFilters = {
  search: '',
  sport: '',
  startDate: '',
  endDate: '',
  minDurationHours: '',
  maxDurationHours: '',
  minDistanceKm: '',
  maxDistanceKm: '',
};

const bothSetAndReversed = (min: string, max: string) =>
  min !== '' && max !== '' && Number(min) > Number(max);

export function getFilterErrors(f: ActivityFilters) {
  return {
    dates: !!f.startDate && !!f.endDate && f.startDate > f.endDate,
    duration: bothSetAndReversed(f.minDurationHours, f.maxDurationHours),
    distance: bothSetAndReversed(f.minDistanceKm, f.maxDistanceKm),
  };
}

export const hasFilterErrors = (f: ActivityFilters) =>
  Object.values(getFilterErrors(f)).some(Boolean);