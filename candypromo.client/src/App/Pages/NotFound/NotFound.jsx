import React from 'react';
import './NotFound.css'
import { Message } from 'primereact/message';

const NotFound = () => {
    return (
        <div className="not-found_root-container">
            <Message severity="error" text="УПС!!! Страница не найдена..." />
        </div>
    );
};

export default NotFound;