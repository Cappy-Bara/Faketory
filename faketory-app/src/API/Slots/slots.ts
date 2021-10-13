import apiClient from '../axiosConfig';
import { slotResponse } from './types';

export const getSlots = () => {
    return apiClient.get<slotResponse>(`/api/Slot`).then(response => response.data); 
}

export const deleteSlot = (slotId:string) => {
    return apiClient.delete(`/api/Slot?id=${slotId}`); 
}

export const addSlot = () => {
    return apiClient.post(`/api/Slot`); 
}

export const bindSlotWithPlc = (slotId:string, plcId:string) => {
    return apiClient.patch(`/api/Slot?plcId=${plcId}&slotId=${slotId}`); 
}