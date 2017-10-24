'use strict';

var storesUi = {
    stores: $('#stores')
};






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
    var storesColumns = [
        {
            label: '№',
            name: 'Id',
            type: 'int',
            width: 60,
            align: 'left',
            filter: true,
            sorting: true
        },
        //{
        //    label: 'Создатель',
        //    name: 'CreateUserId',
        //    type: 'autocomplete',
        //    width: 100,
        //    align: 'left',
        //    filter: true,
        //    search: true
        //},
        {
            label: 'Название',
            name: 'Name',
            type: 'string',
            width: 120,
            align: 'left',
            filter: true,
            sorting: true
        },
        {
            label: 'АПИ-ключ',
            name: 'ApiKey',
            type: 'string',
            width: 270,
            align: 'left',
            filter: true,
            sorting: true
        },
        {
            label: 'АПИ-секрет',
            name: 'ApiSecret',
            type: 'string',
            width: 270,
            align: 'left',
            hidden: false,
            filter: true,
            sorting: true
        },
        {
            label: 'Заблокирован',
            name: 'IsLocked',
            type: 'bool',
            width: 130,
            align: 'center',
            filter: true,
            sorting: true
        },
        {
            label: 'Скрыт',
            name: 'IsDeleted',
            type: 'bool',
            width: 80,
            align: 'center',
            filter: true,
            sorting: true
        },
        {
            label: 'Создан',
            name: 'CreateDate',
            type: 'datetime',
            width: 160,
            align: 'center',
            filter: true,
            sorting: true
        },
        {
            label: 'Изменён',
            name: 'ModifyDate',
            type: 'datetime',
            width: 160,
            align: 'center',
            hidden: false,
            filter: true,
            sorting: true
        }
    ];


    const $storesTable = $('#stores-table');

    //Create table;

    create(storesUi.stores, storesColumns, true);
});

