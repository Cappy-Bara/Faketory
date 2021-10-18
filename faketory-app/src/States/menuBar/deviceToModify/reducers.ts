import Conveyor from "../../../Components/Devices/ConveyorComponent/Types";
import Pallet from "../../../Components/Devices/PalletComponent/Types";
import Sensor from "../../../Components/Devices/SensorComponent/Types";
import { EDeviceToModifyActions, TDeviceToModifyActions } from "./types";

const initialState: Conveyor | Pallet | Sensor | undefined = undefined;

const deviceToModifyReducer = (state = initialState, action: TDeviceToModifyActions) => {
    switch(action.type){
        case EDeviceToModifyActions.SETDEVICETOMODIFYSTATE:
            return action.payload;
        default:
            return state;
    }
}

export default deviceToModifyReducer;