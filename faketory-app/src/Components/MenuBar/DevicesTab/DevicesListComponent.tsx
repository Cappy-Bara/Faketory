import { Button, Table } from "react-bootstrap";
import { useDispatch } from "react-redux";
import { setOpenedDevicesSubtab } from "../../../States/menuBar/openedDevicesSubtab/actions";
import { DeviceTabState } from "./EDevicesTabState";


const DevicesListComponent = () => {

    const dispatch = useDispatch();

    return (
        <>
            <h3 className="">Devices</h3>
            <Table striped hover size="sm">
                <tbody>
                    <tr>
                        <td className="px-3 py-2">Conveyor</td>
                        <td>
                            <Button
                                size="sm"
                                variant="success"
                                className="float-end add-button px-3 mx-2"
                                onClick={() => dispatch(setOpenedDevicesSubtab(DeviceTabState.addConveyor))}
                            >
                                Add
                            </Button>
                        </td>
                    </tr>
                    <tr>
                        <td className="px-3 py-2">Sensor</td>
                        <td>
                            <Button
                                size="sm"
                                variant="success"
                                className="float-end add-button px-3 mx-2"
                                onClick={() => dispatch(setOpenedDevicesSubtab(DeviceTabState.addSensor))}
                            >
                                Add
                            </Button>
                        </td>
                    </tr>
                    <tr>
                        <td className="px-3 py-2">Pallet</td>
                        <td>
                            <Button
                                size="sm"
                                variant="success"
                                className="float-end add-button px-3 mx-2"
                                onClick={() => dispatch(setOpenedDevicesSubtab(DeviceTabState.addPallet))}
                            >
                                Add
                            </Button>
                        </td>
                    </tr>
                    <tr>
                        <td className="px-3 py-2">Machine</td>
                        <td>
                            <Button
                                size="sm"
                                variant="success"
                                className="float-end add-button px-3 mx-2"
                                onClick={() => dispatch(setOpenedDevicesSubtab(DeviceTabState.addMachine))}
                            >
                                Add
                            </Button>
                        </td>
                    </tr>
                </tbody>
            </Table>
        </>
    )

}
export default DevicesListComponent;