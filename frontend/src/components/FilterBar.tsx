import React from 'react';
import { Difficulty, SportType } from '../types/tour';
import styles from './FilterBar.module.css';

interface Props {
  sport: SportType | '';
  difficulty: Difficulty | '';
  onSportChange: (v: SportType | '') => void;
  onDifficultyChange: (v: Difficulty | '') => void;
}

const SPORTS: Array<{ value: SportType | ''; label: string }> = [
  { value: '', label: 'All sports' },
  { value: 'hiking', label: '🥾 Hiking' },
  { value: 'running', label: '🏃 Running' },
  { value: 'cycling', label: '🚴 Cycling' },
  { value: 'mtb', label: '🚵 MTB' },
];

const DIFFICULTIES: Array<{ value: Difficulty | ''; label: string }> = [
  { value: '', label: 'Any level' },
  { value: 'easy', label: 'Easy' },
  { value: 'moderate', label: 'Moderate' },
  { value: 'hard', label: 'Hard' },
  { value: 'expert', label: 'Expert' },
];

export const FilterBar: React.FC<Props> = ({
  sport,
  difficulty,
  onSportChange,
  onDifficultyChange,
}) => (
  <div className={styles.bar}>
    <div className={styles.group}>
      {SPORTS.map(({ value, label }) => (
        <button
          key={value || 'all-sports'}
          className={`${styles.chip} ${sport === value ? styles.active : ''}`}
          onClick={() => onSportChange(value as SportType | '')}
          aria-pressed={sport === value}
        >
          {label}
        </button>
      ))}
    </div>
    <div className={styles.divider} />
    <div className={styles.group}>
      {DIFFICULTIES.map(({ value, label }) => (
        <button
          key={value || 'any'}
          className={`${styles.chip} ${difficulty === value ? styles.active : ''}`}
          onClick={() => onDifficultyChange(value as Difficulty | '')}
          aria-pressed={difficulty === value}
        >
          {label}
        </button>
      ))}
    </div>
  </div>
);
