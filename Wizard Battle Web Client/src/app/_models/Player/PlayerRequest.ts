export interface PlayerRequest {
    accountID: number | null;
    playerName: string;
    iconID: number;
    experiencePoints: number;
    knowledgePoints: number;
    timeCapsules: number;
    matchWins: number;
    matchLosses: number;
    timePlayedMin: number;
    avgDamage: number;
    avgSpellsHit: number;
    spellBookID: number;
}
