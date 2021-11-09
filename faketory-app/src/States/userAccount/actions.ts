import { EAccountActions} from "./types"

export const setLoggedUser = (user: String) => {
    return {
        type: EAccountActions.SETLOGGEDUSER,
        payload: user
    }
}

export const setLogout = () => {
    return {
        type: EAccountActions.SETLOGOUT,
    }
}