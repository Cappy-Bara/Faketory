import { Button, ToggleButton } from "react-bootstrap";
import { useDispatch } from "react-redux";
import { timestamp } from "../../../API/Actions/actions";
import { updateUserConveyors } from "../../../States/devices/userConveyors/actions";
import { modifyUserPallets } from "../../../States/devices/userPallets/actions";
import { updateUserSensors } from "../../../States/devices/userSensors/actions";

const MainTabComponent = ({ autoTimestamp, setAutoTimestamp }: any) => {

    const dispatch = useDispatch();

    const handleTimestamp = () => {
        timestamp().then(response => {
            response.sensors && dispatch(updateUserSensors(response.sensors));
            response.pallets && dispatch(modifyUserPallets(response.pallets));
            response.conveyors && dispatch(updateUserConveyors(response.conveyors));
        })
    };


    return (

        <>
            <Button
                className="my-3 mx-1 col-5"
                variant="primary"
                onClick={handleTimestamp}
            >
                Timestamp
            </Button>

            <ToggleButton
                className="mb-2"
                id="toggle-check"
                type="checkbox"
                variant="outline-primary"
                checked={autoTimestamp}
                value="0"
                onChange={(e) =>
                    setAutoTimestamp(e.currentTarget.checked)
                }
            >
                Auto Timestamp
            </ToggleButton>
        </>
    )
}

export default MainTabComponent;