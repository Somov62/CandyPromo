import { Card } from 'primereact/card';
import "./Products.css";

function Products() {
    return (
        <div className="products">
            <h1>Продукты</h1>
            <div className="flex gap-3">
                <Card title="Шоколадный"
                    header={() => <img src="./product1.jpg" />}
                    className="md:w-15rem flex-1">
                    <p className="m-0">
                        Шоколадный батончик – это сладкое лакомство, состоящее из шоколадного покрытия и разнообразной начинки. Начинка включает в себя карамель и вафли.
                    </p>
                </Card>
                <Card title="Клубничный"
                    header={() => <img src="./product2.jpg" />}
                    className="md:w-15rem flex-1">
                    <p className="m-0">
                        Клубничный батончик — это вкусный и питательный перекус, который сочетает в себе свежий и сладкий вкус клубники с различными ингредиентами.
                    </p>
                </Card>
                <Card title="Банановый"
                    header={() => <img src="./product3.jpg" />}
                    className="md:w-15rem flex-1">
                    <p className="m-0">
                        Банановый батончик - это питательная закуска, которая сочетает в себе вкус и пользу бананов. Включает в себя кусочки сушеного банана, овсяные хлопья, мед, орехи и другие полезные ингредиенты.
                    </p>
                </Card>
                <Card title="Кокосовый"
                    header={() => <img src="./product4.jpg" />}
                    className="md:w-15rem flex-1">
                    <p className="m-0">
                        Кокосовый батончик - это вкусная и питательная закуска, которая радует своими ароматом и текстурой. Включает в себя сушеную кокосовую стружку, мед, орехи, семена и другие натуральные ингредиенты.
                    </p>
                </Card>
                <Card title="Русский"
                    header={() => <img src="./product5.jpg" />}
                    className="md:w-15rem flex-1">
                    <p className="m-0">
                        Русский батончик — это вкусный и полезный перекус для русского человека. Содержит в себе орехи и мёд.
                    </p>
                </Card>
                <Card title="Яблочный"
                    header={() => <img src="./product6.jpg" />}
                    className="md:w-15rem flex-1">
                    <p className="m-0">
                        Яблочный батончик - это вкусная и полезная закуска, которая сочетает в себе натуральный вкус яблок и других полезных ингредиентов. Содержит сушеные яблоки, овсяные хлопья, мед, орехи и корицу.
                    </p>
                </Card>
            </div>
        </div >
    );
}

export default Products;