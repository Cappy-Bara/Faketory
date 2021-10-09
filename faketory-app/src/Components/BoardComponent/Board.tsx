import "./styles.css";
import ConveyorComponent from "../ConveyorComponent/ConveyorComponent";
import Conveyor from "../ConveyorComponent/Types";
import Pallet from "../PalletComponent/Types";
import PalletComponent from "../PalletComponent/PalletComponent";
import { useState } from "react";
import { Button } from "react-bootstrap";
import { staticElements, timestamp } from "../../API/actions";
import Sensor from "../SensorComponent/Types";
import SensorComponent from "../SensorComponent/SensorComponent";



const Board = () => {

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
  const sensor : Sensor = {
    id: "86c47fee-c375-4zaa-a55b-e97201a2621c",
    posX: 4,
    posY: 4,
    isSensing: true,
    ioId: "92a17fze-g375-4zaa-a55b-e97201a2621c"
  }

  const [pallets, setPallets] = useState<Pallet[]>([pallet, pallet2]);
  const [conveyors, setConveyors] = useState<Conveyor[]>([conveyor]);
  const [sensors, setSensors] = useState<Sensor[]>([sensor]);


  const handleTimestampButton =() => {
    timestamp().then(response =>
      setPallets(response.pallets)
    );
  }

  const handleGetStaticElementsButton = () => {
    staticElements().then(response => {
      setConveyors(response.conveyors);
      setSensors(response.sensors);
    }
    );
  }
  
  return (
    <div
      style={{
        display: "flex",
        alignContent: "center",
        justifyContent: "right",
      }}
    >

      <div className="menu-bar">
        <Button
          className="my-3 mx-1 col-5"
          variant="primary"
          onClick={handleTimestampButton}
        >
          Timestamp
        </Button>

        <Button
          className="my-3 mx-1 col-5"
          variant="primary"
          onClick={handleGetStaticElementsButton}
        >
          Refresh Static Elements
        </Button>



      </div>

      <div
        className="grid"
        style={{
          width: "80vw",
          height: "44.8vw",
          position: "relative",
        }}
      >

        {/* <p className="background-text"
           style={{
            width: "80vw",
            height: "44.8vw"}}>
            F@KET0RY
            </p> */}


        {
          conveyors && conveyors.map(conveyor =>
            <ConveyorComponent key={conveyor.id} conveyor={conveyor} />
          )}

        {
          sensors && sensors.map(sensor =>
            <SensorComponent key={sensor.id} sensor={sensor} />
          )}

        {
          pallets && pallets.map(pallet =>
            <PalletComponent key={pallet.id} pallet={pallet} />
          )}


      </div>
    </div>
  )
}
export default Board;