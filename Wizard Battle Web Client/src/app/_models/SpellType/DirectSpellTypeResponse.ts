import { StaticSpellResponse } from "../Spell";

export interface DirectSpellTypeResponse {
    spellTypeID: number;
    spellTypeName: string;
    spells: StaticSpellResponse[];
}
