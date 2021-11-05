import { stringify } from 'querystring';
import axiosInstance from '../axiosConfig';
import { LoginData, RegisterAccountData } from './types';

const apiClient = axiosInstance.axiosInstance;


export const register = (data: RegisterAccountData) => {
    return apiClient.post(`/api/Account/register`, data);
}

export const login = (data: LoginData) => {
    return apiClient.post(`/api/Account/login`, data).then(response => {
        axiosInstance.initializeToken((response.data).toString());
        return response.data;
    }
    );
}