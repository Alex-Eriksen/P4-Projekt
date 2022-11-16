import { StaticPlayerResponse } from "../Player";

export interface StaticFriendshipResponse {
    mainPlayerID: number;
    friendPlayer: StaticPlayerResponse;
    created_At: string;
    isPending: boolean;
}
