import { CreateSensorForm } from '../../Components/MenuBar/DevicesTab/Subtabs/Sensor/types';
import axiosInstance from '../axiosConfig';
import { sensorResponse, UpdateSensor } from './types';

const apiClient = axiosInstance.axiosInstance;

export const getSensors = () => {
    return apiClient.get<sensorResponse>(`/api/Sensor/all`).then(response => response.data); 
}

export const deleteSensor = (sensorId:string) => {
    return apiClient.delete(`/api/Sensor`,{data : {sensorId : sensorId}}); 
}

export const addSensor = (createSensorForm:CreateSensorForm) => {
    return apiClient.post(`/api/Sensor`,{...createSensorForm}); 
}

export const updateSensor = (updateSensor:UpdateSensor) => {
    return apiClient.put(`/api/Sensor`,{...updateSensor}); 
}