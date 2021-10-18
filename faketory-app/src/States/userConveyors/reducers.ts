import Conveyor from "../../Components/ConveyorComponent/Types";
import { EConveyorsActions, TConveyorActions } from "./types";

const initialState: Conveyor[] = [];

const userConveyorsReducer = (state = initialState, action: TConveyorActions) => {
    switch(action.type){
        case EConveyorsActions.SETUSERCONVEYORSSTATE:
            return action.payload;
        default:
            return state;
    }
}

export default userConveyorsReducer;