function create(container, columns, editable) {
    let html = '';

    html += `<table id="stores-table" 
                    class="bordered striped highlight c-table" 
                    data-url="/Stores/GetList" 
                    data-can-manage="${editable}">`;
    html += '<thead>';

    html += '<tr>';
    $.each(columns,
        (index, column) => {
            html += `<th style="width: ${column.width}px">`;

            if (column.sorting) {
                html += `<button data-role="sorting"
                                 data-label="${column.label}"
                                 data-name="${column.name}"
                                 data-type="${column.type}"
                                 data-align="${column.align}"
                                 data-sorting-enable="false"
                                 title="Сортировать по ${column.label}"
                                 onclick="order(this);">`;
                html += column.label;
                html += '</button>';
            } else {
                html += column.label;
            }

            if (column.filter) {
                html += `<button class="dropdown-button not-closing-dropdown" 
                                 data-activates="${column.name}"
                                 data-role="filter"
                                 data-beloworigin="true"
                                 title="Фильтровать по ${column.label}">`;
                html += '<i class="fa fa-filter"></i>';
                html += '</button>';

                html += `<div id="${column.name}" data-type="${column.type}" class="dropdown-content filter-dropdown-content">`;
                html += '<div class="row no-margin-vertical valign-wrapper">';
                html += '<div class="col s5">';
                html += '<select data-role="filter-operand" onchange="setOperand(this);">';
                html += '<option value="eq" selected="selected">Равно</option>';
                html += '<option value="ne">Не равно</option>';
                html += '<option value="gt">Больше</option>';
                html += '<option value="lt">Меньше</option>';
                html += '<option value="bt">Между</option>';
                html += '</select>';
                html += '</div>';
                html += '<div class="col s7">';
                html += '<input type="text" data-operand-input="op1"/>';
                html += '</div>';
                html += '</div>';
                html += '<div class="row no-margin-vertical valign-wrapper">';
                html += '<div class="col s5">';
                html += '<span class="right hidden" data-operand="and">И</span>';
                html += '</div>';
                html += '<div class="col s7">';
                html += '<input type="text" class="hidden" data-operand-input="op2" />';
                html += '</div>';
                html += '</div>';
                html += '<div class="row no-margin-vertical valign-wrapper">';
                html += '<div class="col s4 offset-s4">';
                html += '<button onclick="applyFilter(this);">';
                html += 'Применить';
                html += '</button>';
                html += '</div>';
                html += '<div class="col s4">';
                html += '<button onclick="clearFilter(this);">';
                html += 'Сбросить';
                html += '</button>';
                html += '</div>';
                html += '</div>';
                html += '</div>';
            }

            html += '</th>';
        });

    if (editable) {
        html += '<th style="width: 40px"></th>';
    }
    html += '</tr>';
    html += '</thead>';

    html += '<tbody>';
    html += `<tr class="white" 
                 data-role="preloader">`;
    html += `<td colspan="11" 
                 class="center">`;
    html += '<span><b>Загрузка...</b></span>';
    html += '<td>';
    html += '<tr>';
    html += '</tbody>';

    html += '<tfoot>';
    html += '<tr>';
    html += `<td colspan="11" 
                 class="center">`;
    html += `<button type="button" 
                     class="left" 
                     data-role="current-page"
                     data-page="0"
                     title="Обновить"
                     onclick="goToPage(this);">`;
    html += '<i class="fa fa-refresh"></i>';
    html += '</button>';

    html += `<button type="button"
                     data-role="first-page"
                     data-page="0"
                     title="Первая страница"
                     onclick="goToPage(this);">`;
    html += '<i class="fa fa-fast-backward"></i>';
    html += '</button>';

    html += `<button type="button"
                     data-role="prev-page"
                     data-page="0"
                     title="Предыдущая страница"
                     onclick="goToPage(this);">`;
    html += '<i class="fa fa-backward"></i>';
    html += '</button>';

    html += '<span class="left-border">Стр.</span>';

    html += `<input type="text"
                    value="0"
                    data-role="page"
                    title="Текущая страница"
                    style="width: 30px;"
                    onchange="setSize(this);">`;
    html += '<span class="left-border">Стр.</span>';
    html += '<span> из </span>';
    html += '<span class="right-border">0</span>';

    html += `<select data-role="size"
                     style="width: 45px;"
                     title="Размер страницы"
                     onchange="goToPage(this);">`;

    html += '<option selected="selected">2</option>';
    html += '<option>5</option>';
    html += '<option>10</option>';
    html += '<option>20</option>';
    html += '<option>50</option>';
    html += '<option>100</option>';
    html += '</select>';
    html += '<span class="right-border"></span>';


    html += `<button type="button"
                     data-role="next-page"
                     data-page="0"
                     title="Следующая страница"
                     onclick="goToPage(this);">`;
    html += '<i class="fa fa-forward"></i>';
    html += '</button>';

    html += `<button type="button"
                     data-role="last-page"
                     data-page="0"
                     title="Последняя страница"
                     onclick="goToPage(this);">`;

    html += '<i class="fa fa-fast-forward"></i>';
    html += '</button>';
    html += '<span class="right">';
    html += '<span>0</span>';
    html += '<span> - </span>';
    html += '<span>10</span>';
    html += '<span> из </span>';
    html += '<span>100</span>';
    html += '</span>';
    html += '</td>';
    html += '</tr>';
    html += '</tfoot>';
    html += '</table>';

    container.html(html);
    $('.dropdown-button').dropdown();
    $('.not-closing-dropdown + .dropdown-content').on('click', e => e.stopPropagation());


    getRows($('#stores-table'));
}

function order(button) {
    const $button = $(button);
    const $buttonSortingOrder = $button.attr('data-sorting-order');

    $button.closest('tr').find('button').each((index, otherButton) => {
        const $otherButton = $(otherButton);
        $otherButton.attr('data-sorting-order', 'asc');
        $otherButton.attr('data-sorting-enable', 'false');
       
        $otherButton.html($otherButton.attr('data-label'));
    });

    $button.attr('data-sorting-enable', 'true');

    if ($buttonSortingOrder === 'desc') {
        $button.attr('data-sorting-order', 'asc');
        $button.html($button.attr('data-label') + '<i class="fa fa-sort-up"></i>');
    } else {
        $button.attr('data-sorting-order', 'desc');
        $button.html($button.attr('data-label') + '<i class="fa fa-sort-down"></i>');
    }

    getRows($button);
}

//function showFilter(button) {
//    const $button = $(button);


//}

function filter(element) {
    getRows(element);
}

function setSize(element) {
    getRows(element);
}



/* General */
function getRows(element) {
    const $table = $(element.closest('table'));

    let parameters = {};
    parameters = Object.assign(parameters, getSorting($table));
    //parameters = Object.assign(parameters, getFilters($table));
    parameters.Filters = getFilters($table);

    parameters = Object.assign(parameters, getPage($table));
    parameters = Object.assign(parameters, getSize($table));

    const dataUrl = $table.attr('data-url');
    const canManage = $table.attr('data-can-manage');

    const $tableBody = $table.find('tbody');

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
                        });

                    if (canManage === 'true') {
                        html += '<td class="center">';
                        html += `<button type="button" onclick="edit(${row.Id});" title="Изменить">`;
                        html += '<i class="fa fa-pencil"></i>';
                        html += '</button>';
                        html += `<button type="button" onclick="remove(${row.Id});" title="Удалить">`;
                        html += '<i class="fa fa-times"></i>';
                        html += '</button>';
                        html += '</td>';
                    }

                    html += '</tr>';
                });

            $tableBody.empty();
            $tableBody.append(html);

            updatePagination($table, result);
        });
}

