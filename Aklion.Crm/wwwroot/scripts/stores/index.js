'use strict';

var storesUi = {
    stores: $('#stores'),
    addStoreForm: $('#add-store-form'),
    addStoreModal: $('#add-store-modal')
};

//function getList() {
//    getHtml(urls.storesGetList,
//        {},
//        result => storesUi.stores.html(result),
//        error => storesUi.stores.html(error));
//}

function editRow(id) {
    getHtml(urls.storesGet,
        {},
        result => storesUi.stores.html(result),
        error => storesUi.stores.html(error));
    alert(id);
}

function deleteRow(id) {
    alert(id);
}

$(document).ready(() => {
    var grid = $('#jqGrid');

    grid.jqGrid({
        url: urls.storesGetList,
        editurl: urls.storesUpdateOrDelete,
        cellsubmit: urls.storesUpdateOrDelete,
        colModel: [

            { label: '№', name: 'id', width: 60, key: true, hidden: true },
            { label: 'Название', name: 'name', width: 440, editable: true },
            { label: 'АПИ-ключ', name: 'apiKey', width: 150, editable: true },
            { label: 'АПИ-секрет', name: 'apiSecret', width: 200, editable: true },
            {
                label: 'Заблокирован', name: 'isLocked', width: 80, align: 'center', stype: 'select', editable: true,
                searchoptions: { value: ':Все;yes:Да;no:Нет' }, editoptions: { value: 'yes:Да;no:Нет' },
                edittype: 'select', formatter: v => v ? 'Да' : 'Нет'
            },
            {
                label: 'Удалён', name: 'isDeleted', width: 80, align: 'center', stype: 'select', editable: true,
                searchoptions: { value: ':Все;yes:Да;no:Нет' }, editoptions: { value: 'yes:Да;no:Нет' },
                edittype: 'select',formatter: v => v ? 'Да' : 'Нет'
            },
            {
                label: 'Создан', name: 'createDate', width: 100, align: 'center', formatter: 'date', sorttype: 'date',
                editable: true, searchoptions: {
                    dataInit: e => {
                        $(e).datepicker({
                            changeMonth: true,
                            changeYear: true,
                            dateFormat: 'dd.mm.yy'
                        });
                    }
                },
                editoptions: {
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
                editable: true, searchoptions: {
                    dataInit: e => {
                        $(e).datepicker({
                            changeMonth: true,
                            changeYear: true,
                            dateFormat: 'dd.mm.yy'
                        });
                    }
                },
                editoptions: {
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
        multiple: true,
        rowList: [10, 20, 50, 100],
        sortable: true,
        pager: '#jqGridPager'
    })
        .jqGrid('filterToolbar', { searchOperators: false })
        .inlineNav('#jqGridPager', {
            edit: true, add: true, del: true, cancel: true,
            editParams: { keys: true },
            addParams: { keys: true }
            });
        //.jqGrid('navGrid', '#jqGridPager', { edit: true, add: true, del: true, search: false });

    //getList();

    //storesUi.addStoreForm.submit(e => {
    //    e.preventDefault();

    //    if (storesUi.addStoreForm.valid()) {
    //        postJson(urls.storesAdd,
    //            storesUi.addStoreForm.serialize(),
    //            () => {
    //                storesUi.addStoreModal.hide();
    //                storesUi.addStoreForm[0].reset();
    //                getList();
    //            },
    //            () => alert('Произошла ошбка'));
    //    }

    //    return;
    //});
});