import { AccountRequest } from "../Account/AccountRequest";
import { PlayerRequest } from "./PlayerRequest";

export interface PlayerAccountRequest {
    account: AccountRequest;
    player: PlayerRequest;
}
