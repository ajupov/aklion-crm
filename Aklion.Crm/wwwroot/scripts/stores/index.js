'use strict';

//var storesUi = {
//    storesTableWrapper: $('#stores')
//};






//function getFilters(tableId) {
//    const filters = {};

//    $(`.${tableId}-table-filter`).each(
//        (index, item) => {
//            const column = $(item);
//            const name = column.attr('data-filter-name');
//            const value = column.val();

//            if (name.length > 0 && value.length > 0) {
//                filters[name] = value;
//            }
//        });

//    return filters;
//}

//function getSorting(tableId) {
//    $(`.${tableId}-table-sorting-button`).each(
//        (index, item) => {
//            const button = $(item);

//            if (button.attr('data-sorting-enable') === 'true') {
//                return {
//                    SortingColumn: button.attr('data-sorting-column'),
//                    SortingOrder: button.attr('data-sorting-order')
//                };
//            }
//        });

//    return {
//        SortingColumn: '',
//        SortingOrder: ''
//    };
//}

//function createHead(tableWrapper, columns, dataUrl) {
//    const tableId = tableWrapper[0].id;

//    let html = `<table id=${tableId}-table" class="bordered striped highlight grid">`;

//    html += '<tbody>';
//    html += '</tbody>';

//    html += '<tfoot>';
//    html += createSortingButtons(tableId);
//    html += '</tfoot>';
//    html += '</table>';

//    html += `Показано: ${(x.Page - 1) * x.Size + 1} - ${x.Page * x.Size < x.TotalCount ? x.Page * x.Size : x.TotalCount} из ${x.TotalCount}`;


//    $('.select').material_select();
//    $('.datepicker').pickadate(datePickerOptions);

//    $(`.${tableId}-table-sorting-button`).click(event => {
//        const button = $(event.target);

//        $(`.${tableId}-table-sorting-button`).each((index, button) => {
//            const otherButton = $(button);
//            otherButton.attr('data-sorting-order', 'asc');
//            otherButton.attr('data-sorting-enable', 'false');
//            otherButton.html(otherButton.attr('data-label'));
//        });

//        if (button.attr('data-sorting-order') === 'desc') {
//            button.attr('data-sorting-order', 'asc');
//            button.attr('data-sorting-enable', 'true');
//            button.html(button.attr('data-label') + '<i class="fa fa-sort-down"></i>');
//        } else {
//            button.attr('data-sorting-order', 'desc');
//            button.attr('data-sorting-enable', 'true');
//            button.html(button.attr('data-label') + '<i class="fa fa-sort-up"></i>');
//        }

//        createBody(tableId, columns, dataUrl);
//    });



//    createBody(tableId, columns, dataUrl);
//}


