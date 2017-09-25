'use strict';

var storesUi = {
    storesTableWrapper: $('#stores')
};

function createTable(tableWrapper, columns, dataUrl) {
    createHead(tableWrapper, columns, dataUrl);
}

function createHead(tableWrapper, columns, dataUrl) {
    var html = '';
    var tableId = tableWrapper[0].id;

    // Create
    html += '<table id="' + tableId + '-table" class="bordered striped highlight">';
    html += '<thead>';

    // Create table headers and sorting buttons
    html += '<tr>';
    $.each(columns,
        (i, e) => {
            // Create cell
            html += '<th ' + (e.hidden ? 'hidden ' : '') + 'width="' + e.width + '">';

            // Create sorting button
            if (e.sorting) {
                html += '<span class="pointer ' + tableId + '-table-sorting" data-label="' + e.label + '"' +
                    'data-sorting-order="asc" data-sorting-column="' + e.name + '">';
                html += e.label;
                html += '</span>';
            } else {
                html += '<span>';
                html += e.label;
                html += '</span>';
            }

            html +='</th>';
        });
    html += '</tr>';

    // Create table search
    html += '<tr>';
    $.each(columns,
        (i, e) => {
            // Create cell
            html += '<th ' + (e.hidden ? 'hidden ' : '') + 'width="' + e.width + '">';

            // Create filter input
            if (e.search) {
                if (e.type === 'int' || e.type === 'string') {
                    html += '<input type="text" class="' + tableId + '-table-filter" />';
                } else if (e.type === 'bool') {
                    html += '<select>';
                    html += '<option>';
                    html += 'Неизвестно';
                    html += '</option>';
                    html += '<option>';
                    html += 'Да';
                    html += '</option>';
                    html += '<option>';
                    html += 'Нет';
                    html += '</option>';
                    html += '</select>';
                } else if (e.type === 'datetime') {
                    html += '<input type="text" class="datepicker ' + tableId + '-table-filter" />';
                }
            }

            html += '</th>';
        });
    html += '</tr>';

    html += '</thead>';
    html += '</table>';

    // Append table
    tableWrapper.append(html);

    // Initialize material select
    $('select').material_select();

    // Initialize material datepicker
    $('.datepicker').pickadate();

    createBody(tableWrapper, columns, dataUrl);

    // Bind table sorting events
    $('.' + tableId + '-table-sorting').click(e => {
        // Get current clicked sorting button
        var sortingButton = $(e.target);
        var sortingColumn = sortingButton.attr('data-sorting-column');
        var sortingOrder = sortingButton.attr('data-sorting-order');
        var sortingButtonLabel = sortingButton.attr('data-label');

        // Reset other sorting buttons
        $('.' + tableId + '-table-sorting').each((oi, oe) => {
            var otherSortingButton = $(oe);
            otherSortingButton.attr('data-sorting-order', 'asc');

            var otherSortingButtonLabel = otherSortingButton.attr('data-label'); 
            otherSortingButton.html(otherSortingButtonLabel);
        });

        // Set current sorting order
        if (sortingOrder === 'desc') {
            sortingButton.attr('data-sorting-order', 'asc');
            sortingButton.html('<i class="fa fa-sort-down"></i>' + sortingButtonLabel);
        } else {
            sortingButton.attr('data-sorting-order', 'desc');
            sortingButton.html(' <i class="fa fa-sort-up"></i>' + sortingButtonLabel);
        }

        createBody(tableWrapper, columns, dataUrl, sortingColumn, sortingOrder);
    });
}

