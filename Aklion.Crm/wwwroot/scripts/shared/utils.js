function initDatePicker(element, table) {
    $(element).datepicker({
        changeMonth: true,
        changeYear: true,
        onSelect: () => {
            if (table !== null && table !== undefined) {
                table[0].triggerToolbar();
            }
        }
    });
}

function getFilters() {
    var params = {};

    window.location.search.replace(/[?&]+([^=&]+)=([^&]*)/gi,
        (str, key, value) => {
            params[key] = value;
        }
    );

    return params;
}