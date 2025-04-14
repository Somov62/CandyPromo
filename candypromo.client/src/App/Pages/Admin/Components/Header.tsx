import axios, { AxiosResponse } from "axios";
import { jwtDecode } from 'jwt-decode';
import Cookies from "js-cookie";
import './Header.css'

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

function GetAccountName(): string {
    var token = Cookies.get("token");
    if (token === undefined)
        return "";
    var test = jwtDecode(token);
    return jwtDecode(token).name;
}

// #endregion

export default function Header() {
    return (
        <div className="header-block flex">
            <div className="header-border flex w-1/4 flex-col">
                <h2>{GetAccountName()}</h2>
            </div>
            <div className="header-border header-promo flex w-full flex-col">
                <h2>Зарегано промо: 10000 из 15000</h2>
            </div>
            <div className="header-border flex w-1/6 flex-col bg-green-100">
                <h2>Активно</h2>
            </div>
            <a className="header-border flex w-1/6 flex-col bg-red-100">
                <h2>Выйти</h2>
            </a>
        </div>
    )
}