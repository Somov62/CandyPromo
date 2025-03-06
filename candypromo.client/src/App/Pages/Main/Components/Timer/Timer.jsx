import React from "react";
import {CountdownCircleTimer} from "react-countdown-circle-timer";
import "./Timer.css";

const minuteSeconds = 60;
const hourSeconds = 3600;
const daySeconds = 86400;

const timerProps = {
    isPlaying: true,
    size: 120,
    strokeWidth: 6
};

const renderTime = (dimension, time) => {
    return (
        <div className="time-wrapper">
            <div className="time">{time}</div>
            <div>{dimension}</div>
        </div>
    );
};

const getTimeSeconds = (time) => (minuteSeconds - time) | 0;
const getTimeMinutes = (time) => ((time % hourSeconds) / minuteSeconds) | 0;
const getTimeHours = (time) => ((time % daySeconds) / hourSeconds) | 0;
const getTimeDays = (time) => (time / daySeconds) | 0;

export default function Timer() {
    const startTime = Date.now() / 1000; // use UNIX timestamp in seconds
    const endTime = startTime + 243248; // use UNIX timestamp in seconds

    const remainingTime = endTime - startTime;
    const days = Math.ceil(remainingTime / daySeconds);
    const daysDuration = days * daySeconds;

    return (
        <div className="TimerWrapper flex flex-column">
            <h1>Отсчет до<br></br>
                розыгрыша</h1>
            <div className="Timer">
                <CountdownCircleTimer
                    {...timerProps} colors="#444444"
                    duration={daysDuration}
                    initialRemainingTime={remainingTime}>
                    {({elapsedTime, color}) => (
                        <span style={{color}}>
                    {renderTime("дней", getTimeDays(daysDuration - elapsedTime))}
                  </span>
                    )}
                </CountdownCircleTimer>
                <CountdownCircleTimer
                    {...timerProps} colors="#444444"
                    duration={daySeconds}
                    initialRemainingTime={remainingTime % daySeconds}
                    onComplete={(totalElapsedTime) => ({
                        shouldRepeat: remainingTime - totalElapsedTime > hourSeconds
                    })}>
                    {({elapsedTime, color}) => (
                        <span style={{color}}>
                    {renderTime("часов", getTimeHours(daySeconds - elapsedTime))}
                  </span>
                    )}
                </CountdownCircleTimer>
                <CountdownCircleTimer
                    {...timerProps} colors="#444444"
                    duration={hourSeconds}
                    initialRemainingTime={remainingTime % hourSeconds}
                    onComplete={(totalElapsedTime) => ({
                        shouldRepeat: remainingTime - totalElapsedTime > minuteSeconds
                    })}>
                    {({elapsedTime, color}) => (
                        <span style={{color}}>
                    {renderTime("минут", getTimeMinutes(hourSeconds - elapsedTime))}
                  </span>
                    )}
                </CountdownCircleTimer>
                <CountdownCircleTimer
                    {...timerProps} colors="#444444"
                    duration={minuteSeconds}
                    initialRemainingTime={remainingTime % minuteSeconds}
                    onComplete={(totalElapsedTime) => ({
                        shouldRepeat: remainingTime - totalElapsedTime > 0
                    })}>
                    {({elapsedTime, color}) => (
                        <span style={{color}}>
                    {renderTime("секунд", getTimeSeconds(elapsedTime))}
                  </span>
                    )}
                </CountdownCircleTimer>
            </div>
        </div>
    );
}
