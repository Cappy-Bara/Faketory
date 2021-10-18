import { DeviceTabState } from "../../../Components/MenuBar/DevicesTab/EDevicesTabState"
import { EOpenedSubtabActions } from "./types"

export const setOpenedDevicesSubtab = (subtab: DeviceTabState) => {
    return {
        type: EOpenedSubtabActions.SETOPENEDDEVICESSUBTABSTATE,
        payload: subtab
    }
}