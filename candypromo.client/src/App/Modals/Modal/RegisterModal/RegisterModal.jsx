import { useRef, useState } from 'react';
import './RegisterModal.css';
import { Dialog } from "primereact/dialog";
import { InputText } from "primereact/inputtext";
import { TabMenu } from 'primereact/tabmenu';
import { Password } from 'primereact/password';
import { InputMask } from 'primereact/inputmask';
import { Button } from 'primereact/button';
import { Toast } from 'primereact/toast';
import Cookies from "js-cookie";
import {useNavigate} from "react-router-dom";
import authService from "@/API/Services/authService.js";
import showErrorInFields from "@/API/Helpers/showErrorInFieldsHelper.js";
import getErrorMessage from "@/API/Helpers/getErrorMessageHelper.js";

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
                            key="1"
                            value={phone}
                            disabled={loading}
                            onChange={(e) => {
                                setPhone(e.target.value);
                                document.getElementById('phone-help').innerText = '';
                            }}
                            placeholder="Введите телефон"
                            mask="+7-(999)-999-99-99" />,
                        <div key="3" className="error-help">
                            <label id="phone-help" />
                        </div>

                    ]) : ([
                        <InputText
                            key="2"
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

        try {
            const response = await authService.register(name, email, phone, password)

            const token = response.data.result.token;
            localStorage.setItem("token", token);
            console.log(token);
            toast.current.show({
                severity: 'success',
                summary: 'Добро пожаловать',
                detail: 'Регистрация завершена успешно!'
            });
            openState.set(false);

            const role = Cookies.get("isAdmin").toLowerCase() === 'true'

            navigate(role ? "/admin" : "/profile");
        }
        catch (error) {
            showErrorInFields(error)
            const message = getErrorMessage(error)
            toast.current.show({ severity: 'error', summary: 'Ошибка регистрации', detail: message });
        }
        finally {
            setLoading(false);
        }
    }
}

export default RegisterModal;