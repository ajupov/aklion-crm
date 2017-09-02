'use strict';

var indexUi = {
    stores: $('#stores')
};

$(document).ready(() => {
    $.get('Stores/GetList',
        result => {
            indexUi.stores.val(result);
        },
        'html').fail(() => indexUi.stores.val('Произошла ошибка'));
});