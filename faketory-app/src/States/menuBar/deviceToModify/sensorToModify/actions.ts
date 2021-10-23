import Sensor from "../../../../Components/Devices/SensorComponent/Types"
import { ESensorToModifyActions } from "./types"

export const setSensorToModify = (pallet: Sensor) => {
    return {
        type: ESensorToModifyActions.SETSENSORTOMODIFYSTATE,
        payload: pallet
    }
}