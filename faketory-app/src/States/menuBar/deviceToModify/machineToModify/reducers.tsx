import Machine from "../../../../Components/Devices/MachineComponent/types";
import { EMachineToModifyActions, TMachineToModifyActions } from "./types";

const initialState: Machine | null = null;

const machineToModifyReducer = (state = initialState, action: TMachineToModifyActions) => {
    switch(action.type){
        case EMachineToModifyActions.SETMACHINETOMODIFYSTATE:
            return action.payload;
        default:
            return state;
    }
}

export default machineToModifyReducer;