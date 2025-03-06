import React from 'react';
import "./Main.css"
import ConditionsSection from "@/App/Pages/Main/Components/ConditionsSection/ConditionsSection.jsx";
import Products from './Components/Products/Products';

const Main = () => {
    return (
        <div className="Main">
            <ConditionsSection/>
            <Products/>
        </div>
    );
};

export default Main;