import { useEffect, useMemo, useState } from 'react';
import './RegisterModal.css';
import Modal from '../Modal';

function RegisterModal({ isModalOpen, setIsModalOpen }) {
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    return (
        <Modal
            canForceClose={true}
            isModalOpen={isModalOpen}
            setIsModalOpen={setIsModalOpen}
        >
            <div>
                <label>Регистрация</label>

                <label>Имя</label>
                <input
                    type='text'
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                />

                <label>Почта</label>
                <input
                    type='text'
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                />
                <label>Пароль</label>
                <input
                    type='text'
                    value={password}
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
    }
}

export default RegisterModal;
