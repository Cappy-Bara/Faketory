import { EOpenedTabActions } from "./types"

export const setOpenedTab = (tabName: string) => {
    return {
        type: EOpenedTabActions.SETOPENEDTABSTATE,
        payload: tabName
    }
}