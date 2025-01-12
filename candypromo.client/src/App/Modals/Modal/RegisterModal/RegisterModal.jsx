import { useState } from 'react';
import './RegisterModal.css';
import Modal from '../Modal';

/*
    Компонент - модальное окно регистрации
    Входные параметры:
    - isModalOpen, setIsModalOpen - хук useState(true | false) на отображение модалки.
*/

function RegisterModal({ isModalOpen, setIsModalOpen }) {

    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    return (
        <Modal
            canForceClose={true}
            isModalOpen={isModalOpen}
            setIsModalOpen={setIsModalOpen}
            header='Регистрация'
        >
            <div>
                <label>Имя
                    <input
                        type='text'
                        value={name}
                        placeholder='Введите имя'
                        onChange={(e) => setName(e.target.value)}
                    />
                </label>

                <label>Почта</label>
                    <input
                        type='text'
                        value={email}
                        placeholder='Введите email'
                        onChange={(e) => setEmail(e.target.value)}
                    />
                <label>Пароль</label>
                    <input
                        type='text'
                        value={password}
                        placeholder='Придумайте пароль'
                        onChange={(e) => setPassword(e.target.value)}
                    />
                <button onClick={register}>Зарегистрироваться</button>
            </div>
        </Modal>
    );

    async function register() {
        const response = await fetch('http://localhost:5249/auth/register', {
            // Метод, если не указывать, будет использоваться GET
            method: 'POST',
            // Заголовок запроса
            headers: { 'Content-Type': 'application/json' },
            // Данные
            body: JSON.stringify({name, email, password}),
        });
        console.log(response);
        if (response.ok) {
            const data = await response.json();
            console.log(data);
        }
        else {
            setIsModalOpen(false);
        }
    }
}

export default RegisterModal;
