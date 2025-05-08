import React from 'react';
import Header from "./Components/Header"
import StatusPrize from "./Components/StatusPrize"
import './Admin.css'
import PrizesTable from "@/App/Pages/Admin/Components/PrizesTable/PrizesTable.jsx";

const Admin = () => {
    return (
        <div>
            <Header />
            <StatusPrize />
            <PrizesTable />
        </div>
    );
};

export default Admin;