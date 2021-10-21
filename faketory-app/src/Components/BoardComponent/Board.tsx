import "./styles.css";
import ConveyorComponent from "../Devices/ConveyorComponent/ConveyorComponent";
import Conveyor from "../Devices/ConveyorComponent/Types";
import Pallet from "../Devices/PalletComponent/Types";
import PalletComponent from "../Devices/PalletComponent/PalletComponent";
import { useEffect} from "react";
import { staticElements, timestamp } from "../../API/Actions/actions";
import { useDispatch, useSelector } from "react-redux";
import { IState } from "../../States";
import { setUserConveyors } from "../../States/devices/userConveyors/actions";
import { setUserPallets } from "../../States/devices/userPallets/actions";
import { setUserSensors } from "../../States/devices/userSensors/actions";
import Sensor from "../Devices/SensorComponent/Types";
import SensorComponent from "../Devices/SensorComponent/SensorComponent";



const Board = ({autoTimestampOn}:any) => {

  const userConveyors = useSelector<IState, Conveyor[]>(state => state.userConveyors);
  const userSensors = useSelector<IState, Sensor[]>(state => state.userSensors);
  const userPallets = useSelector<IState, Pallet[]>(state => state.userPallets);

  const dispatch = useDispatch();


  const handleTimestampButton = () => {
    timestamp().then(response =>
      dispatch(setUserPallets(response.pallets))
    );
  }

  const handleGetStaticElementsButton = () => {
    staticElements().then(response => {
      dispatch(setUserConveyors(response.conveyors));
      dispatch(setUserSensors(response.sensors));
    });
  }

  const autoTimestamp = () => {
    handleTimestampButton();
    handleGetStaticElementsButton();
  }

  useEffect(() => {
    if (!autoTimestampOn) {
      const interval = setInterval(autoTimestamp, 250);
      return () => clearInterval(interval);
    };
  }, [autoTimestampOn]);

  return (

    <div
      className="grid"
      style={{
        width: "80vw",
        height: "44.8vw",
        position: "relative",
      }}
    >
      {
        userConveyors && userConveyors.map(conveyor =>
          <ConveyorComponent key={conveyor.id} conveyor={conveyor} />
        )}

      {
        userSensors && userSensors.map(sensor =>
          <SensorComponent key={sensor.id} sensor={sensor} />
        )}

      {
        userPallets && userPallets.map(pallet =>
          <PalletComponent key={pallet.id} pallet={pallet} />
        )}

    </div>
  )
}
export default Board;