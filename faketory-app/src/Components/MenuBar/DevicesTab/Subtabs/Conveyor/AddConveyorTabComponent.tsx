import { Button } from "react-bootstrap"
import { DeviceTabState } from "../../EDevicesTabState";



const AddConveyorTabComponent = ({changeActiveTab}: any) => {

    return (
        <>
            <Button
                size="sm"
                variant="secondary"
                className="float-end add-button px-3 mx-2"
                onClick={() => changeActiveTab(DeviceTabState.list)}
            >
                Back
            </Button>
        </>
    )
}

export default AddConveyorTabComponent;