function createBody(tableWrapper, columns, dataUrl, page, size, sortingColumn, sortingOrder) {
    var tableId = tableWrapper[0].id;
    var table = $('table#' + tableId + '-table');

    // Clear
    table.children('tbody').empty();
    table.children('tfoot').empty();
    
    //Data
    var data = {
        Page: page > 0 ? page : 1,
        Size: size > 0 ? size : 10,
        SortingColumn: sortingColumn,
        SortingOrder: sortingOrder
    };

    // Get rows
    $.get(dataUrl,
        data,
        x => {
            if (x.Items === null || x.Items.length === 0) {
                return;
            }

            var notHiddenColumnsCount = 0;
            var isFirstRow = true;
            var html = '<tbody>';

            $.each(x.Items,
                (ii, ie) => {
                    html += '<tr>';

                    $.each(columns,
                        (ci, ce) => {
                            if (ie.hasOwnProperty(ce.name)) {

                                if (isFirstRow && !ce.hidden) {
                                    notHiddenColumnsCount++;
                                }

                                var value = ie[ce.name];

                                html += '<td ' + (ce.key ? ' data-id="' + value + '"' : '') +
                                    ' align="' + ce.align + '"' + (ce.hidden ? ' hidden' : '') + '>';

                                if (ce.type === 'int' || ce.type === 'string') {
                                    if (value !== null) {
                                        html += value;
                                    }
                                }

                                if (ce.type === 'datetime') {
                                    if (value !== null) {
                                        var dateTime = new Date(value);
                                        html += dateTime.toLocaleDateString('ru-RU') + ' в ' +
                                            dateTime.toLocaleTimeString('en-GB');
                                    } 
                                }

                                if (ce.type === 'bool') {
                                    var checked = value ? 'checked="checked"' : '';
                                    html += '<input type="checkbox" disabled ' + checked +'></input>';
                                }

                                html += '</td>';
                            }
                        });
                    isFirstRow = false;

                    html += '</tr>';
                });
            html += '</tbody>';

            table.append(html);

            /////Foofter
            var tfooter = '';
            tfooter += '<tfoot>';
            tfooter += '<tr>';
            tfooter += '<td colspan="' + notHiddenColumnsCount + '">';

            tfooter += '<input type="button" class="table-button" value="◄◄">';
            tfooter += '<input type="button" class="table-button" value="◄">';
            tfooter += 'Стр.';
            tfooter += '<input type="text"  class="table-text" value="' + x.Page + '">';
            tfooter += ' из ' + x.PageCount;
            tfooter += '<input type="button" class="table-button" value="►">';
            tfooter += '<input type="button" class="table-button" value="►►">';


            // Размер
            tfooter += '<select>';

            var avialablePageSizes = [5, 10, 20, 50, 100];

            $.each(avialablePageSizes,
                (i, e) => {
                    tfooter += '<option ' + (e === x.Size ? 'selected' : '')+ '>';
                    tfooter += e;
                    tfooter += '</option>';
                });

            tfooter += '</select>';
            //

            // Количество
            tfooter += '<span class="right">';
            tfooter += 'Показано: ' + ((x.Page - 1) * x.Size + 1) + ' - ' +
                (x.Page * x.Size < x.TotalCount ? x.Page * x.Size : x.TotalCount) + ' из ' + x.TotalCount;
            tfooter += '</span>';
            //



            tfooter += '</td>';
            tfooter += '</tr>';
            tfooter += '</tfoot>';

            
            var d = x;

            table.append(tfooter);

        }).fail(e => alert(e));
}

