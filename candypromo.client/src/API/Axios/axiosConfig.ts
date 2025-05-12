import axios, {AxiosInstance, AxiosResponse, InternalAxiosRequestConfig} from "axios";

export const instance : AxiosInstance  = axios.create();

instance .interceptors.request.use((config : InternalAxiosRequestConfig<any>) : InternalAxiosRequestConfig<any> => {
    config.headers.Authorization = `Bearer ${localStorage.getItem("token") || ''}`;
    config.headers["Content-Type"] = "application/json";
    return config;
});

instance .interceptors.response.use(
    (config : AxiosResponse<any, any>): AxiosResponse<any, any> => {
        return config;
    },

    async (error) => {
        const originalRequest = {...error.config};

        originalRequest._isRetry = true;
        console.log(error.response);
        // if(
        //     error.response.status === 401 &&
        //     error.config &&
        //     !error.config._isRetry
        // ){
        //     try{
        //         const accessToken = localStorage.getItem('token')
        //         const refreshToken = localStorage.getItem('refresh-token')
        //
        //         const response = await instance .post('/api/refresh-token',{accessToken,refreshToken})
        //         console.log(response)
        //
        //         localStorage.setItem('token', response.data.data.accessToken)
        //         localStorage.setItem('refresh-token', response.data.data.refreshToken)
        //
        //
        //         return instance .request(originalRequest)
        //     }catch (error) {
        //         console.log('Auth Error')
        //         window.location.href = APP_URL
        //     }
        // }

        throw error.response;
    }
);