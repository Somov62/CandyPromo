import { Card } from 'primereact/card';
import "./Prizes.css";
import { Carousel } from 'primereact/carousel';

function Prizes() {

    const prizes = [
        { title: "Планшет", img: "./prizes/prize1.jpg", description: "Этот планшет сочетает в себе элегантный дизайн и мощные возможности. Он идеально подходит для просмотра фильмов, чтения, игр и работы." },
        { title: "Телевизор", img: "./prizes/prize2.jpg", description: "Погрузитесь в мир развлечений с этим современным телевизором, который станет настоящим украшением вашего дома. Его яркий экран с высоким разрешением обеспечит четкость и насыщенность изображений." },
        { title: "Электросамокат", img: "./prizes/prize3.jpg", description: "Откройте для себя свободу передвижения с этим стильным и мощным электросамокатом! Компактный и легкий, он идеально подойдет для города." },
        { title: "Наушники", img: "./prizes/prize4.jpg", description: "Ощутите качественный звук с этими стильными и комфортными наушниками. Они созданы, чтобы погрузить вас в музыку, фильмы или игры, обеспечивая чистый звук и глубокие басы." },
        { title: "Телефон", img: "./prizes/prize5.jpg", description: "Мощный и стильный смартфон, который станет вашим незаменимым помощником в повседневной жизни. Высококачественный дисплей обеспечивает яркость и четкость изображения." },
        { title: "Приставка", img: "./prizes/prize6.jpg", description: "Погрузитесь в увлекательный мир игр с этой мощной игровой приставкой! Она обеспечивает невероятную графику, молниеносную скорость загрузки и доступ к широчайшему ассортименту игр." },
    ]

    const responsiveOptions = [
        {
            breakpoint: '1400px',
            numVisible: 1,
            numScroll: 1
        },

        {
            breakpoint: '800px',
            numVisible: 1,
            numScroll: 1
        }
    ];

    const prizeTemplate = (prize) => {
        return (
            <Card title={prize.title}
                header={() => <img src={prize.img} style={{ width: '100%' }} />}
                className="ml-4 mr-4">
                <p className="m-0">
                    {prize.description}
                </p>
            </Card>
        );
    };

    return (
        <div id="prizes" className="prizes">
            <h1 className="white" >Призы</h1>
            <div className="justify-content-center flex">
                <Carousel value={prizes} numVisible={3} numScroll={3} responsiveOptions={responsiveOptions} itemTemplate={prizeTemplate} />
            </div>
        </div >
    );
}

export default Prizes;