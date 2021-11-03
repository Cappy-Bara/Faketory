import { EAccountActions, User } from "./types"

export const setLoggedUser = (user: User) => {
    return {
        type: EAccountActions.SETLOGGEDUSER,
        payload: user
    }
}

export const setLogout = () => {
    return {
        type: EAccountActions.SETLOGGEDUSER,
    }
}