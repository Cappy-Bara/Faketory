import Machine from "../../Components/Devices/MachineComponent/types";

export interface CreateMachine{
    posX: number,
    posY: number,
    processingTimestampAmount: number,
    randomFactor: number,
}

export interface UpdateMachine{
    id: string,
    posX: number,
    posY: number,
    processingTimestampAmount: number,
    randomFactor: number,
}

export interface MachinesResponse{
    machines: Machine[];
}