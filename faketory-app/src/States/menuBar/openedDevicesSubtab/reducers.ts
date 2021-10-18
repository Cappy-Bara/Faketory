import { DeviceTabState } from "../../../Components/MenuBar/DevicesTab/EDevicesTabState";
import { EOpenedSubtabActions, TOpenedDevicesSubtabActions } from "./types";

const initialState: DeviceTabState = DeviceTabState.list;

const openedDevicesubtabReducer = (state = initialState, action: TOpenedDevicesSubtabActions) => {
    switch(action.type){
        case EOpenedSubtabActions.SETOPENEDDEVICESSUBTABSTATE:
            return action.payload;
        default:
            return state;
    }
}

export default openedDevicesubtabReducer;