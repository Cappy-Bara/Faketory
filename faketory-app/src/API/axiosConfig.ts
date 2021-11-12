import axios from 'axios';

var i = 0;

const axiosInstance = axios.create({
    baseURL: "http://localhost:5000/",
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