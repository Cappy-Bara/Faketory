import Pallet from "../../Components/PalletComponent/Types";
import { EPalletsActions, TPalletActions } from "./types";

const initialState: Pallet[] = [];

const userPalletReducer = (state = initialState, action: TPalletActions) => {
    switch(action.type){
        case EPalletsActions.SETUSERPALLETSSTATE:
            return action.payload;
        default:
            return state;
    }
}

export default userPalletReducer;