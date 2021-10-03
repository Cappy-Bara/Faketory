import Conveyor from '../Components/ConveyorComponent/Types';
import apiClient from './axiosConfig';

export const getConveyor = (conveyorId:string) => {
    return apiClient.get<Conveyor>(`/api/Conveyor?Id=${conveyorId}`).then(response => response.data); 
}