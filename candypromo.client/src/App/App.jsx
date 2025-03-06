import './App.css';
import Header from "@/App/Header/Header.jsx";
import {Outlet} from "react-router-dom";

function App() {
    return (
        <div className="flex flex-column">
            <Header/>
            <Outlet/>
        </div>
    );
}

export default App;
