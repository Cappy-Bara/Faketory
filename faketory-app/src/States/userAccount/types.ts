export enum EAccountActions {
    SETLOGGEDUSER = "SETLOGGEDUSER",
    SETLOGOUT = "SETLOGOUT"
}

export interface User{
    token:string,
    email:string,
}

export interface ISetLoggedUserAction{
    type: EAccountActions.SETLOGGEDUSER;
    payload: User;
}

export interface ISetLogout{
    type: EAccountActions.SETLOGOUT;
}

export type TAccountActions = ISetLoggedUserAction | ISetLogout;