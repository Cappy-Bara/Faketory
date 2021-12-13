export enum EAnimationActions {
    SETANIMATIONSTATE = "SETANIMATIONSTATE"
}

export interface ISetAnimationStateAction{
    type: EAnimationActions.SETANIMATIONSTATE
}

export type TSetAnimationStateActions = ISetAnimationStateAction;