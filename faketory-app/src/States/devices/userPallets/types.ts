import Pallet from "../../../Components/Devices/PalletComponent/Types";

export enum EPalletsActions {
    SETUSERPALLETSSTATE = "SETUSERPALLETSSTATE",
    MODIFYUSERPALLETSSTATE = "MODIFYUSERPALLETSSTATE"
}

export interface ISetPalletsAction{
    type: EPalletsActions.SETUSERPALLETSSTATE
    payload: Pallet[];
}

export interface IModifyPalletsAction{
    type: EPalletsActions.MODIFYUSERPALLETSSTATE
    payload: Pallet[];
}

export type TPalletActions = ISetPalletsAction | IModifyPalletsAction;