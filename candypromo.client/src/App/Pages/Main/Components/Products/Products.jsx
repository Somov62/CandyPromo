import { useState, useRef } from "react";
import { Card } from 'primereact/card';
import "./Products.css";

function Products() {
    return (
        <div className="products">
            <Card title="Шоколадный"
                header={() => <img src="./product1.jpg" />}
                className="md:w-15rem">
                <p className="m-0">
                    Шоколадный батончик – это сладкое лакомство, состоящее из шоколадного покрытия и разнообразной начинки. Начинка включает в себя карамель и вафли.
                </p>
            </Card>
            <Card title="Клубничный"
                header={() => <img src="./product2.jpg" />}
                className="md:w-15rem">
                <p className="m-0">
                    Клубничный батончик — это вкусный и питательный перекус, который сочетает в себе свежий и сладкий вкус клубники с различными ингредиентами.
                </p>
            </Card>
            <Card title="Банановый"
                header={() => <img src="./product3.jpg" />}
                className="md:w-15rem">
                <p className="m-0">
                    Клубничный батончик — это вкусный и питательный перекус, который сочетает в себе свежий и сладкий вкус клубники с различными ингредиентами.
                </p>
            </Card>
            <Card title="Кокосовый"
                header={() => <img src="./product4.jpg" />}
                className="md:w-15rem">
                <p className="m-0">
                    Клубничный батончик — это вкусный и питательный перекус, который сочетает в себе свежий и сладкий вкус клубники с различными ингредиентами.
                </p>
            </Card>
            <Card title="Русский"
                header={() => <img src="./product5.jpg" />}
                className="md:w-15rem">
                <p className="m-0">
                    Клубничный батончик — это вкусный и питательный перекус, который сочетает в себе свежий и сладкий вкус клубники с различными ингредиентами.
                </p>
            </Card>
            <Card title="Яблочный"
                header={() => <img src="./product6.jpg" />}
                className="md:w-15rem">
                <p className="m-0">
                    Клубничный батончик — это вкусный и питательный перекус, который сочетает в себе свежий и сладкий вкус клубники с различными ингредиентами.
                </p>
            </Card>
        </div>
    );
}

export default Products;