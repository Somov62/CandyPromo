import axios, { AxiosResponse } from "axios";
import Timer from "../../../Components/Timer/Timer";
import './StatusPrize.css'

// #region Classes

interface DatePrizeResult {
    result: Date;
    errors: any;
    timeGenerated: Date;
}

// #endregion

// #region Methods

async function GetDatePrize(): Promise<number> {
    return await axios.get<DatePrizeResult>("/api/Promo/date")
        .then((response) => {
            return new Date(response.data.result).getTime();
        })
        .catch(() => {
            return Date.now();
        });
}

const date = await GetDatePrize();

// #endregion
export default function StatusPrize() {
    return (
        <div className="status-prize-block items-center">
            <h2 className="">Победители пока не определен</h2>
            <h3 className="">Розыгрыш состоится {new Date(date).toLocaleDateString()}</h3>
            <Timer endTime={date} />
        </div>
    )
}