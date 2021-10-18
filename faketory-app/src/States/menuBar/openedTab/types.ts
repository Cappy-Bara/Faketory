export enum EOpenedTabActions {
    SETOPENEDTABSTATE = "SETOPENEDTABSTATE"
}

export interface ISetOpenedTabAction{
    type: EOpenedTabActions.SETOPENEDTABSTATE
    payload: string;
}

export type TOpenedTabActions = ISetOpenedTabAction;