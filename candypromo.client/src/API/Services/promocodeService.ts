import { instance } from "../Axios/axiosConfig"


const promocodeService = {

    getPromocodesCount() {
        return instance.get('api/promocode/count')
    },

    register (promocode: string) {
        return instance.post('api/promocode/register', promocode)
    },

    getMyPromocodes() {
        return instance.get('api/promocode')
    }
}

export default promocodeService