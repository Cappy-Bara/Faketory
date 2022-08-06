import Pallet from "../../Components/Devices/PalletComponent/Types";
import Conveyor from "../../Components/Devices/ConveyorComponent/Types";
import Sensor from "../../Components/Devices/SensorComponent/Types";
import Machine from "../../Components/Devices/MachineComponent/types";

export interface timestampResponse{
    pallets:Pallet[]
    sensors:SensorState[]
    conveyors:ConveyorState[]
    machines:MachineState[]
}

export interface staticObjectResponse{
    conveyors:Conveyor[],
    sensors: Sensor[],
    machines: Machine[],
}

export interface allObjectResponse{
    conveyors:Conveyor[],
    machines: Machine[],
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

export interface MachineState{
    id:string;
    isProcessing: boolean;
}