$(document).ready(() => {


    //var storesColumns = [
    //    {
    //        label: '№',
    //        name: 'Id',
    //        key: true,
    //        type: 'int',
    //        autocompleteUrl: '',
    //        width: 50,
    //        align: 'left',
    //        hidden: false,
    //        search: true,
    //        sorting: true,
    //        edit: false
    //    },
    //    //{
    //    //    label: 'Создатель',
    //    //    name: 'CreateUserId',
    //    //    key: false,
    //    //    type: 'autocomplete',
    //    //    autocompleteUrl: '/Users',
    //    //    width: 60,
    //    //    align: 'left',
    //    //    hidden: false,
    //    //    search: true,
    //    //    edit: true
    //    //},
    //    {
    //        label: 'Название',
    //        name: 'Name',
    //        key: false,
    //        type: 'string',
    //        autocompleteUrl: '',
    //        width: 185,
    //        align: 'left',
    //        hidden: false,
    //        search: true,
    //        sorting: true,
    //        edit: true
    //    },
    //    {
    //        label: 'АПИ-ключ',
    //        name: 'ApiKey',
    //        key: false,
    //        type: 'string',
    //        autocompleteUrl: '',
    //        width: 250,
    //        align: 'left',
    //        hidden: false,
    //        search: true,
    //        sorting: true,
    //        edit: true
    //    },
    //    {
    //        label: 'АПИ-секрет',
    //        name: 'ApiSecret',
    //        key: false,
    //        type: 'string',
    //        autocompleteUrl: '',
    //        width: 250,
    //        align: 'left',
    //        hidden: false,
    //        search: true,
    //        sorting: true,
    //        edit: true
    //    },
    //    {
    //        label: 'Заблокирован',
    //        name: 'IsLocked',
    //        key: false,
    //        type: 'bool',
    //        autocompleteUrl: '',
    //        width: 120,
    //        align: 'center',
    //        hidden: false,
    //        search: true,
    //        sorting: true,
    //        edit: true
    //    },
    //    {
    //        label: 'Скрыт',
    //        name: 'IsDeleted',
    //        key: false,
    //        type: 'bool',
    //        autocompleteUrl: '',
    //        width: 80,
    //        align: 'center',
    //        hidden: false,
    //        search: true,
    //        sorting: true,
    //        edit: true
    //    },
    //    {
    //        label: 'Создан',
    //        name: 'CreateDate',
    //        key: false,
    //        type: 'datetime',
    //        autocompleteUrl: '',
    //        width: 150,
    //        align: 'center',
    //        hidden: false,
    //        search: true,
    //        sorting: true,
    //        edit: true
    //    },
    //    {
    //        label: 'Изменён',
    //        name: 'ModifyDate',
    //        key: false,
    //        type: 'datetime',
    //        autocompleteUrl: '',
    //        width: 150,
    //        align: 'center',
    //        hidden: false,
    //        search: true,
    //        sorting: true,
    //        edit: true
    //    },
    //    //{
    //    //    label: 'Удаление',
    //    //    name: '',
    //    //    key: false,
    //    //    type: 'datetime',
    //    //    autocompleteUrl: '',
    //    //    width: 110,
    //    //    align: 'center',
    //    //    hidden: false,
    //    //    search: false,
    //    //    sorting: false,
    //    //    edit: false
    //    //}
    //];
    //var storesDataUrl = '/Stores/GetList';

    const $storesTable = $('#stores-table');
    const $storesTableBody = $storesTable.find('tbody');
    
    $storesTableBody.empty();

    $.get('/Stores/GetList',
        {},
        result => {

            let html = '';

            $.each(result.Items,
                (i, e) => {
                    html += '<tr>';

                    html += '<td>';
                    html += e.Id;
                    html += '</td>';

                    html += '<td>';
                    html += e.CreateUserId;
                    html += '</td>';

                    html += '<td>';
                    html += e.Name;
                    html += '</td>';

                    html += '<td>';
                    html += e.ApiKey;
                    html += '</td>';

                    html += '<td>';
                    html += e.ApiSecret;
                    html += '</td>';

                    html += '<td>';
                    html += e.IsLocked ? 'Да' : 'Нет';
                    html += '</td>';

                    html += '<td>';
                    html += e.IsDeleted ? 'Да' : 'Нет';
                    html += '</td>';

                    html += '<td>';
                    html += e.CreateDate !== null
                        ? (new Date(e.CreateDate).toLocaleDateString('ru-RU') +
                            ' ' +
                            new Date(e.CreateDate).toLocaleTimeString('en-GB'))
                        : '';
                    html += '</td>';

                    html += '<td>';
                    html += e.ModifyDate !== null
                        ? (new Date(e.ModifyDate).toLocaleDateString('ru-RU') +
                            ' ' +
                            new Date(e.ModifyDate).toLocaleTimeString('en-GB'))
                        : '';
                    html += '</td>';
                    html += '<td>';
                    const id = $storesTable[0].id + '-' + e.Id;
                    html += `<input type="checkbox" id="${id}"/><label for="${id}"></label>`;
                    html += '</td>';

                    html += '</tr>';
                });

            $storesTableBody.append(html);
        });
});

function order(button) {
    const $button = $(button);
    const $elementSortingOrder = $button.attr('data-sorting-order');

    $button.closest('td').closest('tr').find('button').each((index, otherButton) => {
        const $otherButton = $(otherButton);
        $otherButton.attr('data-sorting-order', 'asc');
        $otherButton.attr('data-sorting-enable', 'false');
        $otherButton.html($otherButton.attr('data-label'));
    });
    $button.attr('data-sorting-enable', 'true');

    if ($elementSortingOrder === 'desc') {
        $button.attr('data-sorting-order', 'asc');
        $button.html($button.attr('data-label') + '<i class="fa fa-sort-down"></i>');
    } else {
        $button.attr('data-sorting-order', 'desc');
        $button.html($button.attr('data-label') + '<i class="fa fa-sort-up"></i>');
    }

    getRows($button);
}

function change(input) {
    getRows(input);
}

function refresh(element) {
    getRows(element);
}

function fastBackward(element) {
    getRows(element);
}

function backward(element) {
    getRows(element);
}

function changePage(element) {
    getRows(element);
}

function changeSize(element) {
    getRows(element);
}

function forward(element) {
    getRows(element);
}

function fastForward(element) {
    getRows(element);
}

function getRows(element) {
    const table = element.closest('table');

    alert(table);
}

