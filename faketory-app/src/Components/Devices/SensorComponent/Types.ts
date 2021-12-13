export default interface Sensor{
    id:string;
    posX:number;
    posY:number;
    isSensing: boolean;
    negativeLogic: boolean;
    slotId: string;
    bit: number;
    byte: number;
}
