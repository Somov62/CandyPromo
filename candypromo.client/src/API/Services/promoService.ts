import { instance } from "../Axios/axiosConfig"

const promoService = {

    async getEndingDate() {
        const response = await instance.get("api/promo/date");
        return new Date(response.data.result).getTime();
    },

    active() {
        return instance.get("api/promo/active");
    }
};

export default promoService