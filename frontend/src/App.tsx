import { useCallback, useEffect, useMemo, useState } from 'react';
import { Tour } from './types/tour';
import { TourCard } from './components/TourCard';
import { FilterBar } from './components/FilterBar';
import { ActivityFilters, EMPTY_FILTERS, hasFilterErrors } from './types/filters';
import { useDebounce } from './hooks/useDebounce';
import { fetchActivities } from './services/activityService';
import './App.css';

const PAGE_SIZE = 12;
const NO_TOURS: Tour[] = [];

interface Result {
  key: string;          // the filters this result belongs to
  tours: Tour[];
  totalCount: number;
  page: number;
  error: string | null;
}

export default function App() {
  const [result, setResult] = useState<Result | null>(null);
  const [loadingMore, setLoadingMore] = useState(false);

  const [filters, setFilters] = useState<ActivityFilters>(EMPTY_FILTERS);
  const debouncedFilters = useDebounce(filters, 400);

  const updateFilters = useCallback(
    (patch: Partial<ActivityFilters>) => setFilters(prev => ({ ...prev, ...patch })),
    []
  );
  const resetFilters = useCallback(() => setFilters(EMPTY_FILTERS), []);
  const hasActiveFilters = useMemo(
    () => JSON.stringify(filters) !== JSON.stringify(EMPTY_FILTERS),
    [filters]
  );

  const queryKey = JSON.stringify(debouncedFilters);
  const invalid = hasFilterErrors(debouncedFilters);

  // The result that matches the current filters. While the filters are invalid,
  // keep showing the last result instead of fetching.
  const current = invalid ? result : result?.key === queryKey ? result : null;

  const loading = !invalid && current === null;
  const tours = current?.tours ?? NO_TOURS;
  const totalCount = current?.totalCount ?? 0;
  const error = current?.error ?? null;
  const hasMore = tours.length < totalCount;

  // First page: runs on mount and whenever the (debounced) filters change
  useEffect(() => {
    if (invalid) return;

    const controller = new AbortController();

    fetchActivities(debouncedFilters, 1, PAGE_SIZE, controller.signal)
      .then(r =>
        setResult({
          key: queryKey,
          tours: r.items,
          totalCount: r.totalCount,
          page: 1,
          error: null,
        })
      )
      .catch(e => {
        if (e.name === 'AbortError') return;
        setResult({ key: queryKey, tours: [], totalCount: 0, page: 1, error: e.message });
      });

    return () => controller.abort();
  }, [debouncedFilters, queryKey, invalid]);

  const loadMore = async () => {
    if (!current) return;
    const { key, page } = current;

    setLoadingMore(true);
    try {
      const r = await fetchActivities(debouncedFilters, page + 1, PAGE_SIZE);
      setResult(prev =>
        prev && prev.key === key
          ? {
              ...prev,
              tours: [...prev.tours, ...r.items],
              totalCount: r.totalCount,
              page: page + 1,
              error: null,
            }
          : prev
      );
    } catch (e) {
      const message = e instanceof Error ? e.message : 'Unknown error';
      setResult(prev => (prev && prev.key === key ? { ...prev, error: message } : prev));
    } finally {
      setLoadingMore(false);
    }
  };

  // These only cover the tours loaded so far, not all matching tours
  const totalKm = useMemo(() => tours.reduce((acc, t) => acc + t.distanceKm, 0), [tours]);
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
              <span className="hero__stat-value">{totalCount}</span>
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
          <svg viewBox="0 0 800 400" preserveAspectRatio="xMidYMid slice">
            {[0, 1, 2, 3, 4, 5, 6, 7].map(i => (
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
            filters={filters}
            onChange={updateFilters}
            onReset={resetFilters}
            hasActiveFilters={hasActiveFilters}
          />
          <p className="result-count">
            {totalCount} {totalCount === 1 ? 'tour' : 'tours'}
          </p>
        </div>

        {loading && <div className="state-msg">Loading your trails…</div>}

        {error && (
          <div className="state-msg state-msg--error">Could not load tours: {error}</div>
        )}

        {!loading && !error && tours.length === 0 && (
          <div className="state-msg">No tours match those filters.</div>
        )}

        {!loading && tours.length > 0 && (
          <>
            <div className="grid">
              {tours.map(tour => (
                <TourCard key={tour.id} tour={tour} />
              ))}
            </div>

            {hasMore && (
              <div className="load-more">
                <button type="button" onClick={loadMore} disabled={loadingMore}>
                  {loadingMore ? 'Loading…' : 'Load more'}
                </button>
              </div>
            )}
          </>
        )}
      </main>

      <footer className="footer">
        <p>
          Powered by{' '}
          <a href="https://www.komoot.com" target="_blank" rel="noopener noreferrer">Komoot</a>
          {' '}· Built with ♥
        </p>
      </footer>
    </div>
  );
}