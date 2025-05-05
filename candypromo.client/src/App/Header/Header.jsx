import React, {useState} from "react";
import "./Header.css";
import { useNavigate } from "react-router-dom";
import {Menubar} from "primereact/menubar";
import {Button} from "primereact/button";
import LoginModal from "@/App/Modals/Modal/LoginModal/LoginModal.jsx";
import RegisterModal from "@/App/Modals/Modal/RegisterModal/RegisterModal.jsx";
import Cookies from "js-cookie";

const Header = () => {
    const [isLoginModalOpen, setIsLoginModalOpen] = useState(false);
    const [isRegisterModalOpen, setIsRegisterModalOpen] = useState(false);
    const navigate = useNavigate();

    return (
        <div>
            <Menubar
                className="m-2"
                start={(
                    <div className="align-items-center mr-2 flex gap-2">
                        <img
                            alt="logo"
                            src="./vite.svg"
                            height="40"
                            className="mr-2 ml-2"/>

                        <label className="header-label" onClick={() => navigate("/")}>Candy Promo</label>
                    </div>
                )}
                model={
                    [
                        {
                            label: "Условия",
                            command: () => {
                                navigate("/#conditions");
                            }
                        },
                        {
                            label: "Призы",
                            command: () => {
                                navigate("/#prizes");
                            }
                        },
                        {
                            label: "Продукты",
                            command: () => {
                                navigate("/#products");
                            }
                        },
                        {
                            label: "Победители",
                            command: () => {
                                navigate("/#winners");
                            }
                        },
                        {
                            label: "Вопрос-ответ",
                            command: () => {
                                navigate("/#questions");
                            }
                        },
                        {
                            label: "Правила",
                            command: () => {
                                navigate("/#rules");
                            }
                        }
                    ]
                }

                end={(
                    <div className="profile-buttons flex">
                            <Button
                                size="small"
                                icon="pi pi-user"
                                onClick={navigateToProfilePage}>Личный кабинет</Button>

                            <LoginModal
                                openState={{value: isLoginModalOpen, set: setIsLoginModalOpen}}
                                navigateToRegisterPage={navigateToRegisterPage}/>

                            <RegisterModal
                                openState={{value: isRegisterModalOpen, set: setIsRegisterModalOpen}}
                                navigateToLoginPage={navigateToLoginPage}/>
                    </div>
                )}/>
        </div>
    );

    function navigateToLoginPage() {
        setIsRegisterModalOpen(false);
        setIsLoginModalOpen(true);
    }

    function navigateToRegisterPage() {
        setIsLoginModalOpen(false);
        setIsRegisterModalOpen(true);
    }

    function navigateToProfilePage()
    {
        let role = Cookies.get("isAdmin");

        if (role === undefined)
        {
            navigateToLoginPage();
            return;
        }

        role = role.toLowerCase() === "true";

        navigate(role ? "/admin" : "/profile");
    }
};

export default Header;