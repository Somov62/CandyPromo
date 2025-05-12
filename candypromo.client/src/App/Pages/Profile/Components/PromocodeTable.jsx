import {useEffect, useRef, useState} from 'react';
import './PromocodeTable.css'
import {InputText} from "primereact/inputtext";
import {Toast} from "primereact/toast";
import {Button} from "primereact/button";
import promocodeService from "@/API/Services/promocodeService.js";

const PromocodeTable = () => {
    const toastPromo = useRef(null);
    const [promocode, setPromocode] = useState("");
    const [promocodes, setPromocodes] = useState([]);
    console.log(promocodes);
    useEffect(() => {
        promocodeService.getMyPromocodes().then((data) => {
            setPromocodes(data.data.result)
        })
    }, [])

    return (
        <div className="promocode-table-wrapper">
            <Toast
                ref={toastPromo}
                position="top-center"/>

            <div className="promocode-table">
                <div className="reg-promo-panel">
                    <InputText
                        placeholder="Введите новый промокод"
                        value={promocode}
                        onChange={(e) => setPromocode(e.target.value)}/>
                    <Button
                        className="ml-4"
                        label="Зарегистрировать"
                        onClick={() => RegisterPromocode()}/>
                </div>

                {
                    promocodes.length > 0 ?
                        (
                            <div className="mt-4">

                                <h3>Ваши промокоды</h3>

                                <table>
                                    <tbody>
                                    {promocodes.map(function (row, i) {
                                        return <tr key={i}>
                                            <td>{row.code}</td>
                                            <td>{row.status + ' ' + (row.prizeName ?? '')}</td>
                                        </tr>
                                    })}
                                    </tbody>
                                </table>
                            </div>
                        )
                        : null
                }
            </div>

        </div>
    );

    async function RegisterPromocode() {
        try {
            await promocodeService.register(promocode);
            toastPromo.current.show({severity: "success", summary: "Ваш промокод был успешно зарегистрирован!"});

            promocodeService.getMyPromocodes().then((data) => {
                setPromocodes(data.data.result)
            });
        } catch (error) {
            toastPromo.current.show({
                severity: "error",
                summary: "Ошибка регистрации промокода!",
                detail: error.data.errors[0].reason
            });
        }
    }
};

export default PromocodeTable;