'use strict';

var indexUi = {
    organizations: $('#organizations')
};

$(document).ready(() => {
    $.get('organizations/',
        result => {
            debugger;
            indexUi.organizations.val(result);
        },
        'json').fail(() => alert('Произошла ошибка'));
});