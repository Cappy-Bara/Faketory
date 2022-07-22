import axios from 'axios';

const axiosInstance = axios.create({
    baseURL: "https://localhost:44368/",
    headers: {"Authorization": "NONE"}
})

const initializeToken = (token: string) => {
    if(axiosInstance.defaults.headers){
        axiosInstance.defaults.headers = {'Authorization': `Bearer ${token}`};
    }
}

const axiosElement = {
    axiosInstance: axiosInstance,
    initializeToken: initializeToken,
}

export default axiosElement;