import { EAccountActions, TAccountActions} from "./types";

const initialState: String | null = null;

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