import Sensor from "../../../../Components/Devices/SensorComponent/Types";
import { ESensorToModifyActions, TSensorToModifyActions } from "./types";

const initialState: Sensor | null = null;

const sensorToModifyReducer = (state = initialState, action: TSensorToModifyActions) => {
    switch(action.type){
        case ESensorToModifyActions.SETSENSORTOMODIFYSTATE:
            return action.payload;
        default:
            return state;
    }
}

export default sensorToModifyReducer;