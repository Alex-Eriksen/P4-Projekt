import { StaticTransactionResponse } from "../Transaction";

export interface DirectSkinItemResponse {
    skinID: number;
    skinName: string;
    skinDescription: string;
    skinPrice: number;
    imageName: string;
    transactions: StaticTransactionResponse[];
}
