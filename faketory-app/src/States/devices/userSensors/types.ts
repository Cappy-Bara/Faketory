import { SensorState } from "../../../API/Actions/types";
import Sensor from "../../../Components/Devices/SensorComponent/Types";

export enum ESensorsActions {
    SETUSERSENSORSSTATE = "SETUSERSENSORSSTATE",
    UPDATEUSERSENSORSSTATE = "UPDATEUSERSENSORSSTATE"
}

export interface ISetSensorsAction{
    type: ESensorsActions.SETUSERSENSORSSTATE
    payload: Sensor[];
}

export interface IUpdateSensorsAction{
    type: ESensorsActions.UPDATEUSERSENSORSSTATE
    payload: SensorState[];
}

export type TSensorActions = ISetSensorsAction | IUpdateSensorsAction;