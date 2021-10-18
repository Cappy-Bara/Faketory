import Sensor from "../../Components/SensorComponent/Types"
import { ESensorsActions } from "./types"

export const setUserSensors = (sensors: Sensor[]) => {
    return {
        type: ESensorsActions.SETUSERSENSORSSTATE,
        payload: sensors
    }
}