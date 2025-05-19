import "./ConditionsSection.css"
import {Card} from 'primereact/card';

const ConditionsSection = () => {
    return (
        <div className="ConditionsSection" id="conditions">
                <h1 className="white">Условия</h1>
                <div className="justify-content-center flex flex-row gap-4">
                    <Card
                        title="1. Покупайте"
                        className="card md:w-25rem"
                        header={
                            (
                                <img
                                    alt="card"
                                    src="./candyworld.png"/>
                            )
                        }>
                        <p className="m-0">Покупайте батончики Candy в промоупаковке </p>
                    </Card>
                    <Card
                        title="2. Регистрируйте"
                        className="card md:w-25rem"
                        header={
                            (
                                <img
                                    alt="card"
                                    src="./promocode.png"
                                    />
                            )
                        }>
                        <p className="m-0">Регистрируйте коды с упаковки </p>
                    </Card>
                    <Card
                        title="3. Участвуйте"
                        className="card md:w-25rem"
                        header={
                            (
                                <img
                                    alt="card"
                                    src="./prizes.jpg"
                                    style={{height: '390px'}}/>
                            )
                        }>
                        <p className="m-0">Участвуйте в розыгрыше призов каждый месяц </p>
                    </Card>
                </div>
            </div>
    );
};

export default ConditionsSection;