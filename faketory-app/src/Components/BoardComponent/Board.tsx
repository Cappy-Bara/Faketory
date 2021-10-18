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

  const pallet: Pallet = {
    id: "fe08d3c3-dbe3-45aa-b973-6977b834826b",
    posX: 7,
    posY: 10,
  }
  const pallet2: Pallet = {
    id: "80c47f0e-c375-4eab-a55b-e97201a2621c",
    posX: 8,
    posY: 10,
  }
  const conveyor: Conveyor = {
    id: "9b8ff161-a649-41d4-ec25-08d982c18279",
    posX: 12,
    posY: 10,
    length: 6,
    isRunning: true,
    isTurnedDownOrLeft: true,
    isVertical: false,
    frequency: 0,
    slot: "4bb433f1-4a4a-43f1-1732-08d95d116f0d",
    byte: 0,
    bit: 0
  }
  const sensor: Sensor = {
    id: "86c47fee-c375-4zaa-a55b-e97201a2621c",
    posX: 4,
    posY: 4,
    isSensing: true,
    ioId: "92a17fze-g375-4zaa-a55b-e97201a2621c"
  }


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