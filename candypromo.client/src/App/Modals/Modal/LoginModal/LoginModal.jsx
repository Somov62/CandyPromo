/* eslint-disable react/jsx-key */
import { useState, useRef } from "react";
import "./LoginModal.css";
import { Dialog } from "primereact/dialog";
import { InputText } from "primereact/inputtext";
import { Password } from "primereact/password";
import { Button } from "primereact/button";
import { Toast } from "primereact/toast";
import { InputMask } from 'primereact/inputmask';
import { TabMenu } from "primereact/tabmenu";
import axios from "axios";
import Cookies from "js-cookie";
import {useNavigate} from "react-router-dom";

/*
 Компонент - модальное окно входа
 Входные параметры:
 - openState - хук useState(true | false) на отображение модалки.
 */

function LoginModal({ openState, navigateToRegisterPage }) {
    const [selectedTabId, setSelectedTabId] = useState(0);
    const [isLoading, setIsLoading] = useState(false);
    const [userPhone, setUserPhone] = useState("");
    const [userEmail, setUserEmail] = useState("");
    const [userPassword, setUserPassword] = useState("");
    const navigate = useNavigate();
    const toast = useRef(null);

    const handleKeyDown = async (event) => {
        if (event.key === "Enter")
            await login();
    };

    return (
        <div>
            <Toast
                ref={toast}
                position="top-center" />
            <Dialog
                className="login-modal"
                header="Вход"
                visible={openState.value}
                style={{ width: "50vw" }}
                onHide={() => {
                    if (!openState.value) return;
                    openState.set(false);
                }}>
                <TabMenu
                    className="TabMenu mb-3"
                    model={[{ label: "Телефон" }, { label: "Электронная почта" }]}
                    disabled={isLoading}
                    activeIndex={selectedTabId}
                    onTabChange={(e) => {
                        setSelectedTabId(e.index);
                        setUserEmail("");
                        setUserPhone("");
                    }} />
                <div className="flex flex-column">
                    {selectedTabId === 0 ? ([
                        <InputMask
                            value={userPhone}
                            disabled={isLoading}
                            onChange={(e) => {
                                setUserPhone(e.target.value);
                                document.getElementById('phone-help').innerText = '';
                            }}
                            onKeyDown={handleKeyDown}
                            placeholder="Введите телефон"
                            mask="+7-(999)-999-99-99" />,
                        <div className="error-help">
                            <label id="phone-help" />
                        </div>
                    ]) : ([
                        <InputText
                            placeholder="Введите email"
                            disabled={isLoading}
                            type="email"
                            value={userEmail}
                            onKeyDown={handleKeyDown}
                            onChange={(e) => setUserEmail(e.target.value)}
                            id="email" />,
                        <div className="error-help">
                            <label id="email-help" />
                        </div>
                    ])}
                </div>
                <div className="flex flex-column">
                    <Password
                        id="password"
                        aria-describedby="password-help"
                        value={userPassword}
                        onKeyDown={handleKeyDown}
                        onChange={(e) => {
                            setUserPassword(e.target.value);
                            document.getElementById("password-help").innerText = "";
                        }}
                        feedback={false}
                        placeholder="Введите пароль"
                        disabled={isLoading} />
                    <div className="error-help">
                        <label id="password-help" />
                    </div>
                </div>
                <Button
                    className="login-button"
                    label="Войти"
                    loading={isLoading}
                    onClick={() => login()} />
                <Button
                    className="link-button"
                    label="Создать аккаунт"
                    link
                    onClick={navigateToRegisterPage}
                    disabled={isLoading} />
            </Dialog>
        </div>);

    async function login() {
        setIsLoading(true);
        if (selectedTabId === 0) {
            document.getElementById('phone-help').innerText = '';
        } else if (selectedTabId === 1) {
            document.getElementById('email-help').innerText = '';
        }
        document.getElementById('password-help').innerText = '';

        await axios
            .post(`api/auth/login`, { email: userEmail, phone: userPhone, password: userPassword })
            .then((response) => {
                const token = response.data;
                localStorage.setItem("token", token);
                console.log(response.data);
                openState.set(false);

                const role = Cookies.get("isAdmin").toLowerCase() === 'true';

                navigate(role ? "/admin" : "/profile");

            }).catch((response) => {
                if (response.status === 400) {
                    console.log(response.errors[0]);
                    response.errors.forEach(e => {
                        e.propertyNames.forEach(pn => {
                            const element = document.getElementById(`${pn.toLowerCase()}-help`);
                            if (element) {
                                element.innerText = e.reason;
                            }
                        });
                    });
                } else {
                    console.log(response);
                }
                toast.current.show({ severity: "error", summary: "Ошибка авторизации" });
            });
        setIsLoading(false);
    }
}

export default LoginModal;