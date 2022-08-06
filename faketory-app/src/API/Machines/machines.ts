import Machine from '../../Components/Devices/ConveyorComponent/Types';
import axiosInstance from '../axiosConfig';
import { CreateMachine, MachinesResponse, UpdateMachine } from './types';

const apiClient = axiosInstance.axiosInstance;

export const getConveyor = (conveyorId:string) => {
    return apiClient.get<Machine>(`/api/Machines?Id=${conveyorId}`).then(response => response.data); 
}

export const deleteMachine = (machineId:string) => {
    return apiClient.delete(`/api/Machines`,{data : {machineId : machineId}}); 
}

export const getConveyors = () => {
    return apiClient.get<MachinesResponse>(`/api/Machines/all`); 
}

export const addMachine = (createMachine:CreateMachine) => {
    return apiClient.post(`/api/Machines`,{...createMachine}); 
}

export const updateMachine = (updateMachine:UpdateMachine) => {
    console.log(updateMachine.id)
    return apiClient.put(`/api/Machines`,{...updateMachine}); 
}