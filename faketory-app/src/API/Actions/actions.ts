import apiClient from '../axiosConfig';
import  {timestampResponse, staticObjectResponse, allObjectResponse } from './types';


export const timestamp = () => {
    return apiClient.post<timestampResponse>(`/api/Action/timestamp`).then(response => response.data); 
}

export const staticElements = () => {
    return apiClient.get<staticObjectResponse>(`/api/Action/elements/static`).then(response => response.data); 
}

export const allElemets = () => {
    return apiClient.get<allObjectResponse>(`/api/Action/elements/all`).then(response => response.data); 
}