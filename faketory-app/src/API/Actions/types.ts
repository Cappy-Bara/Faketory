import Pallet from "../../Components/Devices/PalletComponent/Types";
import Conveyor from "../../Components/Devices/ConveyorComponent/Types";
import Sensor from "../../Components/Devices/SensorComponent/Types";

export interface timestampResponse{
    pallets:Pallet[]
}

export interface staticObjectResponse{
    conveyors:Conveyor[],
    sensors: Sensor[],
}

export interface allObjectResponse{
    conveyors:Conveyor[],
    sensors: Sensor[],
    pallets: Pallet[],
}

