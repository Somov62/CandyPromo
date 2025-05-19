import "./Main.css"
import ConditionsSection from "@/App/Pages/Main/Components/ConditionsSection/ConditionsSection.jsx";
import Products from './Components/Products/Products';
import Timer from "./Components/Timer/TimerBlock.tsx";
import Prizes from './Components/Prizes/Prizes';
import RegisterPromo from "./Components/RegisterPromo/RegisterPromo";
import Footer from "@/App/Components/Footer/Footer.jsx";
import Faq from "./Components/FAQ/FAQ.jsx";

const Main = () => {
    return (
        <div className="Main">
            <ConditionsSection />
            <RegisterPromo />
            <Products />
            <Timer />
            <Prizes />
            <Faq/>
        </div>
    );
};

export default Main;