import { MachineState } from "../../../API/Actions/types";
import Machine from "../../../Components/Devices/MachineComponent/types";

export enum EMachinesActions {
    SETUSERMACHINESSTATE = "SETUSERMACHINESSTATE",
    UPDATEUSERMACHINESSTATE = "UPDATEUSERMACHINESSTATE"
}

export interface ISetMachinesAction{
    type: EMachinesActions.SETUSERMACHINESSTATE
    payload: Machine[];
}

export interface IUpdateMachinesAction{
    type: EMachinesActions.UPDATEUSERMACHINESSTATE
    payload: MachineState[];
}

export type TMachineActions = ISetMachinesAction | IUpdateMachinesAction;