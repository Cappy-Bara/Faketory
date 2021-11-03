import { EAccountActions, TAccountActions, User } from "./types";

const initialState: User | null = null;

const loggedUserReducer = (state = initialState, action: TAccountActions) => {
    switch(action.type){
        case EAccountActions.SETLOGGEDUSER:
            return action.payload;
        case EAccountActions.SETLOGOUT:
            return null;
        default:
            return state;
    }
}

export default loggedUserReducer;