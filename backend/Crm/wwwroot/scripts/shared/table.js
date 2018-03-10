'use strict';

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
                            initAutocomplete(e, column.AutocompleteUrl, column.AutocompleteHidden, column.DependentFields);
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

function createPager($table, pager, isViewable, isCreatable, createUrl, isEditable, updateUrl, isDeletable, deleteUrl, isFilterable) {
    $table.jqGrid('navGrid',
        pager,
        {
            view: isViewable,
            refresh: true,
            search: false,
            add: isCreatable,
            del: isDeletable,
            edit: isEditable,
            afterRefresh: () => $table[0].triggerToolbar()
        },
        {
            width: 'auto',
            recreateForm: true,
            closeOnEscape: true,
            resize: false,
            url: updateUrl,
            closeAfterEdit: true,
            errorTextFormat: e => e.responseJSON.Error
        },
        {
            width: 'auto',
            recreateForm: true,
            closeOnEscape: true,
            resize: false,
            url: createUrl,
            closeAfterAdd: true,
            clearAfterAdd: true,
            errorTextFormat: e => e.responseJSON.Error
        },
        {
            width: 'auto',
            recreateForm: true,
            closeOnEscape: true,
            resize: false,
            url: deleteUrl,
            errorTextFormat: e => e.responseJSON.Error
        },
        {},
        {
            width: 'auto',
            recreateForm: true,
            closeOnEscape: true,
            resize: false,
            viewPagerButtons: false
        });

    if (isFilterable) {
        $table.jqGrid('filterToolbar', {});
    }
}

function createTable(options) {
    const $table = $(options.Element);
    const filters = getFilters();

    $table.jqGrid({
        url: options.DataUrl,
        datatype: 'json',
        rowNum: 10,
        rowList: [10, 20, 50, 100],
        height: 'auto',
        pager: options.Pager,
        width: '100%',
        sortname: options.SortingColumn !== null && options.SortingColumn !== undefined ? options.SortingColumn : 'CreateDate',
        sortorder: options.SortingColumn ? 'asc' : 'desc',
        caption: options.Title,
        viewrecords: true,
        colModel: getColModel(options.Columns, $table),
        sortable: true,
        search: true,
        postData: options.CanExtractFilters && filters !== null ? Object.assign({}, filters, isDeletedFilters) : isDeletedFilters,
        multiselect: options.Multiselect,
        onSelectRow: options.OnSelectRow !== null && options.OnSelectRow !== undefined ? options.OnSelectRow : undefined,
        prmNames: prmNames,
        jsonReader: jsonReader,

        subGrid: options.HasSubTable,
        subGridOptions: options.HasSubTable ? subTableOptions : null,
        subGridRowExpanded: options.HasSubTable
            ? (id, rowId) => {
                const subTableId = `SubTable_${id}`;
                const subTablePager = `SubTablePager_${id}`;
                const subTableSelector = `#${subTableId}`;
                const subTablePagerSelector = `#${subTablePager}`;
                const $subTable = $(subTableSelector);

                $subTable.html(`<table id='${subTableId}' class='scroll'></table><div id='${subTablePager}'></div>`);

                $subTable.jqGrid({
                    url: options.SubTableDataUrl,
                    datatype: 'json',
                    rowNum: 100,
                    //rowList: [10, 20, 50, 100],
                    //height: 'auto',
                    pager: options.Pager,
                    //width: '100%',
                    sortname: options.SubTableSortingColumn !== null && options.SubTableSortingColumn !== undefined
                        ? options.SubTableSortingColumn : 'CreateDate',
                    sortorder: options.SubTableSortingColumn ? 'asc' : 'desc',
                    //caption: options.Title,
                    //viewrecords: true,
                    colModel: getColModel(options.SubTableColumns, $table),
                    //sortable: true,
                    //search: true,
                    //postData: options.CanExtractFilters && filters !== null ? Object.assign({}, filters, isDeletedFilters) : isDeletedFilters,
                    //multiselect: options.Multiselect,
                    //onSelectRow: options.OnSelectRow !== null && options.OnSelectRow !== undefined ? options.OnSelectRow : undefined,
                    prmNames: prmNames,
                    jsonReader: jsonReader
                    });

                    createPager($subTable, subTablePagerSelector, options.SubTableIsViewable, options.SubTableIsCreatable,
                        options.SubTableCreateUrl, options.SubTableIsEditable, options.SubTableUpdateUrl,
                        options.SubTableIsDeletable, options.SubTableDeleteUrl, options.SubTableIsFilterable);
        } : null
    });

    createPager($table, options.Pager, options.IsViewable, options.IsCreatable, options.CreateUrl,
        options.IsEditable, options.UpdateUrl, options.IsDeletable, options.DeleteUrl, options.IsFilterable);
}