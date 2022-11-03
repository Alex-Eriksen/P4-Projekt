import { Icon } from "../Misc/Icon";

export interface StaticPlayerResponse {
  accountID: number;
  playerID: number;
  playerName: string;
  icon: Icon;
  playerStatus: string;
  experiencePoints: number;
  maxHealth: number;
  maxMana: number;
  knowledgePoints: number;
  timeCapsules: number;
}
