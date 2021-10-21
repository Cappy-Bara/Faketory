import { useSelector } from "react-redux";
import { IState } from "../../../States";
import DevicesListComponent from "./DevicesListComponent";
import { DeviceTabState } from "./EDevicesTabState";
import AddConveyorTabComponent from "./Subtabs/Conveyor/AddConveyorTabComponent";
import AddPalletTabComponent from "./Subtabs/Pallet/AddPalletTabComponent";
import AddSensorTabComponent from "./Subtabs/Sensor/AddSensorTabComponent";


const DevicesTabComponent = () => {

    const showedTab = useSelector<IState, DeviceTabState>(state => state.openedDevicesSubtab);

    switch (showedTab) {
        case DeviceTabState.addConveyor:
            return <AddConveyorTabComponent />

        case DeviceTabState.addSensor:
            return <AddSensorTabComponent />

        case DeviceTabState.addPallet:
            return <AddPalletTabComponent />

        default:
            return <DevicesListComponent />
    }
}

export default DevicesTabComponent;