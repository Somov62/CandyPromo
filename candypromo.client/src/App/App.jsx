import './App.css';
import Header from "@/App/Components/Header/Header.jsx";
import {Outlet} from "react-router-dom";
import Footer from "@/App/Components/Footer/Footer.jsx";

function App() {
    return (
        <div className="flex flex-column">
            <Header/>
            <Outlet/>
            <Footer/>
        </div>
    );
}

export default App;
