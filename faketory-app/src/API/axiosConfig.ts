import axios from 'axios';


const axiosInstance = axios.create({
    baseURL: "http://localhost:5000/",
})

axiosInstance.interceptors.request.use((config:any) => {
    config.headers.Authorization = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJrYWNwZXI5MTZAZ21haWwuY29tIiwiZXhwIjoxNjM1MjgyNzYzLCJpc3MiOiJodHRwczovL2Zha2V0b3J5LmNvbSIsImF1ZCI6Imh0dHBzOi8vZmFrZXRvcnkuY29tIn0.utU6cwnGrAnrF1viv5HM4P-pqeBM0qVycRnBqPDf-zM";
    return config;
})

export default axiosInstance