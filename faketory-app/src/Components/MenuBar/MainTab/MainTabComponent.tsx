import { Button, ToggleButton } from "react-bootstrap";
import { useDispatch } from "react-redux";
import { staticElements, timestamp } from "../../../API/Actions/actions";
import { setUserConveyors } from "../../../States/devices/userConveyors/actions";
import { setUserPallets } from "../../../States/devices/userPallets/actions";
import { setUserSensors } from "../../../States/devices/userSensors/actions";

const MainTabComponent = ({ autoTimestamp, setAutoTimestamp }: any) => {

    const dispatch = useDispatch();
    const handleTimestamp = () => {
        timestamp().then(response =>
            dispatch(setUserPallets(response.pallets)))
        staticElements().then(response => {
            dispatch(setUserConveyors(response.conveyors));
            dispatch(setUserSensors(response.sensors));
        })};

    return (

            <>
                <Button
                    className="my-3 mx-1 col-5"
                    variant="primary"
                    onClick={handleTimestamp}
                >
                    Timestamp
                </Button>

                {<ToggleButton
                    className="mb-2"
                    id="toggle-check"
                    type="checkbox"
                    variant="outline-primary"
                    checked={autoTimestamp}
                    value="0"
                    onChange={(e) => {
                        console.log(e.currentTarget.checked)
                        setAutoTimestamp(e.currentTarget.checked);
                    }}
                >
                    Auto Timestamp
                </ToggleButton>}
            </>
        )
    }

    export default MainTabComponent;