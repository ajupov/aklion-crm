﻿'use strict';

const prmNames = {
    page: 'Page',
    rows: 'Size',
    sort: 'SortingColumn',
    order: 'SortingOrder',
    search: 'IsSearch',
    nd: 'Timestamp',
    id: 'Id',
    oper: 'Operation',
    editoper: 'Editing',
    addoper: 'Creating',
    deloper: 'Deleting',
    totalrows: 'TotalCount'
}

const jsonReader = {
    root: 'Items',
    page: 'Page',
    total: 'PageCount',
    records: 'Size',
    repeatitems: true,
    id: 'Id'
}

const subTableOptions = {
    'plusicon': 'ui-icon-triangle-1-e',
    'minusicon': 'ui-icon-triangle-1-s',
    'openicon': 'ui-icon-arrowreturn-1-e',
    'reloadOnExpand': true,
    'selectOnExpand': true
}

const isDeletedFilters = {
    IsDeleted: false
};

function getColModel(columns, $table) {
    const colModel = [];

    $.each(columns,
        (index, column) => {
            colModel.push({
                key: column.Name === 'Id',
                name: column.Name.length > 0 ? column.Name : '',
                label: column.Label !== null && column.Label !== undefined && column.Label.length > 0
                    ? column.Label
                    : 'Без названия',
                width: column.Width > 0 ? column.Width : 100,
                align: column.Align !== null && column.Align !== undefined && column.Align.length > 0
                    ? column.Align
                    : column.Type === 'checkbox' ||
                    column.Type === 'select' ||
                    column.Type === 'date' ||
                    column.Type === 'datetime'
                    ? 'center'
                    : column.Type === 'money'
                    ? 'right'
                    : 'left',
                hidden: column.Hidden,
                datefmt: column.Type === 'datetime'
                    ? 'd.m.Y H:i:s'
                    : 'd.m.Y',
                formatter: column.Formatter !== null && column.Formatter !== undefined
                    ? column.Formatter
                    : column.Type === 'checkbox'
                    ? 'checkbox'
                    : column.Type === 'select'
                    ? 'select'
                    : column.Type === 'date' || column.Type === 'datetime'
                    ? 'date'
                    : column.Type === 'number'
                    ? 'integer'
                    : column.Type === 'email'
                    ? 'email'
                    : column.Type === 'money'
                    ? 'currency'
                    : column.Type === 'image'
                    ? 'image'
                    : e => e === undefined || e === null
                    ? ''
                    : e,
                unformat: column.Unformatter !== null && column.Formatter !== undefined
                    ? column.Unformatter
                    : null,
                formatoptions: column.Type === 'datetime'
                    ? { srcformat: 'd.m.Y h:i:s', newformat: 'd.m.Y H:i:s' }
                    : column.Type === 'date'
                    ? { srcformat: 'd.m.Y', newformat: 'd.m.Y' }
                    : column.Type === 'money'
                    ? { suffix: ' ₽' }
                    : undefined,
                editable: column.Editable,
                edittype: column.Type === 'checkbox'
                    ? 'checkbox'
                    : column.Type === 'select'
                    ? 'select'
                    : column.Type === 'textarea'
                    ? 'textarea'
                    : column.Type === 'image'
                    ? 'image'
                    : column.Type === 'file'
                    ? 'file'
                    : 'text',
                editoptions: {
                    value: column.Type === 'checkbox'
                        ? 'true:false'
                        : column.Type === 'select'
                        ? column.SelectValues.replace('0:;', '')
                        : [],
                    maxlength: column.MaxLength > 0 ? column.MaxLength : 0x7FFFFFFF,
                    dataInit: e => {
                        if (column.Type === 'date' || column.Type === 'datetime') {
                            initDatePicker(e, $table);
                        }
                        if (column.Type === 'autocomplete') {
                            initAutocomplete(e,
                                column.AutocompleteUrl,
                                column.AutocompleteHidden,
                                column.DependentFields);
                        }
                        if (column.Type === 'money') {
                            initMoney(e, false);
                        }
                    }
                },
                editrules: {
                    edithidden: column.EditHidden
                },
                search: column.Search,
                stype: column.Type === 'select' || column.Type === 'checkbox'
                    ? 'select'
                    : 'text',
                searchoptions: {
                    dataInit: e => {
                        if (column.Type === 'date' || column.Type === 'datetime') {
                            initDatePicker(e, $table);
                        }
                        if (column.Type === 'money') {
                            initMoney(e, true);
                        }
                    },
                    value: column.Type === 'select'
                        ? column.SelectValues
                        : column.Type === 'checkbox'
                        ? 'false:Нет;true:Да;null:Все'
                        : []
                },
                sortable: column.Sortable,
                sorttype: column.Type === 'date' || column.Type === 'datetime'
                    ? 'date'
                    : column.Type === 'number'
                    ? 'int'
                    : column.Type === 'money' || column.Type === 'double'
                    ? 'currency'
                    : 'text'
            });
        });

    return colModel;
}

