import { instance } from "../Axios/axiosConfig"

const promoService = {

    getEndingDate() {
        return instance.get('api/promo/date');
    },

    active() {
        return instance.get('api/promo/active');
    }
};

export default promoService