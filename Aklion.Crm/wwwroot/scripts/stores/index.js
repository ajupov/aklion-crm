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

    const $storesTable = $('#stores-table');

    getList($storesTable);
});

function order(button) {
    const $button = $(button);
    const $buttonSortingOrder = $button.attr('data-sorting-order');

    $button.closest('td').closest('tr').find('button').each((index, otherButton) => {
        const $otherButton = $(otherButton);
        $otherButton.attr('data-sorting-order', 'asc');
        $otherButton.attr('data-sorting-enable', 'false');
        $otherButton.html($otherButton.attr('data-label'));
    });

    $button.attr('data-sorting-enable', 'true');

    if ($buttonSortingOrder === 'desc') {
        $button.attr('data-sorting-order', 'asc');
        $button.html($button.attr('data-label') + '<i class="fa fa-sort-down"></i>');
    } else {
        $button.attr('data-sorting-order', 'desc');
        $button.html($button.attr('data-label') + '<i class="fa fa-sort-up"></i>');
    }

    getRows($button);
}

function filter(input) {
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
    const $table = $(element.closest('table'));

    let parameters = {};
    parameters = Object.assign(parameters, getSorting($table));
    parameters = Object.assign(parameters, getFilters($table));
    parameters = Object.assign(parameters, getPage($table));
    parameters = Object.assign(parameters, getSize($table));

    getList($table, parameters);
}

function getSorting($table) {
    const sorting = {};

    $table.find('[data-role="sorting"]').each((index, button) => {
        const $button = $(button);

        const dataSortingEnable = $button.attr('data-sorting-enable') === 'true';
        if (dataSortingEnable) {
            sorting['SortingColumn'] = $button.attr('data-name');
            sorting['SortingOrder'] = $button.attr('data-sorting-order');
        }
    });

    return sorting;
}

function getFilters($table) {
    const filters = {};

    $table.find('[data-role="filter"]').each((index, input) => {
        const $input = $(input);

        const value = $input.val();

        if (value.length > 0) {
            const dataName = $input.attr('data-name');

            filters[dataName] = value;
        }
    });

    return filters;
}

function getPage($table) {
    const page = {};

    const input = $table.find('[data-role="page"]')[0];
    const value = parseInt($(input).val());
    page['Page'] = value > 0 ? value : 1;

    return page;
}

function getSize($table) {
    const size = {};

    const input = $table.find('[data-role="size"]')[0];
    const value = parseInt($(input).val());
    size['Size'] = value > 0 ? value : 1;

    return size;
}

function getList($table, parameters) {
    const dataUrl = $table.attr('data-url');

    const $tableBody = $table.find('tbody');

    $tableBody.empty();

    $.get(dataUrl,
        parameters,
        result => {
            if (result === undefined || result === null) {
                return;
            }

            const columns = getColumns($table);
            
            let html = '';

            $.each(result.Items,
                (itemIndex, row) => {
                    html += '<tr>';

                    $.each(columns,
                        (columnIndex, column) => {
                            if (row.hasOwnProperty(column.Name)) {
                                const value = row[column.Name];
                                
                                html += `<td class="${column.Align}-align">`;

                                if (value !== null) {
                                    switch (column.Type) {
                                    case 'int':
                                    case 'decimal':
                                    case 'double':
                                    case 'string':
                                        html += value;
                                        break;
                                    case 'date':
                                        html += new Date(value).toLocaleDateString('ru-RU');
                                        break;
                                    case 'datetime':
                                        html += new Date(value).toLocaleDateString('ru-RU') +
                                            ' ' +
                                            new Date(value).toLocaleTimeString('en-GB');
                                        break;
                                    case 'time':
                                        html += new Date(value).toLocaleTimeString('en-GB');
                                        break;
                                    case 'bool':
                                        html += value ? 'Да' : 'Нет';
                                        break;
                                    }
                                }

                                html += '</td>';
                            }
                        });

                    //html += '<td class="center">';
                    //html += `<button type="button" onclick="edit(${row.Id});" title="Изменить">`;
                    //html += '<i class="fa fa-pencil"></i>';
                    //html += '</button>';
                    //html += `<button type="button" onclick="remove(${row.Id});" title="Удалить">`;
                    //html += '<i class="fa fa-times"></i>';
                    //html += '</button>';
                    //html += '</td>';

                    html += '</tr>';
                });

            $tableBody.append(html);
        });
}

function getColumns($table) {
    const columns = new Array();

    $table.find('[data-role="sorting"]').each((index, button) => {
        const $button = $(button);

        columns.push({
            Name: $button.attr('data-name'),
            Type: $button.attr('data-type'),
            Align: $button.attr('data-align')
        });
    });

    return columns;
}

function edit(id) {
    alert(id);
}

function remove(id) {
    alert(id);
}