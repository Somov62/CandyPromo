import "./Main.css"
import ConditionsSection from "@/App/Pages/Main/Components/ConditionsSection/ConditionsSection.jsx";
import Products from './Components/Products/Products';
import Timer from "@/App/Pages/Main/Components/Timer/Timer.jsx";
import RegisterPromocode from "@/App/Pages/Main/Components/RegisterPromocode/RegisterPromocode.jsx";
import Prizes from './Components/Prizes/Prizes';

const Main = () => {
    return (
        <div className="Main">
            <ConditionsSection />
            <Products />
            <Timer />
            <RegisterPromocode />
            <Prizes />
        </div>
    );
};

export default Main;