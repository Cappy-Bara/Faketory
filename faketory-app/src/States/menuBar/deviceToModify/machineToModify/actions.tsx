import Machine from "../../../../Components/Devices/MachineComponent/types"
import { EMachineToModifyActions } from "./types"

export const setMachineToModify = (machine: Machine) => {
    return {
        type: EMachineToModifyActions.SETMACHINETOMODIFYSTATE,
        payload: machine
    }
}