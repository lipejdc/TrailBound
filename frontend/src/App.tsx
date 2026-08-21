import React, { useEffect, useState, useMemo } from 'react';
import { Tour, Difficulty, SportType } from './types/tour';
import { MOCK_TOURS } from './services/tourService';
// When your backend is ready, replace MOCK_TOURS with:
// import { fetchTours } from './services/tourService';
import { TourCard } from './components/TourCard';
import { FilterBar } from './components/FilterBar';
import './App.css';

export default function App() {
  const [tours, setTours] = useState<Tour[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [sport, setSport] = useState<SportType | ''>('');
  const [difficulty, setDifficulty] = useState<Difficulty | ''>('');

  useEffect(() => {
    // --- Using mock data for now ---
    setTours(MOCK_TOURS);
    setLoading(false);

    // --- Swap this in when your .NET API is ready ---
    // setLoading(true);
    // fetchTours({ sport: sport || undefined, difficulty: difficulty || undefined })
    //   .then(setTours)
    //   .catch(e => setError(e.message))
    //   .finally(() => setLoading(false));
  }, []);

  const filtered = useMemo(() => {
    return tours.filter(t => {
      if (sport && t.sport !== sport) return false;
      if (difficulty && t.difficulty !== difficulty) return false;
      return true;
    });
  }, [tours, sport, difficulty]);

  const totalKm = useMemo(
    () => tours.reduce((acc, t) => acc + t.distanceKm, 0),
    [tours]
  );
  const totalElevation = useMemo(
    () => tours.reduce((acc, t) => acc + t.elevationGainM, 0),
    [tours]
  );

  return (
    <div className="app">
      {/* Hero */}
      <header className="hero">
        <div className="hero__inner">
          <p className="hero__eyebrow">My trails</p>
          <h1 className="hero__title">Every path<br />tells a story.</h1>
          <div className="hero__stats">
            <div className="hero__stat">
              <span className="hero__stat-value">{tours.length}</span>
              <span className="hero__stat-label">tours</span>
            </div>
            <div className="hero__stat-div" />
            <div className="hero__stat">
              <span className="hero__stat-value">{totalKm.toFixed(0)}</span>
              <span className="hero__stat-label">km covered</span>
            </div>
            <div className="hero__stat-div" />
            <div className="hero__stat">
              <span className="hero__stat-value">{totalElevation.toLocaleString()}</span>
              <span className="hero__stat-label">m climbed</span>
            </div>
          </div>
        </div>
        <div className="hero__topo" aria-hidden="true">
          {/* Decorative topo rings */}
          <svg viewBox="0 0 800 400" preserveAspectRatio="xMidYMid slice">
            {[0,1,2,3,4,5,6,7].map(i => (
              <ellipse
                key={i}
                cx={620}
                cy={200}
                rx={60 + i * 55}
                ry={30 + i * 28}
                fill="none"
                stroke="white"
                strokeWidth="1"
                strokeOpacity={0.12 - i * 0.01}
              />
            ))}
          </svg>
        </div>
      </header>

      {/* Filters + Grid */}
      <main className="main">
        <div className="filters">
          <FilterBar
            sport={sport}
            difficulty={difficulty}
            onSportChange={setSport}
            onDifficultyChange={setDifficulty}
          />
          <p className="result-count">
            {filtered.length} {filtered.length === 1 ? 'tour' : 'tours'}
          </p>
        </div>

        {loading && (
          <div className="state-msg">Loading your trails…</div>
        )}

        {error && (
          <div className="state-msg state-msg--error">
            Could not load tours: {error}
          </div>
        )}

        {!loading && !error && filtered.length === 0 && (
          <div className="state-msg">No tours match those filters.</div>
        )}

        {!loading && !error && (
          <div className="grid">
            {filtered.map(tour => (
              <TourCard key={tour.id} tour={tour} />
            ))}
          </div>
        )}
      </main>

      <footer className="footer">
        <p>Powered by <a href="https://www.komoot.com" target="_blank" rel="noopener noreferrer">Komoot</a> · Built with ♥</p>
      </footer>
    </div>
  );
}
