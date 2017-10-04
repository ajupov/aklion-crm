'use strict';

function getHtml(address, data, successFunction, errorFunction) {
    $.get(address, data,
        result => successFunction(result),
        'html').fail(error => errorFunction(error));
}

function getJson(address, data, successFunction, errorFunction) {
    $.get(address, data,
        result => successFunction(result),
        'json').fail(error => errorFunction(error));
}

function postJson(address, data, successFunction, errorFunction) {
    $.post(address, data,
        result => successFunction(result),
        'json').fail(error => errorFunction(error));
}

function table(element, pagerSelector) {
    element.jqGrid({
        url: urls.storesUpdate,
        colModel: [
            { label: '№', name: 'id', width: 60, key: true, sorttype: 'number', hidden: true },
            { label: 'Название', name: 'name', width: 440 },
            { label: 'АПИ-ключ', name: 'apiKey', width: 150 },
            { label: 'АПИ-секрет', name: 'apiSecret', width: 200 },
            {
                label: 'Заблокирован', name: 'isLocked', width: 80, align: 'center', formatter: 'checkbox', stype: 'select',
                searchoptions: { value: ':Все;yes:Да;no:Нет' }
            },
            {
                label: 'Удалён', name: 'isDeleted', width: 80, align: 'center', formatter: 'checkbox', stype: 'select',
                searchoptions: { value: ':Все;yes:Да;no:Нет' }
            },
            {
                label: 'Создан', name: 'createDate', width: 100, align: 'center', formatter: 'date', sorttype: 'date',
                searchoptions: {
                    dataInit: e => {
                        $(e).datepicker({
                            changeMonth: true,
                            changeYear: true,
                            dateFormat: 'dd.mm.yy'
                        });
                    }
                }
            },
            {
                label: 'Изменён', name: 'modifyDate', width: 100, align: 'center', formatter: 'date', sorttype: 'date',
                searchoptions: {
                    dataInit: e => {
                        $(e).datepicker({
                            changeMonth: true,
                            changeYear: true,
                            dateFormat: 'dd.mm.yy'
                        });
                    }
                }
            },
            {
                name: '', width: 20, sortable: false, search: false,
                formatter: (e, c, r) => '<span class="ui-icon ui-icon-pencil" onclick="editRow(' + r.id + ');"></span>'
            },
            {
                name: '', width: 20, sortable: false, search: false,
                formatter: (e, c, r) => '<span class="ui-icon ui-icon-trash" onclick="deleteRow(' + r.id + ');"></span>'
            }
        ],
        datatype: 'json',
        viewrecords: true,
        height: 'auto',
        hoverrows: true,
        rowNum: 10,
        rowList: [10, 20, 50, 100],
        sortable: true,
        recordpos: 'left',
        searchOnEnter: true,
        rownumWidth: 25,
        pager: pagerSelector
    });

    element.jqGrid('filterToolbar', { searchOperators: false });
}



