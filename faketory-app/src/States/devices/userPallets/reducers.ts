import Pallet from "../../../Components/Devices/PalletComponent/Types";
import { EPalletsActions, TPalletActions } from "./types";

const initialState: Pallet[] = [];

const userPalletReducer = (state = initialState, action: TPalletActions) => {
    switch (action.type) {
        case EPalletsActions.SETUSERPALLETSSTATE:
            return action.payload;

        case EPalletsActions.MODIFYUSERPALLETSSTATE: {
            var output = [...state];

            action.payload.forEach(pallet => {
                 var index = state.findIndex(x => x.id === pallet.id);
                 output[index].posX = pallet.posX;
                 output[index].posY = pallet.posY;
            });
            return output;
        }

        default:
            return state;
    }
}

export default userPalletReducer;