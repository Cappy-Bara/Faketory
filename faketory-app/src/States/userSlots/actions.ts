import { Slot } from "../../Components/MenuBar/PLCTab/Types";
import { ESlotActions } from "./types";


export const setUserSlots = (slots: Slot[]) => {
    return {
        type: ESlotActions.SETUSERSLOTSSTATE,
        payload: slots
    }
}