import { useEffect, useState } from "react";
import { Button, ToggleButton } from "react-bootstrap";
import { useDispatch } from "react-redux";
import { timestamp } from "../../../API/Actions/actions";
import { setAnimationState } from "../../../States/animationSource/actions";
import { updateUserConveyors } from "../../../States/devices/userConveyors/actions";
import { updateUserMachines } from "../../../States/devices/userMachines/actions";
import { modifyUserPallets } from "../../../States/devices/userPallets/actions";
import { updateUserSensors } from "../../../States/devices/userSensors/actions";

const MainTabComponent = () => {

    const [autoTimestamp, setAutoTimestamp] = useState<boolean>(true);
    const dispatch = useDispatch();

    const handleTimestamp = () => {
        timestamp().then(response => {
            response.sensors && dispatch(updateUserSensors(response.sensors));
            response.pallets && dispatch(modifyUserPallets(response.pallets));
            response.conveyors && dispatch(updateUserConveyors(response.conveyors));
            response.machines && dispatch(updateUserMachines(response.machines));
            dispatch(setAnimationState());
        })
    };

    useEffect(() => {
    if (!autoTimestamp) {
        const interval = setInterval(handleTimestamp, 500);
        return () => clearInterval(interval);
    };
    }, [autoTimestamp]);

    return (

        <div className="text-center">
            
            <Button
                className="my-3 m-3 col-5"
                variant="primary"
                onClick={handleTimestamp}
            >
                Timestamp
            </Button>

            <br/>

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
        </div>
    )
}

export default MainTabComponent;