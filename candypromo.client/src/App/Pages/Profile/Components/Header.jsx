import React from 'react';
import { getClaimsFromJwt } from "@/API/Helpers/jwt.js"
import Cookies from "js-cookie";

const Header = () => {
    function exit() {
        localStorage.clear();
        document.cookie.split(";").forEach(cookie => {
            const eqPos = cookie.indexOf("=");
            const name = eqPos > -1 ? cookie.substring(0, eqPos) : cookie;
            document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
        });
    }

    function getAccountName() {
        const token = Cookies.get("token");
        if (token === undefined)
            return "";
        const claims = getClaimsFromJwt(token);
        return claims["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
    }

    return (
        <div className="header-admin flex justify-content-between">
            <div className="header-border flex w-1/4 flex-col">
                <h2>{getAccountName()}</h2>
            </div>
            <a className="header-border flex w-1/6 cursor-pointer flex-col bg-red-100" onClick={exit} href="/">
                <h2>Выйти</h2>
            </a>
        </div>
    );
};

export default Header;