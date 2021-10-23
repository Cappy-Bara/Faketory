interface Sensor{
    id:string;
    posX:number;
    posY:number;
    isSensing: boolean;
    ioId: string;
    negativeLogic: boolean;
    slotId: string;
    bit: number;
    byte: number;
}

export default Sensor