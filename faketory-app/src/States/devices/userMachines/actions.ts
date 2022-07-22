import { MachineState } from "../../../API/Actions/types"
import Machine from "../../../Components/Devices/MachineComponent/types"
import { EMachinesActions } from "./types"

export const setUserMachines = (machines: Machine[]) => {
    return {
        type: EMachinesActions.SETUSERMACHINESSTATE,
        payload: machines
    }
}

export const updateUserMachines = (machineStates: MachineState[]) => {
    return {
        type: EMachinesActions.UPDATEUSERMACHINESSTATE,
        payload: machineStates
    }
}