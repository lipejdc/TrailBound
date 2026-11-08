export type Difficulty = 'easy' | 'moderate' | 'hard' | 'expert';
export type SportType = 'hiking' | 'running' | 'cycling' | 'mtb';

export interface Tour {
  id: string;
  name: string;
  sport: SportType;
  difficulty: Difficulty;
  distanceKm: number;
  elevationGainM: number;
  durationMinutes: number;
  coverImageUrl: string;
  startLocation: string;
  description: string;
  komootUrl: string;
  date: Date; // ISO date string
}
