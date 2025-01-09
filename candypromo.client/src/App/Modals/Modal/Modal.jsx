import './Modal.css';

function Modal({isModalOpen, setIsModalOpen, canForceClose, children}) {

    function closeModal()
    {
        setIsModalOpen(false)
    }

    function forceClose(target)
    {
        if (canForceClose && target.classList.contains('shadow'))
            setIsModalOpen(false)
    }

    return isModalOpen ? (
        <div className='shadow' onClick={(e) => forceClose(e.target)}>
            <div className='container'>
                {children}
            </div>
        </div>
    ) : null;
}

export default Modal;
