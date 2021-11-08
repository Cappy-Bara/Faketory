import Sensor from "./Types";
import "./styles.css"
import { useDispatch } from "react-redux";
import { setSensorToModify } from "../../../States/menuBar/deviceToModify/sensorToModify/actions";
import { setOpenedTab } from "../../../States/menuBar/openedTab/actions";
import { setOpenedDevicesSubtab } from "../../../States/menuBar/openedDevicesSubtab/actions";
import { DeviceTabState } from "../../MenuBar/DevicesTab/EDevicesTabState";

interface Props{
    sensor :Sensor;
}


const SensorComponent = ({sensor} : Props) => {

    const dispatch = useDispatch();

    const handleClick = () => {
        dispatch(setSensorToModify(sensor));
        dispatch(setOpenedTab("devices"))
        dispatch(setOpenedDevicesSubtab(DeviceTabState.modifySensor))
    }

    const tileSize: number = 3.2;

    return(
        <div

            key={sensor.posX}
            className={sensor.isSensing ? "sensor-active" : "sensor-inactive"}
            onClick={handleClick}
            style={{
                position: `absolute`,
                bottom:`${((sensor.posY)*tileSize)}vw`,
                left:`${((sensor.posX)*tileSize)}vw`,
                width: `${tileSize}vw`,
                height: `${tileSize}vw`,
            }}
        />
    )
}
export default SensorComponent;