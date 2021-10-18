import Conveyor from "../../../Components/Devices/ConveyorComponent/Types"
import Pallet from "../../../Components/Devices/PalletComponent/Types"
import Sensor from "../../../Components/Devices/SensorComponent/Types"
import { EDeviceToModifyActions } from "./types"

export const setOpenedDevicesSubtab = (device: Conveyor | Pallet | Sensor) => {
    return {
        type: EDeviceToModifyActions.SETDEVICETOMODIFYSTATE,
        payload: device
    }
}