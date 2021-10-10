import {Slot} from '../Components/MenuBar/PLCTab/Types';
import apiClient from './axiosConfig';

export const getSlots = () => {
    return apiClient.get<Slot[]>(`/api/Slot`).then(response => response.data); 
}

export const deleteSlot = (slotId:string) => {
    return apiClient.delete(`/api/Slot?id=${slotId}`).then(response => response.data); 
}

export const addSlot = () => {
    return apiClient.post(`/api/Slot`).then(response => response.data); 
}

export const bindSlotWithPlc = (slotId:string, plcId:string) => {
    return apiClient.patch(`/api/Slot?plcId=${plcId}&slotId=${slotId}`).then(response => response.data); 
}