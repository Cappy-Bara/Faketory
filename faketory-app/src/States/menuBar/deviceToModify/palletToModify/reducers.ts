import Pallet from "../../../../Components/Devices/PalletComponent/Types";
import { EPalletToModifyActions, TPalletToModifyActions } from "./types";

const initialState: Pallet | null = null;

const palletToModifyReducer = (state = initialState, action: TPalletToModifyActions) => {
    switch(action.type){
        case EPalletToModifyActions.SETPALLETTOMODIFYSTATE:
            return action.payload;
        default:
            return state;
    }
}

export default palletToModifyReducer;