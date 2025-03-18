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
    const toastPromo = useRef(null);

    return (
        <div>
            <Toast
                ref={toastPromo}
                position="top-center" />

            <LoginModal
                openState={{ value: isLoginModalOpen, set: setIsLoginModalOpen }}
                navigateToRegisterPage={navigateToRegisterPage} />

            <RegisterModal
                openState={{ value: isRegisterModalOpen, set: setIsRegisterModalOpen }}
                navigateToLoginPage={navigateToLoginPage} />

            <div className="regpromo">
                <div className="w-1/4">
                    <div>
                        <p className="text-center font-bold text-4xl">
                            Регистрация промокода
                        </p>
                        <InputText placeholder="Промокод" value={promocode} onChange={(e) => setPromocode(e.target.value)} />
                        <Button className="w-1/1 mt-2"
                            label="Зарегистрировать"
                            onClick={() => RegisterPromocode()} />
                    </div>
                </div >
            </div>
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
                toastPromo.current.show({ severity: "success", summary: "Ваш промокод был успешно зарегистрирован!" });
            }).catch((response) => {
                toastPromo.current.show({ severity: "error", summary: "Ошибка регистрации промокода!" });
            });
    }
}

export default RegisterPromo;