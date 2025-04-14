import { instance } from "../Axios/axiosConfig"


const prizesService = {

    getPrizesSummary() {
        return instance.get('api/prizes')
    },

    getPrizesFullDetails() {
        return instance.get('api/prizes/details')
    },

    getPrizesFullDetailsById (prizeId: string) {
        return instance.get(`api/prizes/${prizeId}/details`)
    },

    getWinnerContacts (prizeId: string) {
        return instance.get(`api/prizes/${prizeId}/contacts`)
    },

    updatePrizeStatus (prizeId: string, status: string) {
        return instance.put(`api/prizes/${prizeId}/status`, status)
    },

    getPrizeStatuses() {
        return instance.get('api/prizes/statuses')
    },
}

export default prizesService