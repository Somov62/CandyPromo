import { useState } from 'react';
import './RegisterModal.css';
import {Dialog} from "primereact/dialog";
import {InputText} from "primereact/inputtext";
/*
    Компонент - модальное окно регистрации
    Входные параметры:
    - isModalOpen, setIsModalOpen - хук useState(true | false) на отображение модалки.
*/

function RegisterModal({ isModalOpen, setIsModalOpen }) {

    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [phone, setPhone] = useState('');
    const [password, setPassword] = useState('');

    return (
        <Dialog header="Регистрация" visible={isModalOpen} style={{width: '50vw'}} onHide={() => {
            if (!isModalOpen) return;
            setIsModalOpen(false)}}>
                <div className="flex flex-column gap-2">
                    <label htmlFor="username">Username</label>
                    <InputText id="username" aria-describedby="username-help"/>
                    <small id="username-help">
                        Enter your username to reset your password.
                    </small>
                </div>
        </Dialog>
    );

    async function register() {
        const response = await fetch('http://localhost:5249/auth/register', {
            // Метод, если не указывать, будет использоваться GET
            method: 'POST',
            // Заголовок запроса
            headers: {'Content-Type': 'application/json'},
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
