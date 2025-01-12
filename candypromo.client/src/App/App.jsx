import { useEffect, useMemo, useState } from 'react';
import './App.css';
import RegisterModal from './Modals/Modal/RegisterModal/RegisterModal';

function App() {
    const [isModalOpen, setIsModalOpen] = useState(false);

    function openModal() {
        setIsModalOpen(true);
    }

    return (
        <div>
            <button onClick={openModal}>Открыть модалку</button>
            <RegisterModal
                isModalOpen={isModalOpen}
                setIsModalOpen={setIsModalOpen}
            />
        </div>
    );
}

export default App;
