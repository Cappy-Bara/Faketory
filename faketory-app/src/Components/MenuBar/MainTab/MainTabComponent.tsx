import { useEffect, useState } from "react";
import { Button, ToggleButton } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { timestamp } from "../../../API/Actions/actions";
import { setAnimationState } from "../../../States/animationSource/actions";
import { updateUserConveyors } from "../../../States/devices/userConveyors/actions";
import { updateUserMachines } from "../../../States/devices/userMachines/actions";
import { modifyUserPallets } from "../../../States/devices/userPallets/actions";
import { updateUserSensors } from "../../../States/devices/userSensors/actions";
import * as signalR from "@microsoft/signalr";
import { timestampResponse } from "../../../API/Actions/types";
import { IState } from "../../../States";
import React from "react";


const MainTabComponent = () => {

    const [autoTimestamp, setAutoTimestamp] = useState<boolean>(true);
    const [socketAutoTimestamp, setSocketAutoTimestamp] = useState<boolean>(true);
    const userEmail = useSelector<IState, string | null>(state => state.loggedUser);
    const [connection,setConnection] = useState<signalR.HubConnection|null>(null); 

    const dispatch = useDispatch();

    const dispatchTimestampData = (data : timestampResponse) => {
        data.sensors && dispatch(updateUserSensors(data.sensors));
        data.pallets && dispatch(modifyUserPallets(data.pallets));
        data.conveyors && dispatch(updateUserConveyors(data.conveyors));
        data.machines && dispatch(updateUserMachines(data.machines));
        dispatch(setAnimationState());
    }

    const handleTimestamp = () => {
        timestamp().then(response => {
            dispatchTimestampData(response)
        })
    };

    useEffect(() => {
    if (!autoTimestamp) {
        const interval = setInterval(handleTimestamp, 500);
        return () => clearInterval(interval);
    };
    }, [autoTimestamp]);

    const handleSocketTimestamp = (data : timestampResponse) => {
        console.log("TIMESTAMP")
        dispatchTimestampData(data);
    }

    const startOrStopAutoTimestamp = (shouldNotStart : boolean) => {
        setSocketAutoTimestamp(shouldNotStart);
        if(shouldNotStart){
            connection!.invoke("StopTimestamping");
        }
        else{
            connection!.invoke("StartTimestamping");
        }
    }

    useEffect(() => {
        if(!connection){
            let newConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:44368/timestamphub', {
                skipNegotiation: true,
                transport: signalR.HttpTransportType.WebSockets
            })
            .build();
    
            newConnection.start();
    
            newConnection.on('timestamp', (data : timestampResponse) => handleSocketTimestamp(data));
            setConnection(newConnection);
        }
    },[]);

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
                onChange={(e) =>{
                    setAutoTimestamp(e.currentTarget.checked)}
                }
            >
                Auto Timestamp
            </ToggleButton>


            <ToggleButton
                className="mb-2"
                id="toggle-check-socket"
                type="checkbox"
                variant="outline-primary"
                checked={socketAutoTimestamp}
                value="0"
                onChange={(e) =>{
                    startOrStopAutoTimestamp(e.currentTarget.checked)}
                }
            >
                Socket auto timestamp
            </ToggleButton>
        </div>


    )
}

export default MainTabComponent;