import { StaticPlayerResponse } from "../Player/index";
import { StaticMessageResponse } from "../Message/StaticMessageResponse";

export interface DirectFriendshipResponse {
    mainPlayer: StaticPlayerResponse;
    friendPlayer: StaticPlayerResponse;
}