$(document).ready(() => {
    var storesColumns = [
        {
            label: '№',
            name: 'Id',
            key: true,
            type: 'int',
            autocompleteUrl: '',
            width: 60,
            align: 'left',
            hidden: false,
            search: true,
            sorting: true,
            edit: false
        },
        //{
        //    label: 'Создатель',
        //    name: 'CreateUserId',
        //    key: false,
        //    type: 'autocomplete',
        //    autocompleteUrl: '/Users',
        //    width: 60,
        //    align: 'left',
        //    hidden: false,
        //    search: true,
        //    edit: true
        //},
        {
            label: 'Название',
            name: 'Name',
            key: false,
            type: 'string',
            autocompleteUrl: '',
            width: 200,
            align: 'left',
            hidden: false,
            search: true,
            sorting: true,
            edit: true
        },
        {
            label: 'АПИ-ключ',
            name: 'ApiKey',
            key: false,
            type: 'string',
            autocompleteUrl: '',
            width: 100,
            align: 'left',
            hidden: false,
            search: true,
            sorting: true,
            edit: true
        },
        {
            label: 'АПИ-секрет',
            name: 'ApiSecret',
            key: false,
            type: 'string',
            autocompleteUrl: '',
            width: 150,
            align: 'left',
            hidden: false,
            search: true,
            sorting: true,
            edit: true
        },
        {
            label: 'Заблокирован',
            name: 'IsLocked',
            key: false,
            type: 'bool',
            autocompleteUrl: '',
            width: 90,
            align: 'center',
            hidden: false,
            search: true,
            sorting: true,
            edit: true
        },
        {
            label: 'Скрыт',
            name: 'IsDeleted',
            key: false,
            type: 'bool',
            autocompleteUrl: '',
            width: 90,
            align: 'center',
            hidden: false,
            search: true,
            sorting: true,
            edit: true
        },
        {
            label: 'Создан',
            name: 'CreateDate',
            key: false,
            type: 'datetime',
            autocompleteUrl: '',
            width: 110,
            align: 'center',
            hidden: false,
            search: true,
            sorting: true,
            edit: true
        },
        {
            label: 'Изменён',
            name: 'ModifyDate',
            key: false,
            type: 'datetime',
            autocompleteUrl: '',
            width: 110,
            align: 'center',
            hidden: false,
            search: true,
            sorting: true,
            edit: true
        },
        //{
        //    label: 'Удаление',
        //    name: '',
        //    key: false,
        //    type: 'datetime',
        //    autocompleteUrl: '',
        //    width: 110,
        //    align: 'center',
        //    hidden: false,
        //    search: false,
        //    sorting: false,
        //    edit: false
        //}
    ];
    var storesDataUrl = '/Stores/GetList';

    createTable(storesUi.storesTableWrapper, storesColumns, storesDataUrl);



    //storesUi.grid.jqGrid({
    //    url: '/Stores/Get',
    //    editurl: '/Stores/Edit',
    //    cellsubmit: 'Stores/Edit',
    //    colModel: [
    //        { label: '№', name: 'Id', width: 60, key: true, hidden: true },
    //        { label: 'Название', name: 'Name', width: 440, editable: true },
    //        { label: 'АПИ-ключ', name: 'ApiKey', width: 150, editable: true },
    //        { label: 'АПИ-секрет', name: 'ApiSecret', width: 200, editable: true },
    //        {
    //            label: 'Заблокирован', name: 'IsLocked', width: 80, align: 'center', stype: 'select', editable: true,
    //            searchoptions: { value: 'null:Все;true:Да;false:Нет' }, editoptions: { value: 'true:Да;false:Нет' },
    //            edittype: 'select', formatter: v => v ? 'Да' : 'Нет'
    //        },
    //        {
    //            label: 'Удалён', name: 'IsDeleted', width: 80, align: 'center', stype: 'select', editable: true,
    //            searchoptions: { value: 'null:Все;true:Да;false:Нет' }, editoptions: { value: 'true:Да;false:Нет' },
    //            edittype: 'select', formatter: v => v ? 'Да' : 'Нет'
    //        },
    //        {
    //            label: 'Создан', name: 'CreateDate', width: 100, align: 'center', formatter: 'date', sorttype: 'date',
    //            datefmt: 'dd.mm.yy',
    //            editable: true, searchoptions: dataOptions,
    //            editoptions: dataOptions
    //        },
    //        {
    //            label: 'Изменён', name: 'ModifyDate', width: 100, align: 'center', formatter: 'date', sorttype: 'date',
    //            datefmt: 'dd.mm.yy',
    //            editable: true, searchoptions: dataOptions,
    //            editoptions: dataOptions
    //        },
    //        {
    //            label: 'Действия', name: 'actions', width: 50, search: false, sortable: false, formatter: 'actions',
    //            formatoptions: {
    //                keys: true,
    //                editOptions: {},
    //                addOptions: {},
    //                delOptions: {}
    //            }
    //        }
    //    ],
    //    datatype: 'json',
    //    viewrecords: true,
    //    height: 'auto',
    //    rowNum: 10,
    //    rowList: [5, 10, 20, 50, 100],
    //    sortable: true,
    //    pager: '#jqGridPager'

    //})
    //.jqGrid('filterToolbar', { searchOperators: false })
    //.inlineNav('#jqGridPager', {
    //    edit: true, add: true, del: true, cancel: true,
    //    editParams: { keys: true },
    //    addParams: { keys: true }
    //    });

});