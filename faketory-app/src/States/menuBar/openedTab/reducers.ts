import { EOpenedTabActions, TOpenedTabActions } from "./types";

const initialState: string = 'home';

const openedTabReducer = (state = initialState, action: TOpenedTabActions) => {
    switch(action.type){
        case EOpenedTabActions.SETOPENEDTABSTATE:
            return action.payload;
        default:
            return state;
    }
}

export default openedTabReducer;