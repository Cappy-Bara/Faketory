import Pallet from "../../../../Components/Devices/PalletComponent/Types"
import { EPalletToModifyActions } from "./types"

export const setPalletToModify = (pallet: Pallet) => {
    return {
        type: EPalletToModifyActions.SETPALLETTOMODIFYSTATE,
        payload: pallet
    }
}