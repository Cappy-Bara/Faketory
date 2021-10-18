import Conveyor from "../../Components/ConveyorComponent/Types"
import { EConveyorsActions } from "./types"

export const setUserConveyors = (conveyors: Conveyor[]) => {
    return {
        type: EConveyorsActions.SETUSERCONVEYORSSTATE,
        payload: conveyors
    }
}