function createPager($table,
    pager,
    isViewable,
    isCreatable,
    createUrl,
    isEditable,
    updateUrl,
    isDeletable,
    deleteUrl,
    isFilterable,
    additionalDataUrl,
    viewFormFilterParam,
    viewFormAdditionalFieldKey,
    viewFormAdditionalFieldValue,
    linkData) {
    $table.jqGrid('navGrid',
        pager,
        {
            view: isViewable,
            refresh: false,
            search: false,
            add: isCreatable === true,
            del: isDeletable === true,
            edit: isEditable === true,
            cloneToTop: true
        },
        {
            width: 'auto',
            recreateForm: true,
            closeOnEscape: true,
            resize: false,
            url: updateUrl,
            closeAfterEdit: true,
            errorTextFormat: e => e.responseJSON.Error,
            onInitializeForm: (formid) => {
                if (!linkData) {
                    return;
                }

                $.each(linkData,
                    (key, value) => {
                        $(formid[0].elements[key]).val(value);
                    });
            }
        },
        {
            width: 'auto',
            recreateForm: true,
            closeOnEscape: true,
            resize: false,
            url: createUrl,
            closeAfterAdd: true,
            clearAfterAdd: true,
            errorTextFormat: e => e.responseJSON.Error,
            onInitializeForm: (formid) => {
                if (!linkData) {
                    return;
                }

                $.each(linkData,
                    (key, value) => {
                        $(formid[0].elements[key]).val(value);
                    });
            }
        },
        {
            width: 'auto',
            recreateForm: true,
            closeOnEscape: true,
            resize: false,
            url: deleteUrl,
            errorTextFormat: e => e.responseJSON.Error
        },
        {
        },
        {
            width: 'auto',
            recreateForm: true,
            closeOnEscape: true,
            resize: false,
            viewPagerButtons: false,
            beforeShowForm: viewFormFilterParam !== null && viewFormFilterParam !== undefined
                ? form => {
                    const viewTableBody = $(form[0]).find('table>tbody');

                    viewTableBody.find('tr#trv_IsDeleted').remove();

                    const id = parseInt(viewTableBody.find('tr#trv_Id>td.DataTD>span').text());
                    const params = Object.assign({}, isDeletedFilters);

                    params[viewFormFilterParam] = id;

                    getJson(additionalDataUrl,
                        params,
                        result => {
                            $.each(result.Items,
                                (index, item) => {
                                    var key = item[viewFormAdditionalFieldKey];
                                    var value = item[viewFormAdditionalFieldValue];

                                    const html =
                                        `<tr class="FormData">
                                    <td class="CaptionTD form-view-label ui-widget-content" width="30%">
                                        <b>${key}</b>
                                    </td>
                                    <td class="DataTD form-view-data ui-helper-reset ui-widget-content">&nbsp;
                                        <span>${value}</span>
                                    </td>
                                </tr>`;
                                    viewTableBody.append(html);
                                });
                        });
                }
                : null
        });

    if (isFilterable) {
        $table.jqGrid('filterToolbar', {});
    }
}

