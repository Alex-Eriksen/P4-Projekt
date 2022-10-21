import { StaticPlayerResponse } from "../Player/StaticPlayerResponse";

export interface DirectAccountResponse {
    accountID: number;
    email: string;
    player: StaticPlayerResponse;
}
