export interface Slot{
    id:string;
    number: number,
    plcId: string | null
}

export interface PLC{
    id:string;
    ip:string,
    model:number;
    isConnected:boolean;
}

export interface PlcStatus{
    plcId: string,
    status: boolean
}