import { useState, useRef } from "react";
import "./LoginModal.css";
import { Dialog } from "primereact/dialog";
import { InputText } from "primereact/inputtext";
import { Password } from "primereact/password";
import { Button } from "primereact/button";
import { Toast } from "primereact/toast";
import { TabMenu } from "primereact/tabmenu";
import axios from "axios";

/*
 Компонент - модальное окно входа
 Входные параметры:
 - openState - хук useState(true | false) на отображение модалки.
 */

function LoginModal({ openState, navigateToRegisterPage }) {
    const [selectedTabId, setSelectedTabId] = useState(0);
    const [isLoading, setIsLoading] = useState(false);
    const [userLogin, setUserLogin] = useState("");
    const [userPassword, setUserPassword] = useState("");

    const toast = useRef(null);

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
                    disabled={loading}
                    activeIndex={selectedTabId}
                    onTabChange={(e) => setSelectedTabId(e.index)} />
                <div className="flex flex-column gap-2">
                    <label htmlFor="username">Логин</label>
                    <InputText
                        id="username"
                        aria-describedby="email-help"
                        value={userLogin}
                        onChange={(e) => {
                            setUserLogin(e.target.value);
                            document.getElementById("email-help").innerText = "";
                        }}
                        disabled={isLoading} />
                    <div className="error-help">
                        <label id="email-help" />
                    </div>
                </div>
                <div className="flex flex-column gap-2">
                    <label htmlFor="password">Пароль</label>
                    <Password
                        id="password"
                        aria-describedby="password-help"
                        value={userPassword}
                        onChange={(e) => {
                            setUserPassword(e.target.value);
                            document.getElementById("password-help").innerText = "";
                        }}
                        feedback={false}
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
        document.getElementById("email-help").innerText = document.getElementById("password-help").innerText = "";
        await axios
            .post(`/api/auth/login`, { email: userLogin, password: userPassword })
            .then((response) => {
                const token = response.data;
                localStorage.setItem("token", token);
                console.log(token);
                toast.current.show({
                    severity: "success",
                    summary: "Добро пожаловать",
                    detail: "Вход выполнен успешно!"
                });
                openState(false);
            }).catch((response) => {
                if (response.status === 400) {
                    console.log(response.response.data.errors[0]);
                    response.response.data.errors.forEach(function (error) {
                        document.getElementById(`${error.propertyNames[0].toLowerCase()}-help`).innerText = error.reason;
                    });
                } else {
                    console.log(response.response.data);
                }
                toast.current.show({ severity: "error", summary: "Ошибка авторизации" });
            });
        setIsLoading(false);
    }
}

export default LoginModal;