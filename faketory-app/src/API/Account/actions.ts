import apiClient from '../axiosConfig';
import { LoginData, RegisterAccountData } from './types';

export const register = (data : RegisterAccountData) => {
    return apiClient.post(`/api/Account/register`,data); 
}

export const login = (data:LoginData) => {
    return apiClient.post(`/api/Account/login`,data).then(response => response.data); 
}