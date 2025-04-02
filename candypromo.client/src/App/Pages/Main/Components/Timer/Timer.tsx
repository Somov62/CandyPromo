import { CountdownCircleTimer } from "react-countdown-circle-timer";
import axios, { AxiosResponse } from "axios";
import "./Timer.css";

// #region Classes

class TimerProps {
    isPlaying: boolean = true;
    size: number = 120;
    strokeWidth: number = 6;
}

interface DatePrizeResult {
    result: Date;
    errors: any;
    timeGenerated: Date;
}

// #endregion

// #region Consts

const minuteSeconds: number = 60;
const hourSeconds: number = 3600;
const daySeconds: number = 86400000;
const timerProps = new TimerProps();
const renderTime = (dimension: string, time: number) => {
    return (
        <div className="time-wrapper">
            <div className="time">{time}</div>
            <div>{dimension}</div>
        </div>
    );
};
const datePrize = await GetDatePrize();
const getTimeSeconds = () => GetRemainingTime().getSeconds() | 0;
const getTimeMinutes = () => GetRemainingTime().getMinutes() | 0;
const getTimeHours = () => GetRemainingTime().getHours() | 0;
const getTimeDays = () => GetRemainingTime().getDay() | 0;

const days: number = Math.ceil(GetRemainingTime().getTime() / daySeconds);
const daysDuration: number = days * daySeconds;

// #endregion

// #region Methods

async function GetDatePrize(): Promise<Date> {
    var result: AxiosResponse<DatePrizeResult> = await axios.get<DatePrizeResult>("/api/Promo/date");
    return new Date(result.data.result);
}

function GetRemainingTime(): Date {
    return new Date(datePrize.getTime() - Date.now());
}

// #endregion

export default function Timer() {
    return (
        <div className="TimerWrapper flex flex-column mt-10">
            <h1>Отсчет до<br></br>
                розыгрыша</h1>
            <div className="Timer">
                <CountdownCircleTimer
                    {...timerProps} colors="#444444"
                    duration={daysDuration}
                    initialRemainingTime={GetRemainingTime().getTime()}>
                    {({ elapsedTime, color }) => (
                        <span style={{ color }}>
                            {renderTime("дней", getTimeDays())}
                        </span>
                    )}
                </CountdownCircleTimer>
                <CountdownCircleTimer
                    {...timerProps} colors="#444444"
                    duration={daySeconds}
                    initialRemainingTime={GetRemainingTime().getTime() % daySeconds}
                    onComplete={(totalElapsedTime) => ({
                        shouldRepeat: GetRemainingTime().getTime() - totalElapsedTime > hourSeconds
                    })}>
                    {({ elapsedTime, color }) => (
                        <span style={{ color }}>
                            {renderTime("часов", getTimeHours())}
                        </span>
                    )}
                </CountdownCircleTimer>
                <CountdownCircleTimer
                    {...timerProps} colors="#444444"
                    duration={hourSeconds}
                    initialRemainingTime={GetRemainingTime().getTime() % hourSeconds}
                    onComplete={(totalElapsedTime) => ({
                        shouldRepeat: GetRemainingTime().getTime() - totalElapsedTime > minuteSeconds
                    })}>
                    {({ elapsedTime, color }) => (
                        <span style={{ color }}>
                            {renderTime("минут", getTimeMinutes())}
                        </span>
                    )}
                </CountdownCircleTimer>
                <CountdownCircleTimer
                    {...timerProps} colors="#444444"
                    duration={minuteSeconds}
                    initialRemainingTime={GetRemainingTime().getTime() % minuteSeconds}
                    onComplete={(totalElapsedTime) => ({
                        shouldRepeat: GetRemainingTime().getTime() - totalElapsedTime > 0
                    })}>
                    {({ elapsedTime, color }) => (
                        <span style={{ color }}>
                            {renderTime("секунд", getTimeSeconds())}
                        </span>
                    )}
                </CountdownCircleTimer>
            </div>
        </div>
    );
}
