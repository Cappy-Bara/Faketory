import { useState } from "react";
import DevicesListComponent from "./DevicesListComponent";
import { DeviceTabState } from "./EDevicesTabState";
import AddConveyorTabComponent from "./Subtabs/Conveyor/AddConveyorTabComponent";
import AddPalletTabComponent from "./Subtabs/Pallet/AddPalletTabComponent";
import AddSensorTabComponent from "./Subtabs/Sensor/AddSensorTabComponent";


const DevicesTabComponent = () => {

    const [showedTab, setShowedTab] = useState<DeviceTabState>(DeviceTabState.list);

    switch (showedTab) {
        case DeviceTabState.addConveyor:
            return <AddConveyorTabComponent changeActiveTab={setShowedTab}/>

        case DeviceTabState.addSensor:
            return <AddSensorTabComponent changeActiveTab={setShowedTab}/>

        case DeviceTabState.addPallet:
            return <AddPalletTabComponent changeActiveTab={setShowedTab}/>

        default:
            return <DevicesListComponent changeActiveTab={setShowedTab} />
    }
}

export default DevicesTabComponent;