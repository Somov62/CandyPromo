import {StrictMode} from 'react'
import {createRoot} from 'react-dom/client'
import './index.css'
import 'primeicons/primeicons.css';
import "primereact/resources/themes/lara-light-cyan/theme.css";
import 'primeflex/primeflex.css';
import {PrimeReactProvider} from 'primereact/api';
import AppRouter from "@/App/AppRouter.jsx";

createRoot(document.getElementById('root')).render(
    <StrictMode>
        <PrimeReactProvider>
           <AppRouter/>
        </PrimeReactProvider>
    </StrictMode>,
)