function setPreloader(element) {
    const $table = $(element.closest('table'));
    const $tableBody = $table.find('tbody');

    const html = '<tr class="white" data-role="preloader">' +
        '<td colspan="11" class="center">' +
        '<span><b>Загрузка...</b></span>' +
        '<td>' +
        '<tr>';

    $tableBody.html(html);
}

/* Get parameters */
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
    const filters = [];

    $table.find('.filter-dropdown-content').each((index, dropdown) => {
        const $dropdown = $(dropdown);

        const name = dropdown.id;
        const type = $dropdown.attr('data-type');
        const operation = $dropdown.find('[data-role="filter-operand"]').val();
        const operand1 = $dropdown.find('[data-operand-input="op1"]').val();
        const operand2 = $dropdown.find('[data-operand-input="op2"]').val();

        if (name.length > 0 && operand1.length > 0) {
            if (operation === 'bt' && operand2.length > 0) {
                filters.push({
                    Name: name,
                    Type: type,
                    Operation: operation,
                    Operand1: operand1,
                    Operand2: operand2
                });
            } else {
                filters.push({
                    Name: name,
                    Type: type,
                    Operation: operation,
                    Operand1: operand1
                });
            }
        }
    });

    $table.find('[data-role="filter-operand"]').each((index, input) => {
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

/* Updating */
function updatePagination($table, result) {
    const currentPage = $table.find('[data-role="current-page"]');
    currentPage.attr('data-page', result.Page);

    const firstPage = $table.find('[data-role="first-page"]');
    firstPage.attr('data-page', 1);

    const prevPage = $table.find('[data-role="prev-page"]');
    prevPage.attr('data-page', result.Page > 1 ? result.Page - 1 : 1);

    const page = $table.find('[data-role="page"]');
    page.val(result.Page);

    const nextPage = $table.find('[data-role="next-page"]');
    nextPage.attr('data-page', result.PageCount > result.Page ? result.Page + 1 : result.Page);

    const lastPage = $table.find('[data-role="last-page"]');
    lastPage.attr('data-page', result.PageCount);
}

/*  Manage */
function edit(id) {
    alert(id);
}

function remove(id) {
    alert(id);
}

/*Filtering*/
function setOperand(element) {
    const $element = $(element);
    const $dropdown = $element.closest('.filter-dropdown-content');
    const $dataOperand = $dropdown.find('[data-operand="and"]');
    const $dataOperandInput2 = $dropdown.find('[data-operand-input="op2"]');

    if ($element.val() === 'bt') {
        $dataOperand.show();
        $dataOperandInput2.show();
    } else {
        $dataOperand.hide();
        $dataOperandInput2.hide();
    }
}

function applyFilter(element) {
    const $element = $(element);
    const $dropdown = $element.closest('.filter-dropdown-content');
    const $select = $dropdown.find('select');
    const $dataOperand = $dropdown.find('[data-operand="and"]');
    const $dataOperandInput1 = $dropdown.find('[data-operand-input="op1"]');
    const $dataOperandInput2 = $dropdown.find('[data-operand-input="op2"]');

    const $th = $element.closest('th');
    const $filterButton = $th.find('[data-role="filter"]');

    if ($dataOperandInput1.val() !== '' || $dataOperandInput2.val() !== '') {
        $filterButton.css('color', 'red');
    } else {
        $filterButton.css('color', 'rgba(0, 0, 0, 0.87)');
    }

    getRows(element);
}

function clearFilter(element) {
    const $element = $(element);
    const $dropdown = $element.closest('.filter-dropdown-content');
    const $select = $dropdown.find('select');
    const $dataOperand = $dropdown.find('[data-operand="and"]');
    const $dataOperandInput1 = $dropdown.find('[data-operand-input="op1"]');
    const $dataOperandInput2 = $dropdown.find('[data-operand-input="op2"]');
    const $th = $element.closest('th');
    const $filterButton = $th.find('[data-role="filter"]');

    $dataOperandInput1.val('');
    $dataOperandInput2.val('');
    $dataOperand.hide();
    $dataOperandInput2.hide();
    $select.val('eq');
    $filterButton.css('color', 'rgba(0, 0, 0, 0.87)');

    getRows(element);
}