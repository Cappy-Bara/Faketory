import { EAnimationActions, TSetAnimationStateActions } from "./types";

const initialState: Boolean = false;

const animationStateReducer = (state = initialState, action: TSetAnimationStateActions) => {
    switch(action.type){
        case EAnimationActions.SETANIMATIONSTATE:
            return !state;
        default:
            return state;
    }
}

export default animationStateReducer;