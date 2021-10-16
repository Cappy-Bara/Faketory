export interface CreateConveyorForm{
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