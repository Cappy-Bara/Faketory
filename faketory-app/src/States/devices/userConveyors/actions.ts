import { ConveyorState } from "../../../API/Actions/types"
import Conveyor from "../../../Components/Devices/ConveyorComponent/Types"
import { EConveyorsActions } from "./types"

export const setUserConveyors = (conveyors: Conveyor[]) => {
    return {
        type: EConveyorsActions.SETUSERCONVEYORSSTATE,
        payload: conveyors
    }
}

export const updateUserConveyors = (conveyorStates: ConveyorState[]) => {
    return {
        type: EConveyorsActions.UPDATEUSERCONVEYORSSTATE,
        payload: conveyorStates
    }
}