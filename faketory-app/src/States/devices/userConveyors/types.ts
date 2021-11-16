import { ConveyorState } from "../../../API/Actions/types";
import Conveyor from "../../../Components/Devices/ConveyorComponent/Types";

export enum EConveyorsActions {
    SETUSERCONVEYORSSTATE = "SETUSERCONVEYORSSTATE",
    UPDATEUSERCONVEYORSSTATE = "UPDATEUSERCONVEYORSSTATE"
}

export interface ISetConveyorsAction{
    type: EConveyorsActions.SETUSERCONVEYORSSTATE
    payload: Conveyor[];
}

export interface IUpdateConveyorsAction{
    type: EConveyorsActions.UPDATEUSERCONVEYORSSTATE
    payload: ConveyorState[];
}

export type TConveyorActions = ISetConveyorsAction | IUpdateConveyorsAction;