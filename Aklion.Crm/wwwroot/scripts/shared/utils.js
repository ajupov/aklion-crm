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

function initAutocomplete(element, url, hiddenId) {
    $(element).autocomplete({
        delay: 200,
        minLength: 1,
        autoFocus: true,
        source: (request, response) => {
            $.get(url,
                { pattern: request.term },
                result => {
                    response($.map(result,
                        (item) => {
                            return {
                                label: item.Value,
                                value: item.Id
                            };
                        }));
                },
                'json');
        },
        select: (event, ui) => {
            const $input = $(event.target);
            const $form = $input.closest('table.EditTable');
            const $hidden = $form.find(`#${hiddenId}`);

            $input.val(ui.item.label);
            $hidden.val(ui.item.value);
            debugger;
            event.preventDefault();
            event.stopPropagation();
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