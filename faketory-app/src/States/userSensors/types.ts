import Sensor from "../../Components/SensorComponent/Types";

export enum ESensorsActions {
    SETUSERSENSORSSTATE = "SETUSERSENSORSSTATE"
}

export interface ISetSensorsAction{
    type: ESensorsActions.SETUSERSENSORSSTATE
    payload: Sensor[];
}

export type TSensorActions = ISetSensorsAction;