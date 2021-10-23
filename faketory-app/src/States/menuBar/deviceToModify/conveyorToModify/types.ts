import Conveyor from "../../../../Components/Devices/ConveyorComponent/Types";

export enum EConveyorToModifyActions {
    SETCONVEYORTOMODIFYSTATE = "SETCONVEYORTOMODIFYSTATE"
}

export interface ISetConveyorToModifyAction{
    type: EConveyorToModifyActions.SETCONVEYORTOMODIFYSTATE
    payload: Conveyor;
}

export type TConveyorToModifyActions = ISetConveyorToModifyAction;