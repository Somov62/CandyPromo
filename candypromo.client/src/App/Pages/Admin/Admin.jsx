import { React, useEffect, useState } from "react";
import Header from "./Components/Header"
import StatusPrize from "./Components/StatusPrize"
import promoService from "../../../API/Services/promoService"
import "./Admin.css"
import PrizesTable from "@/App/Pages/Admin/Components/PrizesTable/PrizesTable.jsx";

const Admin = () => {
    const [active, setActive] = useState(false);
    useEffect(() => {
        const fetchData = async () => {
            try {
                const result = await promoService.active();
                setActive(result.data.result);
            } catch (error) {
                console.log(error);
            }
        }
        fetchData();
    });

    return (
        <div>
            <div style={{ marginInline: "30px" }}>
                <Header />
                {active ? <StatusPrize /> : <PrizesTable />}
            </div>
        </div>
    );
};

export default Admin;