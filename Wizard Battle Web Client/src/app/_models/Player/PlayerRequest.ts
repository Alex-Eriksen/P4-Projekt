export interface PlayerRequest {
  accountID?: number;
  playerName: string;
  iconID: number;
  experiencePoints: number;
  knowledgePoints: number;
  timeCapsules: number;
  matchWins: number;
  matchLosses: number;
  timePlayedMin: number;
}
