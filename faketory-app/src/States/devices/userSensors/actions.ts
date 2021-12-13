import { SensorState } from "../../../API/Actions/types"
import Sensor from "../../../Components/Devices/SensorComponent/Types"
import { ESensorsActions } from "./types"

export const setUserSensors = (sensors: Sensor[]) => {
    return {
        type: ESensorsActions.SETUSERSENSORSSTATE,
        payload: sensors
    }
}

export const updateUserSensors = (sensorStates: SensorState[]) => {
    return {
        type: ESensorsActions.UPDATEUSERSENSORSSTATE,
        payload: sensorStates
    }
}