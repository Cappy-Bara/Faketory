import "./styles.css";
import Conveyor from "../Devices/ConveyorComponent/Types";
import Pallet from "../Devices/PalletComponent/Types";
import { useEffect } from "react";
import { allElemets, timestamp } from "../../API/Actions/actions";
import { useDispatch, useSelector } from "react-redux";
import { IState } from "../../States";
import { setUserConveyors, updateUserConveyors } from "../../States/devices/userConveyors/actions";
import { modifyUserPallets, setUserPallets } from "../../States/devices/userPallets/actions";
import { setUserSensors, updateUserSensors } from "../../States/devices/userSensors/actions";
import Sensor from "../Devices/SensorComponent/Types";
import PalletComponent from "../Devices/PalletComponent/PalletComponent";
import SensorComponent from "../Devices/SensorComponent/SensorComponent";
import ConveyorComponent from "../Devices/ConveyorComponent/ConveyorComponent";
import { setAnimationState } from "../../States/animationSource/actions";
import Machine from "../Devices/MachineComponent/types";
import { setUserMachines, updateUserMachines } from "../../States/devices/userMachines/actions";
import MachineComponent from "../Devices/MachineComponent/MachineComponent";


const Board = ({ autoTimestampOn }: any) => {

  const userConveyors = useSelector<IState, Conveyor[]>(state => state.userConveyors);
  const userSensors = useSelector<IState, Sensor[]>(state => state.userSensors);
  const userPallets = useSelector<IState, Pallet[]>(state => state.userPallets);
  const userMachines = useSelector<IState, Machine[]>(state => state.userMachines);

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
    if (!autoTimestampOn) {
      const interval = setInterval(handleTimestamp, 500);
      return () => clearInterval(interval);
    };
  }, [autoTimestampOn]);

  useEffect(() => {
    allElemets().then(response => {
      dispatch(setUserPallets(response.pallets))
      dispatch(setUserConveyors(response.conveyors));
      dispatch(setUserSensors(response.sensors));
      dispatch(setUserMachines(response.machines));
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

      {
        userMachines && userMachines.map(machine =>
          <MachineComponent key={machine.id} machine={machine} />
        )}
    </div>
  )
}
export default Board;