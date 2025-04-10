import axios, { AxiosResponse } from "axios";
import Timer from "../../../Components/Timer/Timer";

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
export default function StatusPrize() {
    return (
        <div className="">
            <h2 className="">Победители пока не определены</h2>
            <h3 className="">Розыгрыш через</h3>
            <Timer endTime={date} />
        </div>
    )
}