
interface ModifyConveyor{
    id:string;
    posX:number;
    posY:number;
    length: number;
    isRunning: boolean;
    isTurnedDownOrLeft:boolean;
    isVertical:boolean;
    frequency: number,
    slot: string,
    byte: number,
    bit: number
}

export default ModifyConveyor