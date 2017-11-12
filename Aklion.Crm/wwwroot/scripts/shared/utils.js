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
            getJson(url,
                { pattern: request.term },
                result =>
                response($.map(result,
                    item => {
                        return {
                            label: item.Value,
                            value: item.Id
                        };
                    })));
        },
        select: (event, ui) => {
            event.preventDefault();
            event.stopPropagation();

            const $input = $(event.target);
            const $form = $input.closest('table.EditTable');
            const $hidden = $form.find(`#${hiddenId}`);

            $input.val(ui.item.label);
            $hidden.val(ui.item.value);
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

function getJson(url, parameters, callback) {
    $.get(url,
        parameters,
        result => {
            callback(result);
        },
        'json');
}

function postJson(url, parameters, callback) {
    $.post(url,
        parameters,
        result => {
            callback(result);
        },
        'json');
}