import {useState} from 'react';
import './App.css';
import RegisterModal from './Modals/Modal/RegisterModal/RegisterModal';
import LoginModal from './Modals/Modal/LoginModal/LoginModal';
import {Button} from "primereact/button";

function App() {
    const [isLoginModalOpen, setIsLoginModalOpen] = useState(false);
    const [isRegisterModalOpen, setIsRegisterModalOpen] = useState(false);

    return (
        <div>
            <div>
                <Button onClick={() => setIsLoginModalOpen(true)}>Открыть модалку логина</Button>
                <LoginModal
                    openState={{value: isLoginModalOpen, set: setIsLoginModalOpen }}
                    navigateToRegisterPage={navigateToRegisterPage}
                />
            </div>
            <div>
                <Button onClick={() => setIsRegisterModalOpen(true)}>Открыть модалку регистрации</Button>
                <RegisterModal
                    openState={{value: isRegisterModalOpen, set: setIsRegisterModalOpen }}
                    navigateToLoginPage={navigateToLoginPage}
                />
            </div>
        </div>
    );

    function navigateToLoginPage() {
        setIsRegisterModalOpen(false);
        setIsLoginModalOpen(true);
    }

    function navigateToRegisterPage() {
        setIsLoginModalOpen(false);
        setIsRegisterModalOpen(true);
    }
}

export default App;
