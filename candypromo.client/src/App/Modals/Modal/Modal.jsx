import './Modal.css';
import Icon from '@mdi/react';
import {mdiClose} from '@mdi/js';

/*
    Компонент пустое модальное окно.
    Выполняет роль пустого контейнера но с функцией отображения
    в виде модального окна.
    Входные параметры:
    - isModalOpen, setIsModalOpen - хук useState(true | false) на отображение модалки.
    - canForceClose - true | false, где true разрешает пользователю закрыть модалку по клику за пределы модалки.
    - header - string - заголовок модалки.
    - children - контент модалки.
*/
function Modal({isModalOpen, setIsModalOpen, canForceClose, header, children}) {

    return isModalOpen ? (
        <div className='modal-shadow' onClick={(e) => forceClose(e.target)}>
            <div className='modal-container'>
                <div className="modal-header">
                    <label>{header}</label>
                    <Icon className='close-btn'
                          path={mdiClose}
                          size={1}
                          onClick={closeModal}
                    />
                </div>
                {children}
            </div>
        </div>
    ) : null;

    // Закрывает модалку.
    function closeModal() {
        setIsModalOpen(false)
    }

    // Обработчик клика за пределы модалки.
    // Проверяет можно ли закрыть таким способом.
    function forceClose(target) {
        if (canForceClose && target.classList.contains('shadow'))
            closeModal()
    }
}

export default Modal;
