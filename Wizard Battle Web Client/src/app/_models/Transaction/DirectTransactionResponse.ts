import { StaticPlayerResponse } from "../Player";
import { StaticSkinItemResponse } from "../SkinItem";

export interface DirectTransactionResponse {
    transactionID: number;
    player: StaticPlayerResponse;
    skinItem: StaticSkinItemResponse;
    totalCost: number;
    created_At: string;
}
