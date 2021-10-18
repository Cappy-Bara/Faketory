import Conveyor from "../../Components/ConveyorComponent/Types";

export enum EConveyorsActions {
    SETUSERCONVEYORSSTATE = "SETUSERCONVEYORSSTATE"
}

export interface ISetConveyorsAction{
    type: EConveyorsActions.SETUSERCONVEYORSSTATE
    payload: Conveyor[];
}

export type TConveyorActions = ISetConveyorsAction;