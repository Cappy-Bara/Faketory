import Pallet from "../../Components/Devices/PalletComponent/Types";
import Conveyor from "../../Components/Devices/ConveyorComponent/Types";
import Sensor from "../../Components/Devices/SensorComponent/Types";

export interface timestampResponse{
    pallets:Pallet[]
    sensors:SensorState[]
    conveyors:ConveyorState[]
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

export interface SensorState{
    id: string;
    isSensing: boolean;
}

export interface ConveyorState{
    id:string;
    isRunning: boolean;
}
