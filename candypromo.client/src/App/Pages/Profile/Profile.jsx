import React from 'react';
import Timer from "@/App/Pages/Main/Components/Timer/Timer.js";
import Faq from "@/App/Pages/Main/Components/FAQ/FAQ.jsx";
import PromocodeTable from "@/App/Pages/Profile/Components/PromocodeTable.jsx";
import Footer from "@/App/Pages/Main/Components/Footer/Footer.jsx";

const Profile = () => {
    return (
        <div>
            Профиль

            <Timer/>
            <PromocodeTable/>
            <Faq/>
            <Footer/>
        </div>
    );
};

export default Profile;