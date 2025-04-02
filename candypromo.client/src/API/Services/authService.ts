import { instance } from "../Axios/axiosConfig"


const authService = {

    login (Email:string, Phone:string, Password:string) {
        return instance.post('api/auth/login', {Email, Phone, Password})
    },

    register (Name:string, Email:string, Phone:string, Password:string) {
        return instance.post('api/auth/register', {Name, Email, Phone, Password})
    },

}

export default authService