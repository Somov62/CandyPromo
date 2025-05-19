import './App.css';
import Header from "@/App/Components/Header/Header.jsx";
import {Outlet} from "react-router-dom";
import Footer from "./Components/Footer/Footer.jsx";

function App() {
    return (
        <div className="flex-column flex">
            <Header/>
            <Outlet/>
            <Footer/>
        </div>
    );
}

export default App;
