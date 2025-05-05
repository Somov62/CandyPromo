import axios, { AxiosResponse } from "axios";
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
        const result = await axios.get<IDatePrizeResult>("/api/Promo/date");
        return new Date(result.data.result).getTime();
    } catch (error) {
        return new Date();
    }
}

const datePrize = await getDatePrize();
// #endregion

export default function timerBlock() {
    return (
        <div className="TimerWrapper flex-column mt-5 flex">
            <h1>Отсчет до розыгрыша</h1>
            <Timer endTime={datePrize} />
        </div>
    );
}