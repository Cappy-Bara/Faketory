import Pallet from "../../../../Components/Devices/PalletComponent/Types";

export enum EPalletToModifyActions {
    SETPALLETTOMODIFYSTATE = "SETPALLETTOMODIFYSTATE"
}

export interface ISetPalletToModifyAction{
    type: EPalletToModifyActions.SETPALLETTOMODIFYSTATE
    payload: Pallet;
}

export type TPalletToModifyActions = ISetPalletToModifyAction;