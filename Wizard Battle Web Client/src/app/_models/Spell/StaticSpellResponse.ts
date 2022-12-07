import { IconResponse } from "../Icon/"

export interface StaticSpellResponse {
    spellID: number;
    spellName: string;
    spellDescription: string;
    icon: IconResponse;
    spellTypeID: number;
    schoolCategoryID: number;
    damageAmount: number | null;
    manaCost: number;
    lifeTime: number;
    castTime: number;
}
