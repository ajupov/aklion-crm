'use strict';

function createTable(options) {
    options.Element.jqGrid({
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
        colModel: options.Columns,
        sortable: true,
        multiselect: true,
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

    options.Element.jqGrid('navGrid', options.Pager,
        { view: options.IsViewable, refresh: false, search: false, add: options.IsCreatable, del: options.IsDeletable, edit: options.IsEditable },
        {
            width: 'auto', recreateForm: true, closeOnEscape: true, resize: true, url: options.UpdateUrl, closeAfterEdit: true, errorTextFormat: e => e.responseJSON.Error },
        { width: 'auto', recreateForm: true, closeOnEscape: true, resize: true, url: options.CreateUrl, closeAfterAdd: true, clearAfterAdd: true, errorTextFormat: e => e.responseJSON.Error },
        { width: 'auto', recreateForm: true, closeOnEscape: true, resize: true, url: options.DeleteUrl, errorTextFormat: e => e.responseJSON.Error },
        { },
        { width: 500, recreateForm: true, closeOnEscape: true, resize: true }
    );

    if (options.IsFilterable) {
        options.Element.jqGrid('filterToolbar', {});
    }
}

function addSubGridOptions(options, url, colModel) {
    options.subGrid = true;
    options.subGridUrl = url;
    options.subGridModel = colModel;

    //subGridUrl: 'subgrid.php?q=2',
    //subGridModel: [{
    //        name: ['No', 'Item', 'Qty', 'Unit', 'Line Total'],
    //        width: [55, 200, 80, 80, 80]
    //    }
    //],
}