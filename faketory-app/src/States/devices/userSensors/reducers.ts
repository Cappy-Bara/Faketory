import Sensor from "../../../Components/Devices/SensorComponent/Types";
import { ESensorsActions, TSensorActions } from "./types";

const initialState: Sensor[] = [];

const userSensorReducer = (state = initialState, action: TSensorActions) => {
    switch(action.type){
        case ESensorsActions.SETUSERSENSORSSTATE:
            return action.payload;
        default:
            return state;
    }
}

export default userSensorReducer;