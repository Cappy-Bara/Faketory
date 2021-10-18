import Conveyor from "../../../Components/Devices/ConveyorComponent/Types";
import Pallet from "../../../Components/Devices/PalletComponent/Types";
import Sensor from "../../../Components/Devices/SensorComponent/Types";

export enum EDeviceToModifyActions {
    SETDEVICETOMODIFYSTATE = "SETDEVICETOMODIFYSTATE"
}

export interface ISetDeviceToModifyAction{
    type: EDeviceToModifyActions.SETDEVICETOMODIFYSTATE
    payload: Conveyor | Pallet | Sensor;
}

export type TDeviceToModifyActions = ISetDeviceToModifyAction;