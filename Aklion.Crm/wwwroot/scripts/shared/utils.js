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