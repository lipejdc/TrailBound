import React from 'react';
import { SportType } from '../types/tour';
import { ActivityFilters, getFilterErrors } from '../types/filters';
import styles from './FilterBar.module.css';

interface Props {
  filters: ActivityFilters;
  onChange: (patch: Partial<ActivityFilters>) => void;
  onReset: () => void;
  hasActiveFilters: boolean;
}

const SPORTS: Array<{ value: SportType | ''; label: string }> = [
  { value: '', label: 'All sports' },
  { value: 'hiking', label: '🥾 Hiking' },
  { value: 'running', label: '🏃 Running' },
  { value: 'cycling', label: '🚴 Cycling' },
  { value: 'mtb', label: '🚵 MTB' },
];

export const FilterBar: React.FC<Props> = ({ filters, onChange, onReset, hasActiveFilters }) => {
  const errors = getFilterErrors(filters);

  return (
    <div className={styles.bar}>
      <input
        type="search"
        className={styles.search}
        placeholder="Search by title…"
        value={filters.search}
        onChange={e => onChange({ search: e.target.value })}
        aria-label="Search tours by title"
      />

      <div className={styles.row}>
        <div className={styles.group}>
          {SPORTS.map(({ value, label }) => (
            <button
              key={value || 'all-sports'}
              type="button"
              className={`${styles.chip} ${filters.sport === value ? styles.active : ''}`}
              onClick={() => onChange({ sport: value })}
              aria-pressed={filters.sport === value}
            >
              {label}
            </button>
          ))}
        </div>
      </div>

      <div className={styles.ranges}>
        <fieldset className={`${styles.field} ${errors.dates ? styles.invalid : ''}`}>
          <legend>Date</legend>
          <input
            type="date"
            value={filters.startDate}
            max={filters.endDate || undefined}
            onChange={e => onChange({ startDate: e.target.value })}
            aria-label="From date"
          />
          <span>–</span>
          <input
            type="date"
            value={filters.endDate}
            min={filters.startDate || undefined}
            onChange={e => onChange({ endDate: e.target.value })}
            aria-label="To date"
          />
        </fieldset>

        <fieldset className={`${styles.field} ${errors.duration ? styles.invalid : ''}`}>
          <legend>Duration (h)</legend>
          <input
            type="number"
            min={0}
            step={0.5}
            placeholder="Min"
            value={filters.minDurationHours}
            onChange={e => onChange({ minDurationHours: e.target.value })}
            aria-label="Minimum duration in hours"
          />
          <span>–</span>
          <input
            type="number"
            min={0}
            step={0.5}
            placeholder="Max"
            value={filters.maxDurationHours}
            onChange={e => onChange({ maxDurationHours: e.target.value })}
            aria-label="Maximum duration in hours"
          />
        </fieldset>

        <fieldset className={`${styles.field} ${errors.distance ? styles.invalid : ''}`}>
          <legend>Distance (km)</legend>
          <input
            type="number"
            min={0}
            step={1}
            placeholder="Min"
            value={filters.minDistanceKm}
            onChange={e => onChange({ minDistanceKm: e.target.value })}
            aria-label="Minimum distance in km"
          />
          <span>–</span>
          <input
            type="number"
            min={0}
            step={1}
            placeholder="Max"
            value={filters.maxDistanceKm}
            onChange={e => onChange({ maxDistanceKm: e.target.value })}
            aria-label="Maximum distance in km"
          />
        </fieldset>

        {hasActiveFilters && (
          <button type="button" className={styles.reset} onClick={onReset}>
            Clear filters
          </button>
        )}
      </div>
    </div>
  );
};