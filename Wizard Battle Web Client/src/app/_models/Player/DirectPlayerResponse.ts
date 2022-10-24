import { StaticAccountResponse } from "../Account/StaticAccountResponse";

export interface DirectPlayerResponse {
    playerID: number;
    account: StaticAccountResponse;
    playerName: string;
    experiencePoints: number;
    maxHealth: number;
    maxMana: number;
    knowledgePoints: number;
    timeCapsules: number;
}