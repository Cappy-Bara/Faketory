import Conveyor from "../../../../Components/Devices/ConveyorComponent/Types";
import { EConveyorToModifyActions, TConveyorToModifyActions } from "./types";

const initialState: Conveyor | null = null;

const conveyorToModifyReducer = (state = initialState, action: TConveyorToModifyActions) => {
    switch(action.type){
        case EConveyorToModifyActions.SETCONVEYORTOMODIFYSTATE:
            return action.payload;
        default:
            return state;
    }
}

export default conveyorToModifyReducer;