import { instance } from "../Axios/axiosConfig"

const promoService = {

    getEndingDate() {
        return instance.get('api/promo/date')
    },
}

export default promoService