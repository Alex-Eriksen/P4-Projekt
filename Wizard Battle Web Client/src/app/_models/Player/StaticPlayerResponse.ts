import { IconResponse } from "../Icon";

export interface StaticPlayerResponse {
  playerID: number;
  accountID: number;
  playerName: string;
  icon: IconResponse;
  playerStatus: string;
  experiencePoints: number;
  matchWins: number;
  matchLosses: number;
  timePlayedMin: number;
  maxHealth: number;
  maxMana: number;
  knowledgePoints: number;
  timeCapsules: number;
}
