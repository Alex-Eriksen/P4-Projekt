import { IconResponse } from "../Icon/index";

export interface DirectSpellResponse {
    spellID: number;
    spellName: string;
    spellDescription: string;
    icon: IconResponse;
    manaCost: number;
    damageAmount: number;
    castTime: number;
}
