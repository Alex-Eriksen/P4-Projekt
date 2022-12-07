import { IconResponse } from "../Icon";
import { StaticSpellTypeResponse } from "../SpellType";
import { StaticSchoolCategoryResponse } from "../SchoolCategory";

export interface DirectSpellResponse {
    spellID: number;
    spellName: string;
    spellDescription: string;
    icon: IconResponse;
    spellType: StaticSpellTypeResponse;
    schoolCategory: StaticSchoolCategoryResponse;
    damageAmount: number | null;
    manaCost: number;
    lifeTime: number;
    castTime: number;
}
