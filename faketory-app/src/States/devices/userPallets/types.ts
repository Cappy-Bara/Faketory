import Pallet from "../../../Components/Devices/PalletComponent/Types";

export enum EPalletsActions {
    SETUSERPALLETSSTATE = "SETUSERPALLETSSTATE"
}

export interface ISetPalletsAction{
    type: EPalletsActions.SETUSERPALLETSSTATE
    payload: Pallet[];
}

export type TPalletActions = ISetPalletsAction;