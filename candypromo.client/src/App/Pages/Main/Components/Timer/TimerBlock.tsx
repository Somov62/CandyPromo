import axios, { AxiosResponse } from "axios";
import promoService from "../../../../../API/Services/promoService";
import Timer from "../../../../Components/Timer/Timer";
import "./TimerBlock.css";

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
    } catch (error) {
        return new Date();
    }
}

const datePrize = await getDatePrize();
// #endregion

export default function TimerBlock() {
    return (
        <div className="TimerWrapper flex-column mt-5 flex">
            <h1>Отсчет до розыгрыша</h1>
            <Timer endTime={datePrize} />
        </div>
    );
}