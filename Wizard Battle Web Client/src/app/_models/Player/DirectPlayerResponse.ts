import { NumberSymbol } from "@angular/common";
import { StaticAccountResponse } from "../Account/StaticAccountResponse";
import { Icon } from "../Misc/Icon";

export interface DirectPlayerResponse {
    playerID: number;
    account: StaticAccountResponse;
    playerName: string;
    icon: Icon;
    playerStatus: string;
    experiencePoints: number;
    maxHealth: number;
    maxMana: number;
    knowledgePoints: number;
    timeCapsules: number;
    TimePlayed: string;
}
