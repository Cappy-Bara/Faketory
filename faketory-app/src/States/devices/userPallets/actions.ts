import Pallet from "../../Components/PalletComponent/Types"
import { EPalletsActions } from "./types"

export const setUserPallets = (pallets: Pallet[]) => {
    return {
        type: EPalletsActions.SETUSERPALLETSSTATE,
        payload: pallets
    }
}