'use strict';

var storesUi = {
    grid: $('#jqGrid')
};

var dataOptions = {
    dataInit: e => {
        $(e).datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd.mm.yy'
        });
    }
};

$(document).ready(() => {
    storesUi.grid.jqGrid({
        url: '/Stores/Get',
        editurl: '/Stores/Edit',
        cellsubmit: 'Stores/Edit',
        colModel: [
            { label: '№', name: 'Id', width: 60, key: true, hidden: true },
            { label: 'Название', name: 'Name', width: 440, editable: true },
            { label: 'АПИ-ключ', name: 'ApiKey', width: 150, editable: true },
            { label: 'АПИ-секрет', name: 'ApiSecret', width: 200, editable: true },
            {
                label: 'Заблокирован', name: 'IsLocked', width: 80, align: 'center', stype: 'select', editable: true,
                searchoptions: { value: 'null:Все;true:Да;false:Нет' }, editoptions: { value: 'true:Да;false:Нет' },
                edittype: 'select', formatter: v => v ? 'Да' : 'Нет'
            },
            {
                label: 'Удалён', name: 'IsDeleted', width: 80, align: 'center', stype: 'select', editable: true,
                searchoptions: { value: 'null:Все;true:Да;false:Нет' }, editoptions: { value: 'true:Да;false:Нет' },
                edittype: 'select', formatter: v => v ? 'Да' : 'Нет'
            },
            {
                label: 'Создан', name: 'CreateDate', width: 100, align: 'center', formatter: 'date', sorttype: 'date',
                datefmt: 'dd.mm.yy',
                editable: true, searchoptions: dataOptions,
                editoptions: dataOptions
            },
            {
                label: 'Изменён', name: 'ModifyDate', width: 100, align: 'center', formatter: 'date', sorttype: 'date',
                datefmt: 'dd.mm.yy',
                editable: true, searchoptions: dataOptions,
                editoptions: dataOptions
            },
            {
                label: 'Действия', name: 'actions', width: 50, search: false, sortable: false, formatter: 'actions',
                formatoptions: {
                    keys: true,
                    editOptions: {},
                    addOptions: {},
                    delOptions: {}
                }
            }
        ],
        datatype: 'json',
        viewrecords: true,
        height: 'auto',
        rowNum: 10,
        rowList: [5, 10, 20, 50, 100],
        sortable: true,
        pager: '#jqGridPager'

    })
    .jqGrid('filterToolbar', { searchOperators: false })
    .inlineNav('#jqGridPager', {
        edit: true, add: true, del: true, cancel: true,
        editParams: { keys: true },
        addParams: { keys: true }
        });

});