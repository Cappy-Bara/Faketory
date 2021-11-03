import apiClient from '../axiosConfig';
import { Login, RegisterAccount } from './types';

export const register = (data : RegisterAccount) => {
    return apiClient.post(`/api/Account/register`,data); 
}

export const login = (data:Login) => {
    return apiClient.post(`/api/Account/login`,data).then(response => response.data); 
}