import { Button } from "primereact/button";
import { InputText } from "primereact/inputtext";
import axios from "axios";
import { useState, useRef } from "react";
import LoginModal from "@/App/Modals/Modal/LoginModal/LoginModal.jsx";
import RegisterModal from "@/App/Modals/Modal/RegisterModal/RegisterModal.jsx";
import { Toast } from "primereact/toast";
import "./RegisterPromo.css";

function RegisterPromo() {
    const [promocode, setPromocode] = useState("");
    const [isLoginModalOpen, setIsLoginModalOpen] = useState(false);
    const [isRegisterModalOpen, setIsRegisterModalOpen] = useState(false);
    const toast = useRef(null);

    return (
        <div className="regpromo">
            <Toast
                ref={toast}
                position="top-center" />
            <div className="w-1/4">
                <div>
                    <p className="text-center font-bold text-4xl">
                        Регистрация промокода
                    </p>
                    <InputText placeholder="Промокод" value={promocode} onChange={(e) => setPromocode(e.target.value)} />
                    <Button className="w-1/1 mt-2"
                        label="Зарегистрировать"
                        onClick={() => RegisterPromocode()} />
                    <LoginModal
                        openState={{ value: isLoginModalOpen, set: setIsLoginModalOpen }}
                        navigateToRegisterPage={navigateToRegisterPage} />

                    <RegisterModal
                        openState={{ value: isRegisterModalOpen, set: setIsRegisterModalOpen }}
                        navigateToLoginPage={navigateToLoginPage} />
                </div>
            </div >
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

    async function RegisterPromocode() {
        var token = localStorage.getItem("token");
        if (token === null) {
            navigateToLoginPage();
            return null;
        }
        await axios
            .post(`api/promocode/register`, {
                headers:
                {
                    Authorization: 'Bearer ${token}'
                },
                body:
                {
                    promocode
                }
            })
            .then((response) => {
                toast.current.show({ severity: "success", summary: "Ваш промокод был успешно зарегистрирован!" });
            }).catch((response) => {
                toast.current.show({ severity: "error", summary: "Ошибка регистрации промокода!" });
            });
    }
}

export default RegisterPromo;