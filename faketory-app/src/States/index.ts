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
import openedDevicesubtabReducer from "./menuBar/openedDevicesSubtab/reducers";
import openedTabReducer from "./menuBar/openedTab/reducers";
import conveyorToModifyReducer from "./menuBar/deviceToModify/conveyorToModify/reducers";
import sensorToModifyReducer from "./menuBar/deviceToModify/sensorToModify/reducers";
import palletToModifyReducer from "./menuBar/deviceToModify/palletToModify/reducers";
import loggedUserReducer from "./userAccount/reducers";
import animationStateReducer from "./animationSource/reducers";
import Machine from "../Components/Devices/MachineComponent/types";
import userMachinesReducer from "./devices/userMachines/reducers";
import machineToModifyReducer from "./menuBar/deviceToModify/machineToModify/reducers";

export interface IState{
    userSlots: Slot[],
    userConveyors: Conveyor[],
    userSensors: Sensor[],
    userPallets: Pallet[],
    userMachines: Machine[],
    conveyorToModify: Conveyor,
    sensorToModify: Sensor,
    palletToModify: Pallet,
    machineToModify: Machine,
    openedDevicesSubtab: DeviceTabState,
    openedTab: string,
    loggedUser: string | null
    animationSource : boolean
}

export const rootReducer = combineReducers({
    userSlots: userSlotsReducer,
    userConveyors: userConveyorsReducer,
    userPallets: userPalletReducer,
    userSensors: userSensorReducer,
    conveyorToModify : conveyorToModifyReducer,
    sensorToModify : sensorToModifyReducer,
    palletToModify : palletToModifyReducer,
    openedDevicesSubtab : openedDevicesubtabReducer,
    openedTab: openedTabReducer,
    loggedUser: loggedUserReducer,
    animationSource : animationStateReducer,
    userMachines: userMachinesReducer,
    machineToModify: machineToModifyReducer
});