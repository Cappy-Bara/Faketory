import { Button, ToggleButton } from "react-bootstrap";
import { useDispatch } from "react-redux";
import { staticElements, timestamp } from "../../../API/Actions/actions";
import { setUserConveyors } from "../../../States/devices/userConveyors/actions";
import { setUserPallets } from "../../../States/devices/userPallets/actions";
import { setUserSensors } from "../../../States/devices/userSensors/actions";

const MainTabComponent = ({ autoTimestamp, setAutoTimestamp }: any) => {

    const dispatch = useDispatch();
    const handleTimestamp = () => {
        timestamp().then(palletResponse => {
          staticElements().then(staticElements => {
            dispatch(setUserPallets(palletResponse.pallets))
            dispatch(setUserConveyors(staticElements.conveyors));
            dispatch(setUserSensors(staticElements.sensors));
          })
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