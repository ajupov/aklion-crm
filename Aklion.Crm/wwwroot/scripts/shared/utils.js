function initDatePicker(element, table) {
    const $element = $(element);
    $element.datepicker({
        changeMonth: true,
        changeYear: true,
        onSelect: (e, d) => {
            var t = $element;
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

function getText(url, parameters, callback) {
    $.get(url,
        parameters,
        result => {
            callback(result);
        },
        'text');
}

function getJson(url, parameters, callback) {
    $.get(url,
        parameters,
        result => {
            callback(result);
        },
        'json');
}

function getJsonSync(url, parameters, callback) {
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

function postText(url, parameters, callback) {
    $.post(url,
        parameters,
        result => {
            callback(result);
        },
        'text');
}

function getSelectValues(url) {
    let values = '';

    $.get({
        url: url,
        async: false,
        dataType: 'json',
        success: result => {
            $.each(result,
                (key, value) => {
                    values += `${value}:${key};`;
                });
        }
    });

    if (values.charAt(values.length - 1) === ';') {
        values = values.substr(0, values.length - 1);
    }

    return values;
}