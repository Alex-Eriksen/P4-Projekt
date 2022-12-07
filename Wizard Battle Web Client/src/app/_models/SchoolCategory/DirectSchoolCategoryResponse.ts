import { StaticSpellSchoolResponse } from "../SpellSchool";

export interface DirectSchoolCategoryResponse {
    schoolCategoryID: number;
    schoolCategoryName: string;
    spellSchool: StaticSpellSchoolResponse;
}
