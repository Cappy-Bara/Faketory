import Conveyor from "../../../Components/Devices/ConveyorComponent/Types"
import { EConveyorsActions } from "./types"

export const setUserConveyors = (conveyors: Conveyor[]) => {
    return {
        type: EConveyorsActions.SETUSERCONVEYORSSTATE,
        payload: conveyors
    }
}