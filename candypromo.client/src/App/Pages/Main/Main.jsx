import "./Main.css"
import ConditionsSection from "@/App/Pages/Main/Components/ConditionsSection/ConditionsSection.jsx";
import Products from './Components/Products/Products';
import Timer from "@/App/Pages/Main/Components/Timer/TimerBlock.tsx";
import Prizes from './Components/Prizes/Prizes';
import RegisterPromo from "./Components/RegisterPromo/RegisterPromo";

const Main = () => {
    return (
        <div className="Main">
            <ConditionsSection />
            <RegisterPromo />
            <Products />
            <Timer />
            <Prizes />
        </div>
    );
};

export default Main;