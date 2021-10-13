import Conveyor from "../../Components/ConveyorComponent/Types";

export interface CreateConveyor{
    slotId: string,
    posX: number,
    posY: number,
    length: number,
    isVertical: boolean,
    isTurnedDownOrLeft: boolean,
    frequency: number,
    bit: number,
    byte: number,
    negativeLogic: boolean
}

export interface UpdateConveyor{
    conveyorId: string,
    slotId: string,
    posX: number,
    posY: number,
    length: number,
    isVertical: boolean,
    isTurnedDownOrLeft: boolean,
    frequency: number,
    bit: number,
    byte: number,
    negativeLogic: boolean
}

export interface ConveyorsResponse{
    conveyors: Conveyor[];
}