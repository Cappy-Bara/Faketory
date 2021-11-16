import Conveyor from "../../../Components/Devices/ConveyorComponent/Types";
import { EConveyorsActions, TConveyorActions } from "./types";

const initialState: Conveyor[] = [];

const userConveyorsReducer = (state = initialState, action: TConveyorActions) => {
    switch (action.type) {
        case EConveyorsActions.SETUSERCONVEYORSSTATE:
            return action.payload;

        case EConveyorsActions.UPDATEUSERCONVEYORSSTATE: {
            var output = [...state];
            action.payload.forEach(conveyor => {
                var index = state.findIndex(x => x.id === conveyor.id);
                output[index].isRunning = conveyor.isRunning;
            });
            return output;
        }


        default:
            return state;
    }
}

export default userConveyorsReducer;