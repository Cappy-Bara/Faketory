import { useSelector } from "react-redux";
import { IState } from "../../../States";
import DevicesListComponent from "./DevicesListComponent";
import { DeviceTabState } from "./EDevicesTabState";
import AddConveyorTabComponent from "./Subtabs/Conveyor/AddConveyorTabComponent";
import ModifyConveyorTabComponent from "./Subtabs/Conveyor/ModifyConveyorTabComponent";
import AddPalletTabComponent from "./Subtabs/Pallet/AddPalletTabComponent";
import ModifyPalletTabComponent from "./Subtabs/Pallet/ModifyPalletTabComponent";
import AddSensorTabComponent from "./Subtabs/Sensor/AddSensorTabComponent";
import ModifySensorTabComponent from "./Subtabs/Sensor/ModifySensorTabComponent";


const DevicesTabComponent = () => {

    const showedTab = useSelector<IState, DeviceTabState>(state => state.openedDevicesSubtab);

    switch (showedTab) {
        case DeviceTabState.addConveyor:
            return <AddConveyorTabComponent />

        case DeviceTabState.addSensor:
            return <AddSensorTabComponent />

        case DeviceTabState.addPallet:
            return <AddPalletTabComponent />

        case DeviceTabState.modifyConveyor:
            return <ModifyConveyorTabComponent />

        case DeviceTabState.modifySensor:
            return <ModifySensorTabComponent />

        case DeviceTabState.modifyPallet:
            return <ModifyPalletTabComponent />

        default:
            return <DevicesListComponent />
    }
}

export default DevicesTabComponent;