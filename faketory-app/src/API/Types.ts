import Pallet from "../Components/PalletComponent/Types";
import Conveyor from "../Components/ConveyorComponent/Types";
import Sensor from "../Components/SensorComponent/Types";
import {Slot,PLC, PlcStatus } from "../Components/MenuBar/PLCTab/Types";


export interface timestampResponse{
    pallets:Pallet[]
}

export interface staticObjectResponse{
    conveyors:Conveyor[],
    sensors: Sensor[],
}

export interface slotResponse{
    slots:Slot[],
}

export interface plcResponse{
    plcs:PLC[],
}

export interface plcStatusesResponse{
    plcs:PlcStatus[]
}


export interface CreatePlc{
    ip: string,
    modelId: number
}