import Pallet from "../../Components/Devices/PalletComponent/Types";

export interface palletsResponse{
    pallets:Pallet[],
}

export interface UpdatePallet{
    palletId: string,
    posX: number,
    posY: number,
}