import apiClient from '../axiosConfig';
import { CreatePlc, plcResponse, plcStatusesResponse } from './types';

export const getPlcs = () => {
    return apiClient.get<plcResponse>(`/api/Plc`).then(response => response.data); 
}

export const deletePlc = (plcId:string) => {
    return apiClient.delete(`/api/Plc?PlcId=${plcId}`); 
}

export const addPlc = (createPlc:CreatePlc) => {
    return apiClient.post(`/api/Plc`,createPlc); 
}

export const connectToPlc = (plcId:string) => {
    return apiClient.patch(`/api/Plc?PlcId=${plcId}`); 
}

export const getPlcStatuses = () => {
    return apiClient.get<plcStatusesResponse>(`/api/Plc/Connections`).then(response => response.data); 
}