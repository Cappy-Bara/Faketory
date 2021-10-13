import { PLC, PlcStatus } from "../../Components/MenuBar/PLCTab/Types";

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
