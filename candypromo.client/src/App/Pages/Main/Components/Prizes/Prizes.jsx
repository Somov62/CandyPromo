import { Card } from 'primereact/card';

function Prizes() {
    return (
        <div className="products mt-10">
            <h1>Призы</h1>
            <div className="flex gap-3">
                <Card title="Планшет"
                    header={() => <img src="./prizes/prize1.jpg" />}
                    className="md:w-15rem flex-1">
                    <p className="m-0">
                        Этот планшет сочетает в себе элегантный дизайн и мощные возможности. Он идеально подходит для просмотра фильмов, чтения, игр и работы.
                    </p>
                </Card>
                <Card title="Телевизор"
                    header={() => <img src="./prizes/prize2.jpg" />}
                    className="md:w-15rem flex-1">
                    <p className="m-0">
                        Погрузитесь в мир развлечений с этим современным телевизором, который станет настоящим украшением вашего дома. Его яркий экран с высоким разрешением обеспечит четкость и насыщенность изображений.
                    </p>
                </Card>
                <Card title="Электросамокат"
                    header={() => <img src="./prizes/prize3.jpg" />}
                    className="md:w-15rem flex-1">
                    <p className="m-0">
                        Откройте для себя свободу передвижения с этим стильным и мощным электросамокатом! Компактный и легкий, он идеально подойдет для города.
                    </p>
                </Card>
                <Card title="Наушники"
                    header={() => <img src="./prizes/prize4.jpg" />}
                    className="md:w-15rem flex-1">
                    <p className="m-0">
                        Ощутите качественный звук с этими стильными и комфортными наушниками. Они созданы, чтобы погрузить вас в музыку, фильмы или игры, обеспечивая чистый звук и глубокие басы.
                    </p>
                </Card>
                <Card title="Телефон"
                    header={() => <img src="./prizes/prize5.jpg" />}
                    className="md:w-15rem flex-1">
                    <p className="m-0">
                        Мощный и стильный смартфон, который станет вашим незаменимым помощником в повседневной жизни. Высококачественный дисплей обеспечивает яркость и четкость изображения.
                    </p>
                </Card>
                <Card title="Приставка"
                    header={() => <img src="./prizes/prize6.jpg" />}
                    className="md:w-15rem flex-1">
                    <p className="m-0">
                        Погрузитесь в увлекательный мир игр с этой мощной игровой приставкой! Она обеспечивает невероятную графику, молниеносную скорость загрузки и доступ к широчайшему ассортименту игр.
                    </p>
                </Card>
            </div>
        </div >
    );
}

export default Prizes;