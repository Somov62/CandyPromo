import { instance } from "../Axios/axiosConfig"


const promocodeService = {

    getPromocodesCount() {
        return instance.get('api/promocode/count')
    },

    register (promocode: string) {
        return instance.post('api/promocode/register', promocode)
    },
}

export default promocodeService