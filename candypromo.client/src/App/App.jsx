import { useState } from 'react';
import './App.css';
import RegisterModal from './Modals/Modal/RegisterModal/RegisterModal';
import {Button} from "primereact/button";

function App() {
    const [isModalOpen, setIsModalOpen] = useState(false);

    function openModal() {
        setIsModalOpen(true);
    }

    return (
            <div>
                <Button onClick={openModal}>Открыть модалку</Button>
                <RegisterModal
                    isModalOpen={isModalOpen}
                    setIsModalOpen={setIsModalOpen}
                />

            </div>
    );
}

export default App;