function createTable(options) {
    const $table = $(options.Element);
    if ($table.hasClass('ui-jqgrid-btable')) {
        return;
    }

    const filters = getFilters();

    $table.jqGrid({
        url: options.DataUrl,
        datatype: 'json',
        rowNum: 10,
        rowList: [10, 20, 50, 100],
        height: 'auto',
        pager: options.Pager,
        width: '100%',
        sortname: options.SortingColumn !== null && options.SortingColumn !== undefined
            ? options.SortingColumn
            : 'CreateDate',
        sortorder: options.SortingColumn ? 'asc' : 'desc',
        caption: options.Title,
        viewrecords: true,
        colModel: getColModel(options.Columns, $table),
        sortable: true,
        search: true,
        postData: options.CanExtractFilters && filters !== null
            ? Object.assign({}, filters, isDeletedFilters)
            : isDeletedFilters,
        multiselect: options.Multiselect,
        onSelectRow: options.OnSelectRow !== null && options.OnSelectRow !== undefined
            ? options.OnSelectRow
            : undefined,
        prmNames: prmNames,
        jsonReader: jsonReader,
        subGrid: options.HasSubTable,
        subGridOptions: options.HasSubTable ? subTableOptions : null,
        subGridRowExpanded: options.HasSubTable
            ? (rowId, id) => {
                const subTableId = `${rowId}-sub-table`;
                const subTablePager = `${rowId}-sub-table-pager`;
                const subTableSelector = `#${subTableId}`;
                const subTablePagerSelector = `#${subTablePager}`;
                $(`#${rowId}`)
                    .html(`<table id='${subTableId}' class='scroll'></table><div id='${subTablePager}'></div>`);

                const $subTable = $(subTableSelector);

                $subTable.jqGrid({
                    url: options.SubTableDataUrl,
                    datatype: 'json',
                    rowNum: 10,
                    rowList: [10, 20, 50, 100],
                    height: 'auto',
                    pager: options.HasSubTablePager ? subTablePagerSelector : '',
                    width: '100%',
                    sortname: options.SubTableSortingColumn !== null && options.SubTableSortingColumn !== undefined
                        ? options.SubTableSortingColumn
                        : 'CreateDate',
                    sortorder: options.SubTableSortingColumn ? 'asc' : 'desc',
                    viewrecords: options.HasSubTablePager,
                    colModel: getColModel(options.SubTableColumns, $table),
                    sortable: true,
                    search: true,
                    prmNames: prmNames,
                    jsonReader: jsonReader,
                    serializeGridData: data => {
                        data[options.SubTableLink] = parseInt(id);
                        return data;
                    }
                });

                const linkData = {};

                linkData[options.SubTableLink] = parseInt(id);

                if (options.HasSubTablePager) {
                    createPager($subTable,
                        subTablePagerSelector,
                        options.SubTableIsViewable,
                        options.SubTableIsCreatable,
                        options.SubTableCreateUrl,
                        options.SubTableIsEditable,
                        options.SubTableUpdateUrl,
                        options.SubTableIsDeletable,
                        options.SubTableDeleteUrl,
                        options.SubTableIsFilterable,
                        null,
                        null,
                        null,
                        null,
                        linkData);
                }
            }
            : null
    });

    createPager($table,
        options.Pager,
        options.IsViewable,
        options.IsCreatable,
        options.CreateUrl,
        options.IsEditable,
        options.UpdateUrl,
        options.IsDeletable,
        options.DeleteUrl,
        options.IsFilterable,
        options.SubTableDataUrl,
        options.ViewFormFilterParam,
        options.ViewFormAdditionalFieldKey,
        options.ViewFormAdditionalFieldValue);

    createFilterForm($table, options);
}

