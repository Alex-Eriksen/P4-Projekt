import { StaticPlayerResponse } from "../Player";

export interface StaticFriendshipResponse {
  mainPlayerID: number;
  friendPlayer: StaticPlayerResponse;
}
