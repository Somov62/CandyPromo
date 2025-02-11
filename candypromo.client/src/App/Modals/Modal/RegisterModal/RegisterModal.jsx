import React, { useRef, useState } from 'react';
import './RegisterModal.css';
import { Dialog } from "primereact/dialog";
import { InputText } from "primereact/inputtext";
import { TabMenu } from 'primereact/tabmenu';
import { Password } from 'primereact/password';
import { InputMask } from 'primereact/inputmask';
import { Button } from 'primereact/button';
import { Toast } from 'primereact/toast';
import axios from "axios";
import Cookies from "js-cookie";
import {useNavigate} from "react-router-dom";

/*
    Компонент - модальное окно регистрации
    Входные параметры:
    - openState - хук useState(true | false) на отображение модалки.
*/

function RegisterModal({ openState, navigateToLoginPage }) {

    const [selectedTabId, setSelectedTabId] = useState(0);
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [phone, setPhone] = useState('');
    const [password, setPassword] = useState('');
    const toast = useRef(null);
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();

    return (
        <div>
            <Toast
                ref={toast}
                position="top-center" />
            <Dialog
                header="Регистрация"
                visible={openState.value}
                style={{ width: '50vw' }}
                onHide={() => {
                    if (!openState.value) return;
                    openState.set(false)
                }}>

                <div className="inputs">
                    <TabMenu
                        className="TabMenu mb-3"
                        model={[{ label: 'Телефон' }, { label: 'Электронная почта' }]}
                        disabled={loading}
                        activeIndex={selectedTabId}
                        onTabChange={(e) => setSelectedTabId(e.index)} />

                    <InputText
                        type="name"
                        area-describedby="name-help"
                        disabled={loading}
                        value={name}
                        onChange={(e) => {
                            setName(e.target.value);
                            document.getElementById('name-help').innerText = '';
                        }}
                        placeholder="Введите ФИО полностью" />
                    <div className="error-help">
                        <label id="name-help" />
                    </div>

                    {selectedTabId === 0 ? ([
                        <InputMask
                            value={phone}
                            disabled={loading}
                            onChange={(e) => {
                                setPhone(e.target.value);
                                document.getElementById('phone-help').innerText = '';
                            }}
                            placeholder="Введите телефон"
                            mask="+7-(999)-999-99-99" />,
                        <div className="error-help">
                            <label id="phone-help" />
                        </div>

                    ]) : ([
                        <InputText
                            placeholder="Введите email"
                            disabled={loading}
                            type="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            id="email" />,
                        <div className="error-help">
                            <label id="email-help" />
                        </div>
                    ])}

                    <Password
                        inputId="password"
                        value={password}
                        disabled={loading}
                        onChange={(e) => setPassword(e.target.value)}
                        feedback={false}
                        placeholder="Придумайте пароль"
                        className="password-input"
                        toggleMask />
                    <div className="error-help">
                        <label id="password-help" />
                    </div>

                    <Button
                        label="Создать аккаунт"
                        loading={loading}
                        onClick={register} />

                    <Button
                        label="У меня уже есть аккаунт"
                        disabled={loading}
                        link
                        onClick={navigateToLoginPage}
                        className="link-btn mt-3" />
                </div>

            </Dialog>
        </div>
    );

    async function register() {
        setLoading(true);

        document.getElementById('name-help').innerText = '';
        if (selectedTabId === 0) {
            document.getElementById('phone-help').innerText = '';
        } else if (selectedTabId === 1) {
            document.getElementById('email-help').innerText = '';
        }
        document.getElementById('password-help').innerText = '';

        await axios.post('api/auth/register', { name, email, phone, password })
            .then((response) => {
                const token = response.data;
                localStorage.setItem("token", token);
                console.log(token);
                toast.current.show({
                    severity: 'success',
                    summary: 'Добро пожаловать',
                    detail: 'Регистрация завершена успешно!'
                });
                openState.set(false);

                const role = Cookies.get("role");

                if (role === "admin")
                    navigate("/admin");

                if (role === "user")
                    navigate("/profile")
            })
            .catch((error) => {
                console.log(error);
                let detail
                if (error.status === 400) {
                    error.response.data.errors.forEach(e => {
                        e.propertyNames.forEach(pn => {
                            const element = document.getElementById(`${pn.toLowerCase()}-help`);
                            if (element) {
                                element.innerText = e.reason;
                            }
                        });
                    });
                    detail = 'Неверные данные';
                } else if (error.status === 500) {
                    detail = error.Error;
                } else {
                    detail = error.message;
                }
                toast.current.show({ severity: 'error', summary: 'Ошибка регистрации', detail: detail });
            });
        setLoading(false);
    }
}

export default RegisterModal;