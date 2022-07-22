import Machine from "../../../../Components/Devices/MachineComponent/types";


export enum EMachineToModifyActions {
    SETMACHINETOMODIFYSTATE = "SETMACHINETOMODIFYSTATE"
}

export interface ISetMachineToModifyAction{
    type: EMachineToModifyActions.SETMACHINETOMODIFYSTATE
    payload: Machine;
}

export type TMachineToModifyActions = ISetMachineToModifyAction;