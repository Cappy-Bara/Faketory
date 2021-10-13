import Pallet from "../../Components/PalletComponent/Types";
import Conveyor from "../../Components/ConveyorComponent/Types";
import Sensor from "../../Components/SensorComponent/Types";

export interface timestampResponse{
    pallets:Pallet[]
}

export interface staticObjectResponse{
    conveyors:Conveyor[],
    sensors: Sensor[],
}

