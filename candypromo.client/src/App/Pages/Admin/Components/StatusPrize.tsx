import axios, { AxiosResponse } from "axios";
import promoService from "../../../../API/Services/promoService";
import Timer from "../../../Components/Timer/Timer";
import "./StatusPrize.css"

// #region Classes

interface IDatePrizeResult {
    result: Date;
    errors: any;
    timeGenerated: Date;
}

// #endregion

// #region Methods

async function getDatePrize(): Promise<number> {

    try {
        return await promoService.getEndingDate();
    }
    catch (error) {
        return Date.now();
    }
}

const date = await getDatePrize();

// #endregion
export default function statusPrize() {
    return (
        <div className="status-prize-block items-center mb-6 mt-6">
            <h2 className="">Победители пока не определены</h2>
            <h3 className="">Розыгрыш состоится {new Date(date).toLocaleDateString()}</h3>
            <Timer endTime={date} />
        </div>
    )
}