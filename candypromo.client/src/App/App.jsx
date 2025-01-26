import {useState} from 'react';
import './App.css';
import RegisterModal from './Modals/Modal/RegisterModal/RegisterModal';
import {Button} from "primereact/button";

function App() {
    const [isRegModalOpen, setIsRegModalOpen] = useState(false);

    return (
        <div>
            <Button onClick={() => setIsRegModalOpen(true)}>Открыть модалку</Button>
            <RegisterModal
                isModalOpen={isRegModalOpen}
                setIsModalOpen={setIsRegModalOpen}/>
        </div>
    );
}

export default App;
