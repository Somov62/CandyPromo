import axios, { AxiosResponse } from "axios";
import Cookies from "js-cookie";
import { getClaimsFromJwt, JwtClaims } from "../../../../API/Helpers/jwt"
import "./Header.css"

// #region Classes

interface IDatePrizeResult {
    result: Date;
    errors: any;
    timeGenerated: Date;
}

// #endregion

// #region Methods

function getAccountName(): string {
    const token = Cookies.get("token");
    if (token === undefined)
        return "";
    const claims = getClaimsFromJwt(token);
    return claims["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
}

// #endregion

export default function header() {
    return (
        <div className="flex">
            <div className="header-border flex w-1/4 flex-col">
                <h2>{getAccountName()}</h2>
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
    );
}