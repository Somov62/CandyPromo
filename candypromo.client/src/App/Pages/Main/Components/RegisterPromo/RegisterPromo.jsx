import { Button } from "primereact/button";
import { InputText } from "primereact/inputtext";

function RegisterPromo() {
    return (
        <div className="products w-1/2 m-auto mt-10">
            <p className="text-center font-bold text-4xl">
                Зарегистрируйся и получи возможность выиграть крутые призы!
            </p>
            <InputText placeholder="Промокод" />
            <Button className="w-1/1 mt-2" label="Зарегистрировать"/>
        </div >
    );
}

export default RegisterPromo;