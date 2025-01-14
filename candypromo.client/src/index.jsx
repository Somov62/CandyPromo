import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App/App.jsx'
import "primereact/resources/themes/lara-light-cyan/theme.css";
import 'primeflex/primeflex.css';
import { PrimeReactProvider } from 'primereact/api';

createRoot(document.getElementById('root')).render(
  <StrictMode>
      <PrimeReactProvider>
          <App />
     </PrimeReactProvider>
</StrictMode>,
)