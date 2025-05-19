import {useEffect, useState, useRef} from 'react';
import prizesService from "@/API/Services/prizesService.js";
import {DataTable} from "primereact/datatable";
import {Column} from "primereact/column";
import {Button} from "primereact/button";
import {Dialog} from 'primereact/dialog';
import {Dropdown} from 'primereact/dropdown';
import {Toast} from "primereact/toast";

const PrizesTable = ({isEnded}) => {

    const [prizesInfo, setPrizesInfo] = useState([]);

    useEffect(() => {
        prizesService.getPrizesFullDetails().then(data => setPrizesInfo(data.data.result))
        prizesService.getPrizeStatuses().then(data => setStatuses(data.data.result))
    }, [])

    const [isContactsModalVisible, setIsContactsModalVisible] = useState(false);
    const [contacts, setContacts] = useState({phone: null, email: null});
    const [contactsName, setContactsName] = useState("");

    const [isEditStatusModalVisible, setIsEditStatusModalVisible] = useState(false);
    const [statuses, setStatuses] = useState([]);
    const [selectedStatus, setSelectedStatus] = useState("");
    const [editedPrize, setEditedPrize] = useState(null);

    const toast = useRef(null);
    console.log(isEnded)
    return (
        <div className="ml-4 mr-4 mb-4">
            <Toast
                ref={toast}
                position="top-center"/>
            <h1>Призы</h1>

            <DataTable
                showGridlines
                stripedRows
                value={prizesInfo}
                tableStyle={{minWidth: '5rem'}}>
                <Column
                    field="name"
                    header="Приз"></Column>
                <Column
                    field="status"
                    header="Статус"></Column>
                <Column
                    header="Промокод"
                    body={(prize) =>
                        <label>{(prize.winnerCode ?? "Не разыгран")}</label>
                    }/>
                <Column
                    header="Победитель"
                    body={(prize) => <Button
                        text
                        onClick={() => ShowContactsModal(prize)}>{prize.winnerName}</Button>}/>
                <Column
                    header=""
                    body={(prize) => <Button
                        icon="pi pi-pencil"
                        disabled={!isEnded}
                        onClick={() => EditStatus(prize)}
                        size="small"/>}
                    style={{width: '50px'}}/>
            </DataTable>

            {
                // Диалог, который отображает контакты победителя
                // если кликнуть по его имени.
            }
            <Dialog
                visible={isContactsModalVisible}
                modal
                header={contactsName}
                footer={
                    <div>
                        <Button
                            label="Ok"
                            icon="pi pi-check"
                            onClick={() => setIsContactsModalVisible(false)}
                            autoFocus/>
                    </div>
                }
                style={{minWidth: '30rem'}}
                onHide={() => {
                    if (!isContactsModalVisible) return;
                    setIsContactsModalVisible(false);
                }}>
                <p style={{marginBlock: '10px'}}><b>Телефон:</b> {(contacts.phone ?? 'не указан')} </p>
                <p><b>Электронная почта:</b> {(contacts.email ?? 'не указана')} </p>
            </Dialog>

            <Dialog
                visible={isEditStatusModalVisible}
                modal
                header="Новый статус"
                footer={
                    <div>
                        <Button
                            label="Сохранить"
                            onClick={() => SaveNewStatus()}/>
                    </div>
                }
                style={{minWidth: '30rem'}}
                onHide={() => {
                    if (!isEditStatusModalVisible) return;
                    setIsEditStatusModalVisible(false);
                }}>
                <Dropdown
                    value={selectedStatus}
                    onChange={(e) => setSelectedStatus(e.value)}
                    options={statuses}
                    placeholder="Выберите статус"
                    className="w-full"
                    style={{marginBlock: '10px'}}/>
            </Dialog>
        </div>
    );

    // Открывает модалку просмотра контактов победителя
    async function ShowContactsModal(prize) {
        setContactsName(prize.winnerName)

        try {
            const response = await prizesService.getWinnerContacts(prize.id);

            setContactsName(response.data.result.name);
            setContacts({phone: response.data.result.phone, email: response.data.result.email});
            setIsContactsModalVisible(true);
        } catch (error) {
            let message = "Ошибка сервера";

            if (error.status === 400) {
                message = error.data.errors[0].reason;
            }

            toast.current.show({severity: "error", summary: "Не удалось посмотреть контакты", detail: message});
        }
    }

    // Открывает модалку редактирования статуса приза
    function EditStatus(prize) {
        setEditedPrize(prize);
        setIsEditStatusModalVisible(true);
    }

    // Закрывает модалку редактирования статуса приза
    async function SaveNewStatus() {

        if (selectedStatus) {
            try {
                await prizesService.updatePrizeStatus(editedPrize.id, selectedStatus);
                editedPrize.status = selectedStatus;
            } catch (error) {
                let message = "Ошибка сервера";

                if (error.status === 400) {
                    message = error.data.errors[0].reason;
                }

                toast.current.show({severity: "error", summary: "Ошибка смены статуса", detail: message});
                return;
            }
        }

        setIsEditStatusModalVisible(false);
    }
};

export default PrizesTable;