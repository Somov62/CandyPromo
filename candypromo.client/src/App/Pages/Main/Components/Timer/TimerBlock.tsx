import axios, { AxiosResponse } from "axios";
import Timer from "../../../../Components/Timer/Timer";
import "./TimerBlock.css";

// #region Classes

interface DatePrizeResult {
    result: Date;
    errors: any;
    timeGenerated: Date;
}

// #endregion

// #region Methods

async function GetDatePrize(): Promise<number> {
    try {
        var result: AxiosResponse<DatePrizeResult> = await axios.get<DatePrizeResult>("/api/Promo/date");
        return new Date(result.data.result).getTime();
    }
    catch {
        return Date.now();
    }
}

const date = await GetDatePrize();

// #endregion

export default function TimerBlock() {
    return (
        <div className="TimerWrapper flex-column mt-5 flex">
            <h1>Отсчет до розыгрыша</h1>
            <Timer endTime={date} />
        </div>
    );
}