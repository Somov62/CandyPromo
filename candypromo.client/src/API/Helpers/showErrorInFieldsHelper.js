
function showErrorInFields(error) {
    if (error.status === 400) {
        error.data.errors.forEach(e => {
            e.propertyNames.forEach(pn => {
                const element = document.getElementById(`${pn.toLowerCase()}-help`);
                if (element) {
                    element.innerText = e.reason;
                }
            });
        });
    }
}

export default showErrorInFields;
