import "./styles.css";
import ConveyorComponent from "../Devices/ConveyorComponent/ConveyorComponent";
import Conveyor from "../Devices/ConveyorComponent/Types";
import Pallet from "../Devices/PalletComponent/Types";
import PalletComponent from "../Devices/PalletComponent/PalletComponent";
import { useEffect } from "react";
import { allElemets, staticElements, timestamp } from "../../API/Actions/actions";
import { useDispatch, useSelector } from "react-redux";
import { IState } from "../../States";
import { setUserConveyors } from "../../States/devices/userConveyors/actions";
import { setUserPallets } from "../../States/devices/userPallets/actions";
import { setUserSensors } from "../../States/devices/userSensors/actions";
import Sensor from "../Devices/SensorComponent/Types";
import SensorComponent from "../Devices/SensorComponent/SensorComponent";



const Board = ({ autoTimestampOn }: any) => {

  const userConveyors = useSelector<IState, Conveyor[]>(state => state.userConveyors);
  const userSensors = useSelector<IState, Sensor[]>(state => state.userSensors);
  const userPallets = useSelector<IState, Pallet[]>(state => state.userPallets);

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

  const autoTimestamp = () => {
    handleTimestamp();
  }
  
  useEffect(() => {
    if (!autoTimestampOn) {
      const interval = setInterval(autoTimestamp, 500);
      return () => clearInterval(interval);
    };
  }, [autoTimestampOn]);

  useEffect(() => {
    allElemets().then(response => {
      dispatch(setUserPallets(response.pallets))
      dispatch(setUserConveyors(response.conveyors));
      dispatch(setUserPallets(response.pallets));
      dispatch(setUserSensors(response.sensors));
    })
  }, [])

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