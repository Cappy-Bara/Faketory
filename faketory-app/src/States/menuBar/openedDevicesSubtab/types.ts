import { DeviceTabState } from "../../../Components/MenuBar/DevicesTab/EDevicesTabState";

export enum EOpenedSubtabActions {
    SETOPENEDDEVICESSUBTABSTATE = "SETOPENEDDEVICESSUBTABSTATE"
}

export interface ISetOpenedDevicesSubtabAction{
    type: EOpenedSubtabActions.SETOPENEDDEVICESSUBTABSTATE
    payload: DeviceTabState;
}

export type TOpenedDevicesSubtabActions = ISetOpenedDevicesSubtabAction;