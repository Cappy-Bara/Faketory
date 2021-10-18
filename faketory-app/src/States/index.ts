import { combineReducers } from "redux";
import Conveyor from "../Components/Devices/ConveyorComponent/Types";
import { Slot } from "../Components/MenuBar/PLCTab/Types";
import Pallet from "../Components/Devices/PalletComponent/Types";
import userConveyorsReducer from "./devices/userConveyors/reducers";
import userSlotsReducer from './userSlots/reducers'
import Sensor from "../Components/Devices/SensorComponent/Types";
import userPalletReducer from "./devices/userPallets/reducers";
import userSensorReducer from "./devices/userSensors/reducers";
import { DeviceTabState } from "../Components/MenuBar/DevicesTab/EDevicesTabState";
import deviceToModifyReducer from "./menuBar/deviceToModify/reducers";
import openedDevicesubtabReducer from "./menuBar/openedDevicesSubtab/reducers";
import openedTabReducer from "./menuBar/openedTab/reducers";

export interface IState{
    userSlots: Slot[],
    userConveyors: Conveyor[],
    userSensors: Sensor[],
    userPallets: Pallet[],
    deviceToModify: Conveyor | Pallet | Sensor,
    openedDevicesSubtab: DeviceTabState,
    openedTab: string,
}

export const rootReducer = combineReducers({
    userSlots: userSlotsReducer,
    userConveyors: userConveyorsReducer,
    userPallets: userPalletReducer,
    userSensors: userSensorReducer,
    deviceToModify : deviceToModifyReducer,
    openedDevicesSubtab : openedDevicesubtabReducer,
    openedTab: openedTabReducer
});