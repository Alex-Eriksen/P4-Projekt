import { DirectPlayerResponse } from "../Player";
import { StaticSkinItemResponse } from "../SkinItem";

export interface PlayerSkin {
	player: DirectPlayerResponse,
	skinItem: StaticSkinItemResponse,
}
