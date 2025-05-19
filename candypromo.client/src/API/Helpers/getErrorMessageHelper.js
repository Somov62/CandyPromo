
function getErrorMessage(error) {

    if (error.status === 400) {
        return 'Неверные данные';
    }

    if (error.status === 500) {
        return error.Error;
    }

    return error.message;
}

export default getErrorMessage;