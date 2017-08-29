'use strict';

var indexUi = {
    organizations: $('#organizations')
};

$(document).ready(() => {
    $.get('Organizations/GetList',
        result => {
            indexUi.organizations.val(result);
        },
        'json').fail(() => alert('Произошла ошибка'));
});