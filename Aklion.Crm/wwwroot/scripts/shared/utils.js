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

function initAutocomplete(element, url, hiddenId, dependentFields) {
    const $input = $(element);

    $input.autocomplete({
        delay: 200,
        minLength: 1,
        autoFocus: true,
        source: (request, response) => {
            const $form = $input.closest('table.EditTable');

            let parameters = {};
            $.each(dependentFields,
                (index, field) => {
                    const $field = $form.find(`#${field}`);
                    parameters[field] = $field.val();
                });
            parameters = Object.assign(parameters, { pattern: request.term });

            getJson(url,
                parameters,
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

function beforeShowJqGridForm(form) {
    //form.closest('.ui-jqdialog').position({
    //    my: 'center',
    //    at: 'center',
    //    of: $('body')
    //});
}