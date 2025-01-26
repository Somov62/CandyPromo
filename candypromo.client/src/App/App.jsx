import {useState} from 'react';
import './App.css';
import RegisterModal from './Modals/Modal/RegisterModal/RegisterModal';
import {Button} from "primereact/button";
import LoginModal from './Modals/Modal/LoginModal/LoginModal';

function App() {
    const [isLoginModalOpen, setIsLoginModalOpen] = useState(false);
    const [isRegisterModalOpen, setIsRegisterModalOpen] = useState(false);

    return (
        <div>
            <div>
                <Button onClick={setIsLoginModalOpen(true)}>Открыть модалку логина</Button>
                <LoginModal
                    isModalOpen={isLoginModalOpen}
                    setIsModalOpen={setIsLoginModalOpen}
                />
            </div>
            <div>
                <Button onClick={setIsRegisterModalOpen(true)}>Открыть модалку регистрации</Button>
                <RegisterModal
                    isModalOpen={isRegisterModalOpen}
                    setIsModalOpen={setIsRegisterModalOpen}
                />
            </div>
        </div>
    );
}

export default App;
