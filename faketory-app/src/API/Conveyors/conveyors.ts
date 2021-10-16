import Conveyor from '../../Components/ConveyorComponent/Types';
import apiClient from '../axiosConfig';
import { ConveyorsResponse, CreateConveyor, UpdateConveyor } from './types';

export const getConveyor = (conveyorId:string) => {
    return apiClient.get<Conveyor>(`/api/Conveyor?Id=${conveyorId}`).then(response => response.data); 
}

export const deleteConveyor = (conveyorId:string) => {
    return apiClient.delete(`/api/Conveyor`,{params: conveyorId}); 
}

export const getConveyors = () => {
    return apiClient.get<ConveyorsResponse>(`/api/Conveyor/all`); 
}

export const addConveyor = (createConveyor:CreateConveyor) => {
    console.log(createConveyor);
    return apiClient.post(`/api/Conveyor`,{...createConveyor}); 
}

export const updateConveyor = (updateConveyor:UpdateConveyor) => {
    return apiClient.put(`/api/Conveyor`,{...updateConveyor}); 
}