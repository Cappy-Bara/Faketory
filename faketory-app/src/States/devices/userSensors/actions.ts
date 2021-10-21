import Sensor from "../../../Components/Devices/SensorComponent/Types"
import { ESensorsActions } from "./types"

export const setUserSensors = (sensors: Sensor[]) => {
    return {
        type: ESensorsActions.SETUSERSENSORSSTATE,
        payload: sensors
    }
}