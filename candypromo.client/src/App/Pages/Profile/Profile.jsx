import React from 'react';
import TimerBlock from "@/App/Pages/Main/Components/Timer/TimerBlock.tsx";
import Faq from "@/App/Pages/Main/Components/FAQ/FAQ.jsx";
import PromocodeTable from "@/App/Pages/Profile/Components/PromocodeTable.jsx";
import Footer from "@/App/Components/Footer/Footer.jsx";
import Header from "@/App/Pages/Profile/Components/Header.jsx";

const Profile = () => {
    return (
        <div>
            <Header/>
            <TimerBlock/>
            <PromocodeTable/>
            <Faq/>
        </div>
    );
};

export default Profile;