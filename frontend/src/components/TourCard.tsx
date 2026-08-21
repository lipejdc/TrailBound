import React from 'react';
import { Tour, Difficulty } from '../types/tour';
import styles from './TourCard.module.css';

interface Props {
  tour: Tour;
}

function formatDuration(minutes: number): string {
  const h = Math.floor(minutes / 60);
  const m = minutes % 60;
  return m > 0 ? `${h}h ${m}m` : `${h}h`;
}

const DIFFICULTY_LABEL: Record<Difficulty, string> = {
  easy: 'Easy',
  moderate: 'Moderate',
  hard: 'Hard',
  expert: 'Expert',
};

const SPORT_ICON: Record<string, string> = {
  hiking: '🥾',
  running: '🏃',
  cycling: '🚴',
  mtb: '🚵',
};

export const TourCard: React.FC<Props> = ({ tour }) => {
  const formattedDate = new Date(tour.date).toLocaleDateString('en-GB', {
    day: 'numeric',
    month: 'short',
    year: 'numeric',
  });

  return (
    <a
      href={tour.komootUrl}
      target="_blank"
      rel="noopener noreferrer"
      className={styles.card}
      aria-label={`Open ${tour.name} on Komoot`}
    >
      <div className={styles.imageWrap}>
        <img
          src={tour.coverImageUrl}
          alt={tour.name}
          className={styles.image}
          loading="lazy"
        />
        {/* Topographic overlay — the signature element */}
        <svg
          className={styles.topoOverlay}
          viewBox="0 0 400 220"
          preserveAspectRatio="xMidYMid slice"
          aria-hidden="true"
        >
          {[30, 60, 90, 120, 150, 180].map((y, i) => (
            <ellipse
              key={i}
              cx={200 + i * 8}
              cy={y + i * 5}
              rx={160 - i * 18}
              ry={28 - i * 2}
              fill="none"
              stroke="white"
              strokeWidth="0.6"
              strokeOpacity="0.35"
            />
          ))}
        </svg>
        <span className={`${styles.badge} ${styles[`badge--${tour.difficulty}`]}`}>
          {DIFFICULTY_LABEL[tour.difficulty]}
        </span>
        <span className={styles.sportBadge}>
          {SPORT_ICON[tour.sport]}
        </span>
      </div>

      <div className={styles.body}>
        <p className={styles.location}>{tour.startLocation} · {formattedDate}</p>
        <h2 className={styles.name}>{tour.name}</h2>
        <p className={styles.description}>{tour.description}</p>

        <div className={styles.stats}>
          <div className={styles.stat}>
            <span className={styles.statValue}>{tour.distanceKm.toFixed(1)}</span>
            <span className={styles.statLabel}>km</span>
          </div>
          <div className={styles.statDivider} />
          <div className={styles.stat}>
            <span className={styles.statValue}>{tour.elevationGainM.toLocaleString()}</span>
            <span className={styles.statLabel}>m ↑</span>
          </div>
          <div className={styles.statDivider} />
          <div className={styles.stat}>
            <span className={styles.statValue}>{formatDuration(tour.durationMinutes)}</span>
            <span className={styles.statLabel}>time</span>
          </div>
        </div>
      </div>
    </a>
  );
};
