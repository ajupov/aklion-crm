'use strict';

function createTable(options) {
    const $table = $(options.Element);
    const filters = getFilters();
    const colModel = [];

    $.each(options.Columns,
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
                hidden: column.Hidden === true,
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
                    : e => e === undefined || e === null
                    ? ''
                    : e,
                unformat: column.Unformatter !== null && column.Formatter !== undefined
                    ? column.Unformatter
                    : null,
                formatoptions: column.Type === 'datetime'
                    ? { srcformat: 'Y-m-d h:i:s', newformat: 'd.m.Y H:i:s' }
                    : column.Type === 'date'
                    ? { srcformat: 'Y-m-d', newformat: 'd.m.Y' }
                    : column.Type === 'money'
                    ? { suffix: ' ₽' }
                    : undefined,
                editable: column.Editable === true,
                edittype: column.Type === 'checkbox'
                    ? 'checkbox'
                    : column.Type === 'select'
                    ? 'select'
                    : column.Type === 'textarea'
                    ? 'textarea'
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
                            initAutocomplete(e, column.AutocompleteUrl, column.AutocompleteHidden, column.DependentFields);
                        }
                        if (column.Type === 'money') {
                            initMoney(e);
                        }
                    }
                },
                editrules: {
                    edithidden: column.EditHidden === true
                },
                search: column.Search === false ? false : true,
                stype: column.Type === 'select' || column.Type === 'checkbox'
                    ? 'select'
                    : 'text',
                searchoptions: {
                    dataInit: e => {
                        if (column.Type === 'date' || column.Type === 'datetime') {
                            initDatePicker(e, $table);
                        }
                        if (column.Type === 'money') {
                            initMoney(e);
                        }
                    },
                    value: column.Type === 'select'
                        ? column.SelectValues
                        : column.Type === 'checkbox'
                        ? 'null:;false:Нет;true:Да'
                        : []
                },
                sortable: column.Sortable === false ? false : true,
                sorttype: column.Type === 'date' || column.Type === 'datetime'
                    ? 'date'
                    : column.Type === 'number'
                    ? 'int'
                    : column.Type === 'money' || column.Type === 'double'
                    ? 'currency'
                    : 'text'
            });
        });

    $table.jqGrid({
        url: options.DataUrl,
        datatype: 'json',
        rowNum: 10,
        rowList: [10, 20, 50, 100],
        height: 'auto',
        pager: options.Pager,
        width: '100%',
        sortname: 'CreateDate',
        sortorder: 'desc',
        caption: options.Title,
        viewrecords: true,
        colModel: colModel,
        sortable: true,
        search: filters !== null,
        postData: filters,
        multiselect: options.Multiselect === true,
        onSelectRow: options.OnSelectRow !== null && options.OnSelectRow !== undefined ? options.OnSelectRow : undefined,
        prmNames: {
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
        },
        jsonReader: {
            root: 'Items',
            page: 'Page',
            total: 'PageCount',
            records: 'Size',
            repeatitems: true,
            id: 'Id'
        }
    });

    $table.jqGrid('navGrid',
        options.Pager,
        {
            view: options.IsViewable === true,
            refresh: false,
            search: false,
            add: options.IsCreatable === true,
            del: options.IsDeletable === true,
            edit: options.IsEditable === true
        },
        {
            width: 'auto',
            recreateForm: true,
            closeOnEscape: true,
            resize: false,
            url: options.UpdateUrl,
            closeAfterEdit: true,
            errorTextFormat: e => e.responseJSON.Error
        },
        {
            width: 'auto',
            recreateForm: true,
            closeOnEscape: true,
            resize: false,
            url: options.CreateUrl,
            closeAfterAdd: true,
            clearAfterAdd: true,
            errorTextFormat: e => e.responseJSON.Error
        },
        {
            width: 'auto',
            recreateForm: true,
            closeOnEscape: true,
            resize: false,
            url: options.DeleteUrl,
            errorTextFormat: e => e.responseJSON.Error
        },
        {},
        {
            width: 'auto',
            recreateForm: true,
            closeOnEscape: true,
            resize: false,
            viewPagerButtons: false
        }
    );

    if (options.IsFilterable === true) {
        $table.jqGrid('filterToolbar', {});
    }
}