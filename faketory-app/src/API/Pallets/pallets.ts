import { CreatePalletForm } from '../../Components/MenuBar/DevicesTab/Subtabs/Pallet/types';
import apiClient from '../axiosConfig';
import { palletsResponse, UpdatePallet } from './types';

export const getPallets = () => {
    return apiClient.get<palletsResponse>(`/api/Pallet/all`).then(response => response.data); 
}

export const deletePallet = (palletId:string) => {
    return apiClient.delete(`/api/Pallet`,{data : {palletId : palletId}}); 
}

export const addPallet = (createPalletForm:CreatePalletForm) => {
    return apiClient.post(`/api/Pallet`,{...createPalletForm}); 
}

export const updatePallet = (updatePalletForm:UpdatePallet) => {
    return apiClient.put(`/api/Pallet`,{...updatePalletForm}); 
}