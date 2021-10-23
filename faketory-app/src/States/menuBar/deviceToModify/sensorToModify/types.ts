import Sensor from "../../../../Components/Devices/SensorComponent/Types";

export enum ESensorToModifyActions {
    SETSENSORTOMODIFYSTATE = "SETSENSORTOMODIFYSTATE"
}

export interface ISetSensorToModifyAction{
    type: ESensorToModifyActions.SETSENSORTOMODIFYSTATE
    payload: Sensor;
}

export type TSensorToModifyActions = ISetSensorToModifyAction;