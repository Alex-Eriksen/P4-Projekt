import { StaticAccountResponse } from "../Account";
import { IconResponse } from "../Icon/";
import { StaticTransactionResponse } from "../Transaction";

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
    avgDamage: number;
    avgSpellsHit: number;
    spellBookID: number;
    transactions: StaticTransactionResponse[];
    modified_At: string;
}
