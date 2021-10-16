import { useState } from "react";
import { Table, Button } from "react-bootstrap";
import DevicesListComponent from "./DevicesListComponent";
import { DeviceTabState } from "./EDevicesTabState";
import AddConveyorTabComponent from "./Subtabs/Conveyor/AddConveyorTabComponent";


const DevicesTabComponent = () => {

    const [showedTab, setShowedTab] = useState<DeviceTabState>(DeviceTabState.list);

    switch (showedTab) {
        case DeviceTabState.addConveyor:
            return <AddConveyorTabComponent changeActiveTab={setShowedTab}/>

        case DeviceTabState.addSensor:
            return <p>add sensor</p>

        case DeviceTabState.addPallet:
            return <p>add Pallet</p>

        default:
            return <DevicesListComponent changeActiveTab={setShowedTab} />
    }
}

export default DevicesTabComponent;