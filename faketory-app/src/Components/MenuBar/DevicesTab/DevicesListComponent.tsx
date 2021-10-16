import { Button, Table } from "react-bootstrap";
import { DeviceTabState } from "./EDevicesTabState";


const DevicesListComponent = ({ changeActiveTab }: any) => {

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
                            onClick={() => changeActiveTab(DeviceTabState.addConveyor)}
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
                            onClick={() => changeActiveTab(DeviceTabState.addSensor)}
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
                            onClick={() => changeActiveTab(DeviceTabState.addSensor)}
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