import { NumberSymbol } from "@angular/common";
import { StaticAccountResponse } from "../Account/StaticAccountResponse";

export interface DirectPlayerResponse {
    playerID: number;
    account: StaticAccountResponse;
    playerName: string;
    playerImage: string;
    playerStatus: string;
    experiencePoints: number;
    maxHealth: number;
    maxMana: number;
    knowledgePoints: number;
    timeCapsules: number;
    TimePlayed: string;
}
