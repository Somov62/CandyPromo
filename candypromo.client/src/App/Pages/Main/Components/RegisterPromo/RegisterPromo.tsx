import { Button } from "primereact/button";
import { InputText } from "primereact/inputtext";
import axios from "axios";
import { useState, useRef } from "react";
/*import LoginModal from "@/App/Modals/Modal/LoginModal/LoginModal.jsx";*/
import LoginModal from "@/App/Modals/Modal/LoginModal/LoginModal.jsx"
import RegisterModal from "@/App/Modals/Modal/RegisterModal/RegisterModal.jsx";
import { Toast } from "primereact/toast";
import Cookies from "js-cookie";
import "./RegisterPromo.css";
import promocodeService from "../../../../../API/Services/promocodeService";

function registerPromo() {
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
                        <p className="text-center text-4xl font-bold">
                            Регистрация промокода
                        </p>
                        <InputText placeholder="Промокод" value={promocode} onChange={(e) => setPromocode(e.target.value)} />
                        <Button className="mt-2 w-1/1"
                            label="Зарегистрировать"
                            onClick={() => registerPromocode()} />
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

    async function registerPromocode() {
        const token = Cookies.get("token");
        if (token === undefined) {
            navigateToLoginPage();
            return null;
        }

        try {
            await promocodeService.register(promocode);
            toastPromo.current.show({ severity: "success", summary: "Ваш промокод был успешно зарегистрирован!" });
        }
        catch (error) {
            toastPromo.current.show({ severity: "error", summary: "Ошибка регистрации промокода!", detail: error.data.errors[0].reason });
        }
    }
}

export default registerPromo;