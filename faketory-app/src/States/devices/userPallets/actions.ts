import Pallet from "../../../Components/Devices/PalletComponent/Types"
import { EPalletsActions } from "./types"

export const setUserPallets = (pallets: Pallet[]) => {
    return {
        type: EPalletsActions.SETUSERPALLETSSTATE,
        payload: pallets
    }
}

export const modifyUserPallets = (pallets: Pallet[]) => {
    return {
        type: EPalletsActions.MODIFYUSERPALLETSSTATE,
        payload: pallets
    }
}