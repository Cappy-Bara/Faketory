import { Slot } from "../../Components/MenuBar/PLCTab/Types";

export enum ESlotActions {
    SETUSERSLOTSSTATE = "SETUSERSLOTSSTATE"
}

export interface ISetUserSlotsAction{
    type: ESlotActions.SETUSERSLOTSSTATE
    payload: Slot[];
}

export type TUserSlotsActions = ISetUserSlotsAction;