import Sensor from "./Types";
import "./styles.css"

interface Props{
    sensor :Sensor;
}


const PalletComponent = ({sensor} : Props) => {

    const tileSize: number = 3.2;

    return(
        <div
            className={sensor.isSensing ? "sensor-active" :"sensor-inactive"}
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
export default PalletComponent;