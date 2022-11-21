import { StaticPlayerResponse } from "../Player";
import { StaticSpellResponse } from "../Spell";

export interface DirectSpellBookResponse {
    spellBookID: number;
    spellBookName: string;
    player: StaticPlayerResponse;
    spells: StaticSpellResponse[];
}
