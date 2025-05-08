import axios, { AxiosResponse } from "axios";
import { useState, useEffect } from "react";
import Cookies from "js-cookie";
import { getClaimsFromJwt, JwtClaims } from "../../../../API/Helpers/jwt"
import promocodeServie from "../../../../API/Services/promocodeService"
/*import { useNavigate } from "react-router-dom";*/
import "./Header.css"

export default function header() {

    useEffect(() => {
        const fetchData = async () => {
            await getCountPromocode();
        }
        fetchData();
    });

    // #region Classes

    interface IDatePrizeResult {
        result: Date;
        errors: any;
        timeGenerated: Date;
    }

    // #endregion

    // #region Variables

    const [countPromoText, setCountPromoText] = useState("");

    // #endregion

    // #region Methods

    function getAccountName(): string {
        const token = Cookies.get("token");
        if (token === undefined)
            return "";
        const claims = getClaimsFromJwt(token);
        return claims["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
    }

    async function getCountPromocode() {
        try {
            const result = await promocodeServie.getPromocodesCount();
            setCountPromoText(`Зарегано промо: ${result.data.result.registersCount} из ${result.data.result.totalCount}`);
        } catch (error) {
            console.log(error);
        }
    }

    function exit() {
        localStorage.clear();
        document.cookie.split(";").forEach(cookie => {
            const eqPos = cookie.indexOf("=");
            const name = eqPos > -1 ? cookie.substring(0, eqPos) : cookie;
            document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
        });
    }

    // #endregion

    return (
        <div className="flex">
            <div className="header-border flex w-1/4 flex-col">
                <h2>{getAccountName()}</h2>
            </div>
            <div className="header-border header-promo flex w-full flex-col">
                <h2>{countPromoText}</h2>
            </div>
            <a className="header-border flex w-1/6 cursor-pointer flex-col bg-red-100" onClick={exit} href="/">
                <h2>Выйти</h2>
            </a>
        </div>
    );
}