import { useState } from 'react';
import './LoginModal.css';
import {Dialog} from "primereact/dialog";
import {InputText} from "primereact/inputtext";
import { Password } from 'primereact/password';
import { Button } from 'primereact/button';
import {login} from "../../../../API/auth";
/*
    Компонент - модальное окно входа
    Входные параметры:
    - isModalOpen, setIsModalOpen - хук useState(true | false) на отображение модалки.
*/

function LoginModal({ isModalOpen, setIsModalOpen }) {

    const [userlogin, setUserLogin] = useState();
    const [password, setPassword] = useState();

    return (
      <Dialog
        className="login-modal"
        header="Вход"
        visible={isModalOpen}
        style={{ width: "40%" }}
        onHide={() => {
          if (!isModalOpen) return;
          setIsModalOpen(false);
        }}
      >
        <div className="flex flex-column gap-2">
          <label htmlFor="username">Логин</label>
          <InputText
            id="username"
            aria-describedby="username-help"
            value={userlogin}
            onChange={(e) => setUserLogin(e.target.value)}
          />
          {/* <small id="username-help">
            Enter your username to reset your password.
          </small> */}
        </div>
        <div className="flex flex-column gap-2">
          <label htmlFor="password">Пароль</label>
          <Password
            id="password"
            aria-describedby="password-help"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            feedback={false}
          />
          {/* <small id="password-help">Введите пароль</small> */}
        </div>
        <Button
          className="login-button"
          label="Войти"
          onClick={() => login(userlogin, password)}
        />
      </Dialog>
    );
}

export default LoginModal;