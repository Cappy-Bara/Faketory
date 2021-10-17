import { Slot } from "../../Components/MenuBar/PLCTab/Types";
import { ESlotActions, TUserSlotsActions } from "./types";

const initialState: Slot[] = [];

const userSlotsReducer = (state = initialState, action: TUserSlotsActions) => {
    switch(action.type){
        case ESlotActions.SETUSERSLOTSSTATE:
            return action.payload;
        default:
            return state;
    }
}

export default userSlotsReducer;