import promoService from "../../../../../API/Services/promoService";
import Timer from "../../../../Components/Timer/Timer";
import "./TimerBlock.css";

// #region Methods

async function getDatePrize(): Promise<number> {
    try {
        return await promoService.getEndingDate();
    } catch (error) {
        return new Date();
    }
}

const datePrize = await getDatePrize();

// #endregion

export default function TimerBlock() {
    return (
        <div className="TimerWrapper flex-column mt-5 mb-15 flex">
            <h1>Отсчет до розыгрыша</h1>
            <Timer endTime={datePrize} />
        </div>
    );
}