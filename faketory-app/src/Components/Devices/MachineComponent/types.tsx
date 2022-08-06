export default interface Machine{
    id:string;
    posX:number;
    posY:number;
    randomFactor: number;
    processingTimestampAmount: number;
    isProcessing: boolean;
}