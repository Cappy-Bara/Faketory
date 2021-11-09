export enum EAccountActions {
    SETLOGGEDUSER = "SETLOGGEDUSER",
    SETLOGOUT = "SETLOGOUT"
}

export interface ISetLoggedUserAction{
    type: EAccountActions.SETLOGGEDUSER;
    payload: String;
}

export interface ISetLogout{
    type: EAccountActions.SETLOGOUT;
}

export type TAccountActions = ISetLoggedUserAction | ISetLogout;