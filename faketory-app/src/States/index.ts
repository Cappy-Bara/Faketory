import { combineReducers } from "redux";
import Conveyor from "../Components/ConveyorComponent/Types";
import { Slot } from "../Components/MenuBar/PLCTab/Types";
import Pallet from "../Components/PalletComponent/Types";
import Sensor from "../Components/SensorComponent/Types";
import userConveyorsReducer from "./userConveyors/reducers";
import userPalletReducer from "./userPallets/reducers";
import userSensorReducer from "./userSensors/reducers";
import userSlotsReducer from './userSlots/reducers'

export interface IState{
    userSlots: Slot[]
    userConveyors: Conveyor[]
    userSensors: Sensor[],
    userPallets: Pallet[],
}

export const rootReducer = combineReducers({
    userSlots: userSlotsReducer,
    userConveyors: userConveyorsReducer,
    userPallets: userPalletReducer,
    userSensors: userSensorReducer
});