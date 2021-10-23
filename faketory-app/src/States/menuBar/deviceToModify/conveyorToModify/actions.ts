import Conveyor from "../../../../Components/Devices/ConveyorComponent/Types"
import { EConveyorToModifyActions } from "./types"

export const setConveyorToModify = (conveyor: Conveyor) => {
    return {
        type: EConveyorToModifyActions.SETCONVEYORTOMODIFYSTATE,
        payload: conveyor
    }
}