import {useState, useEffect} from 'react';
import './LoginModal.css';
import {Dialog} from "primereact/dialog";
import {InputText} from "primereact/inputtext";
import {Password} from 'primereact/password';
import {Button} from 'primereact/button';
import axios from "axios";

/*
    Компонент - модальное окно входа
    Входные параметры:
    - openState - хук useState(true | false) на отображение модалки.
*/

function LoginModal({openState, navigateToRegisterPage}) {
    const [isLoading, setIsLoading] = useState(false);

    const [userLogin, setUserLogin] = useState('');
    const [userPassword, setUserPassword] = useState('');

    const [userLoginError, setUserLoginError] = useState('');
    const [userPasswordError, setUserPasswordError] = useState('');

    useEffect(() => {
        if (isLoading) {
            setUserLoginError('');
            setUserPasswordError('');
        }
    }, [isLoading]);

    return (
        <Dialog
            className="login-modal"
            header="Вход"
            visible={openState.value}
            style={{width: "40%"}}
            onHide={() => {
                if (!openState.value) return;
                openState.set(false);
            }}>
            <div className="flex flex-column gap-2">
                <label htmlFor="username">Логин</label>
                <InputText
                    id="username"
                    aria-describedby="username-help"
                    value={userLogin}
                    onChange={(e) => {
                        setUserLogin(e.target.value);
                        setUserLoginError('')
                    }}
                    disabled={isLoading}/>
                <label className="error-help">{userLoginError}</label>
            </div>
            <div className="flex flex-column gap-2">
                <label htmlFor="password">Пароль</label>
                <Password
                    id="password"
                    aria-describedby="password-help"
                    value={userPassword}
                    onChange={(e) => {
                        setUserPassword(e.target.value);
                        setUserPasswordError('')
                    }}
                    feedback={false}
                    disabled={isLoading}/>
                <label className="error-help">{userPasswordError}</label>
            </div>
            <Button
                className="login-button"
                label="Войти"
                loading={isLoading}
                onClick={() => login()}/>
            <Button
                className="link-button"
                label="Создать аккаунт"
                link
                onClick={navigateToRegisterPage}/>
        </Dialog>);

    async function login() {
        setIsLoading(true);
        await axios
            .post(`/api/auth/login`, {email: userLogin, password: userPassword})
            .then((response) => {
                const token = response.data;
                localStorage.setItem("token", token);
                console.log(response.data);
                openState(false);
            }).catch((response) => {
                console.log(response.response.data.errors[0]);
                response.response.data.errors.forEach(function (error) {
                    if (error.propertyName === 'EmailPhone') setUserLoginError(error.reason);
                    if (error.propertyName === 'Password') setUserPasswordError(error.reason);
                });
            });
        setIsLoading(false);
    }
}

export default LoginModal;