function createFilterForm($table, options) {
    if (options.SearchDialog) {
        $table.navButtonAdd(options.Pager,
            {
                buttonicon: 'ui-icon-search',
                title: 'Фильтр',
                caption: '',
                position: 'last',
                onClickButton: () => {
                    $(options.SearchDialog).dialog('open');
                }
            });

        let html = '<form>';

        $.each(options.Columns,
            (index, item) => {
                if (!item.Hidden || item.FilterHidden) {
                    html += `<label class="search-dialog-label">${item.Label}</label>`;

                    if (item.Type === 'text') {
                        html += `<input type="text" length="${item.MaxLength}" name="${item.Name}"
                                class="search-form-text ui-widget-content ui-corner-all search-dialog-field">`;
                    } else if (item.Type === 'date' || item.Type === 'datetime') {
                        html += `<input type="text" name="Min${item.Name}"
                                class="datepicker ui-widget-content ui-corner-all search-dialog-date-field">
                        <label class="search-dialog-date-label">-</label>
                        <input type="text"  name="Max${item.Name}"
                            class="datepicker ui-widget-content ui-corner-all search-dialog-date-field">`;
                    } else if (item.Type === 'filterlist') {
                        html += `<div class="search-dialog-filterlist-wrapper" >`;
                        html += `<div class="search-dialog-filterlist-item">`;
                        html += `<select name="${item.Name}[0].Key"
                        class="ui-widget-content ui-corner-all search-dialog-fixed-select search-dialog-filterlist-select">`;

                        $.get({
                            url: item.FilterSelectDataUrl,
                            async: false,
                            dataType: 'json',
                            success: result => {
                                $.each(result,
                                    (key, value) => {
                                        html += `<option value=${value}>${key}</option>`;
                                    });
                            }
                        });

                        html += '</select>';
                        html += `<input type="text" length="${item.MaxLength}" name="${item.Name}[0].Value"
                        class="search-form-text ui-widget-content ui-corner-all search-dialog-field search-dialog-filterlist-input">`;
                        html += '</div>';
                        html += '</div>';
                        html += '<a href="#" class="search-dialog-add-link">';
                        html += 'Добавить';
                        html += '</a>';
                    } else if (item.Type === 'checkbox') {
                        html += `<select name="${item.Name
                            }" class="ui-widget-content ui-corner-all search-dialog-select">`;
                        html += '<option value="null"></option>';
                        html += '<option value="true">Да</option>';
                        html += '<option value="false">Нет</option>';
                        html += '</select>';
                        html += '<br>';
                    }
                }
            });

        html += '<br><br>';
        html += '</form>';

        html += '<a class="fm-button ui-state-default ui-corner-all fm-button-icon-left search-dialog-filter-link">';
        html += 'Искать';
        html += '<span class="ui-icon ui-icon-search"></span>';
        html += '</a>';

        html += '<a class="fm-button ui-state-default ui-corner-all fm-button-icon-left search-dialog-reset-link">';
        html += 'Сбросить';
        html += '<span class="ui-icon ui-icon-close"></span>';
        html += '</a>';

        $(options.SearchDialog).html(html);

        initDatePicker($(options.SearchDialog).find('.datepicker'));

        $(options.SearchDialog).find('a.search-dialog-add-link').click(e => {
            e.preventDefault();

            const item = $(options.SearchDialog).find('.search-dialog-filterlist-item').first().clone();
            item.find('input').val('');
            item.find('select').prop('selectedIndex', 0);
            item.append(
                '<a href="#" class="search-dialog-remove-link"><span class="ui-icon ui-icon-closethick"></span></a>');

            $(options.SearchDialog).find('.search-dialog-filterlist-wrapper').append(item);

            $('.search-dialog-remove-link').click(ei => {
                ei.preventDefault();

                $(ei.target).closest('div.search-dialog-filterlist-item').remove();
            });
        });

        $(options.SearchDialog).find('a.search-dialog-remove-link').click(e => {
            e.preventDefault();

            const item = $(options.SearchDialog).find('.search-dialog-filterlist-item').first().clone();

            $(options.SearchDialog).find('.search-dialog-filterlist-wrapper').append(item);
        });

        $(options.SearchDialog).find('a.search-dialog-reset-link').click(e => {
            e.preventDefault();

            const item = $(options.SearchDialog).find('.search-dialog-filterlist-item').first().clone();
            $(options.SearchDialog).find('.search-dialog-filterlist-wrapper').html('');
            $(options.SearchDialog).find('.search-dialog-filterlist-wrapper').append(item);

            $(options.SearchDialog).find('input').val('');
            $(options.SearchDialog).find('select').prop('selectedIndex', 0);

            $table.setGridParam({ postData: {} }).trigger('reloadGrid');
        });

        $(options.SearchDialog).find('a.search-dialog-filter-link').click(e => {
            e.preventDefault();

            var data = $(options.SearchDialog).find('form').serializeArray();

            let keyIndex = 0;
            let valueIndex = 0;

            $.each(data,
                (index, item) => {
                    if (item.name === 'IsDeleted') {
                        switch (item.value) {
                        case 'true':
                            item.value = true;
                            break;

                        case 'false':
                            item.value = false;
                            break;
                        default:
                            item.value = null;
                            break;
                        }
                    }

                    if (item.name.indexOf('[0].Key') >= 0) {
                        item.name = item.name.replace('[0]', `[${keyIndex}]`);
                        keyIndex++;
                    }

                    if (item.name.indexOf('[0].Value') >= 0) {
                        item.name = item.name.replace('[0]', `[${valueIndex}]`);
                        valueIndex++;
                    }
                });

            const param = $table.jqGrid('getGridParam');

            data.push({ 'name': 'SortingColumn', 'value': param.sortname });
            data.push({ 'name': 'SortingOrder', 'value': param.sortorder });

            $table.setGridParam({ postData: data }).trigger('reloadGrid');
        });

        $(options.SearchDialog).dialog({ autoOpen: false, modal: true, resizable: false });
    }
}