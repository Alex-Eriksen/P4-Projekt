import { StaticSchoolCategoryResponse } from "../SchoolCategory/index";

export interface DirectSpellSchoolResponse {
    spellSchoolID: number;
    spellSchoolName: string;
    schoolCategories: StaticSchoolCategoryResponse[];
}
