import { StaticPlayerResponse } from "../Player/index";

export interface DirectFriendshipResponse {
    mainPlayer: StaticPlayerResponse;
    friendPlayer: StaticPlayerResponse;
    created_At: string;
    isPending: boolean;
}
