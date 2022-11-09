import { StaticAccountResponse } from "../Account";
import { IconResponse } from "../Icon/";

export interface DirectPlayerResponse {
    playerID: number;
    account: StaticAccountResponse;
    playerName: string;
    icon: IconResponse;
    playerStatus: string;
    experiencePoints: number;
    maxHealth: number;
    maxMana: number;
    knowledgePoints: number;
    timeCapsules: number;
    matchWins: number;
    matchLosses: number;
    timePlayedMin: number;
}
