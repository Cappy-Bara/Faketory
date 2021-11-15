import Sensor from "../../../Components/Devices/SensorComponent/Types";
import { ESensorsActions, TSensorActions } from "./types";

const initialState: Sensor[] = [];

const userSensorReducer = (state = initialState, action: TSensorActions) => {
    switch(action.type){
        case ESensorsActions.SETUSERSENSORSSTATE:
            return action.payload;

        case ESensorsActions.UPDATEUSERSENSORSSTATE:{
            var output = [...state];
            action.payload.forEach(sensorState => {
                var index = state.findIndex(x => x.id === sensorState.id);
                output[index].isSensing = sensorState.isSensing;
            });
            return output;
        }
        
        default:
            return state;
    }
}

export default userSensorReducer;