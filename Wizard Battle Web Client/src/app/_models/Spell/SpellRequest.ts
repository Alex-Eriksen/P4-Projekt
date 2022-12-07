export interface SpellRequest {
    spellName: string;
    spellDescription: string;
    iconID: number;
    spellTypeID: number;
    schoolCategoryID: number;
    damageAmount: number;
    manaCost: number;
    lifeTime: number;
    castTime: number;
}
