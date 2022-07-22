import Machine from "../../../Components/Devices/MachineComponent/types";
import { EMachinesActions, TMachineActions } from "./types";

const initialState: Machine[] = [];

const userMachinesReducer = (state = initialState, action: TMachineActions) => {
    switch (action.type) {
        case EMachinesActions.SETUSERMACHINESSTATE:
            return action.payload;

        case EMachinesActions.UPDATEUSERMACHINESSTATE: {
            var output = [...state];
            action.payload.forEach(machine => {
                var index = state.findIndex(x => x.id === machine.id);
                output[index].isProcessing = machine.isProcessing;
            });
            return output;
        }

        default:
            return state;
    }
}

export default userMachinesReducer;