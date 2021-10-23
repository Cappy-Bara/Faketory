import Sensor from "../../Components/Devices/SensorComponent/Types";

export interface sensorResponse{
    sensors:Sensor[],
}

export interface UpdateSensor{
    sensorId: string,
    slotId: string,
    posX: number,
    posY: number,
    bit: number,
    byte: number,
    negativeLogic: boolean
}