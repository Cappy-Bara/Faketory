import axios from 'axios';


const axiosInstance = axios.create({
    baseURL: "http://localhost:5000/",
})

axiosInstance.interceptors.request.use((config:any) => {
    config.headers.Authorization = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJrYWNwZXI5MTZAZ21haWwuY29tIiwiZXhwIjoxNjM0NTc1OTQ3LCJpc3MiOiJodHRwczovL2Zha2V0b3J5LmNvbSIsImF1ZCI6Imh0dHBzOi8vZmFrZXRvcnkuY29tIn0.aHjjbGGPLyXexOaR1iJt8agGPR8_urxkL8FR8Yhdtm0";
    return config;
})

export default axiosInstance