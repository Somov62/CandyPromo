import { useState } from 'react';
import './App.css';
import RegisterModal from './Modals/Modal/RegisterModal/RegisterModal';
import {Button} from "primereact/button";
import LoginModal from './Modals/Modal/LoginModal/LoginModal';

function App() {
    const [isModalOpen, setIsModalOpen] = useState(false);

    function openModal() {
        setIsModalOpen(true);
    }

    return (
            <div>
                <Button onClick={openModal}>Открыть модалку</Button>
                <LoginModal
                    isModalOpen={isModalOpen}
                    setIsModalOpen={setIsModalOpen}
                />

            </div>
    );
}

export default App;
