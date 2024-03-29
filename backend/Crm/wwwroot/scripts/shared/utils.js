﻿function initDatePicker(element, table) {
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
                        (key, value) => {
                        return {
                            label: value,
                            value: key
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

function initMoney(element, invert) {
    const $input = $(element);

    $input.val($input.val().replace('.', ','));

    $input.keypress(e => {
        const separatorKeyKode = invert ? 46 : 44;

        if (e.keyCode !== separatorKeyKode && (e.keyCode < 48 || e.keyCode > 57)) {
            e.preventDefault();
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

function getHashFromUrl() {
    return window.location.hash.replace('#', '');
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

function administrationStoreLinkFormatter(value, options, data) {
    return data.StoreId > 0 ? `<a href="/Administration/Stores?Id=${data.StoreId}">${data.StoreName}</a>` : '';
}

function storeLinkFormatter(value, options, data) {
    return data.StoreId > 0 ? `<a href="/Stores?Id=${data.StoreId}">${data.StoreName}</a>` : '';
}

function administrationUserLinkFormatter(value, options, data) {
    return `<a href="/Administration/Users?Id=${data.UserId}">${data.UserLogin}</a>`;
}

function userLinkFormatter(value, options, data) {
    return `<a href="/Users?Id=${data.UserId}">${data.UserLogin}</a>`;
}

function administrationProductLinkFormatter(value, options, data) {
    return `<a href="/Administration/Products?Id=${data.ProductId}">${data.ProductName}</a>`;
}

function productLinkFormatter(value, options, data) {
    return `<a href="/Products?Id=${data.ProductId}">${data.ProductName}</a>`;
}

function administrationClientLinkFormatter(value, options, data) {
    return `<a href="/Administration/Clients?Id=${data.ClientId}">${data.ClientName}</a>`;
}

function administrationOrderLinkFormatter(value, options, data) {
    return `<a href="/Administration/Orders?Id=${data.OrderId}">${data.OrderId}</a>`; 
}

function clientLinkFormatter(value, options, data) {
    return `<a href="/Clients?Id=${data.ClientId}">${data.ClientName}</a>`;
}

function linkUnFormatter(value, options, cell) {
    return $('a', cell).text();
}

function phoneFormatter(value) {
    return `+7${value}`;
}

function phoneUnFormatter(value) {
    return value && value.length > 0 ? value.slice(value.length - 10) : value;
}