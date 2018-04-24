function initProductsTable() {
    createTable({
        Element: '#products-table',
        Pager: '#products-table-pagination',
        SearchDialog: '#products-table-search',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        CanExtractFilters: true,
        DataUrl: '/Products/GetList',
        CreateUrl: '/Products/Create',
        UpdateUrl: '/Products/Update',
        DeleteUrl: '/Products/Delete',
        ViewFormFilterParam: 'ProductId',
        ViewFormAdditionalFieldKey: 'AttributeName',
        ViewFormAdditionalFieldValue: 'Value',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Hidden: true },
            { Name: 'Name', Label: 'Наименование', Type: 'text', Width: 300, Editable: true, MaxLength: 256 },
            {
                Name: 'StatusId',
                Label: 'Статус',
                Type: 'select',
                Editable: true,
                SelectValues: getSelectValues('/ProductStatuses/GetSelect'),
                Width: 100
            },
            { Name: 'ParentId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'ParentProductName',
                Label: 'Родительский продукт',
                Type: 'autocomplete',
                Editable: true,
                Width: 300,
                AutocompleteUrl: '/Products/GetAutocomplete',
                AutocompleteHidden: 'ParentProductId'
            },
            { Name: 'Price', Label: 'Стоимость', Type: 'money', Width: 80, Editable: true },
            { Name: 'VendorCode', Label: 'Артикул', Type: 'text', Width: 120, Editable: true, MaxLength: 16 },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 120 },
            {
                Name: 'IsDeleted',
                Label: 'Удалён',
                Type: 'checkbox',
                Align: 'center',
                Width: 100,
                Formatter: productIsDeletedFormatter,
                Sortable: false
            },
            {
                Name: 'Attributes',
                Label: 'Атрибуты',
                Type: 'filterlist',
                FilterSelectDataUrl: '/ProductAttributes/GetSelect',
                Hidden: true,
                FilterHidden: true
            }
        ],

        HasSubTable: true,
        HasSubTablePager: true,
        SubTableLink: 'ProductId',
        SubTableDataUrl: '/ProductImageKeyLinks/GetList',
        SubTableIsCreatable: true,
        SubTableCreateUrl: '/ProductImageKeyLinks/Create',
        SubTableIsEditable: true,
        SubTableUpdateUrl: '/ProductImageKeyLinks/Update',
        SubTableIsDeletable: true,
        SubTableDeleteUrl: '/ProductImageKeyLinks/Delete',
        SubTableIsFilterable: true,
        SubTableSortingColumn: 'KeyId',
        SubTableColumns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60, Hidden: true },
            {
                Name: 'KeyId',
                Label: 'Ключ',
                Type: 'select',
                Editable: true,
                Sortable: false,
                SelectValues: getSelectValues('/ProductImageKeys/GetSelect'),
                Width: 120
            },
            {
                Name: 'SetValue',
                Label: 'Установить изображение',
                Type: 'custom',
                Width: 155,
                Align: 'center',
                Formatter: loadImageDialogFormatter,
                Sortable: false,
                Search: false
            },
            {
                Name: 'Base64Value',
                Label: 'Изображение',
                Type: 'image',
                Width: 100,
                Search: false,
                Sortable: false,
                Editable: false,
                EditHidden: true,
                Formatter: base64ImageValueFormatter
            },
            { Name: 'ProductId', Type: 'number', Hidden: true, Editable: true },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 }
        ],


       /* 
        HasSubTable: true,
        HasSubTablePager: true,
        SubTableLink: 'ProductId',
        SubTableDataUrl: '/ProductAttributeLinks/GetList',
        SubTableIsCreatable: true,
        SubTableCreateUrl: '/ProductAttributeLinks/Create',
        SubTableIsEditable: true,
        SubTableUpdateUrl: '/ProductAttributeLinks/Update',
        SubTableIsDeletable: true,
        SubTableDeleteUrl: '/ProductAttributeLinks/Delete',
        SubTableIsFilterable: true,
        SubTableSortingColumn: 'AttributeName',
        SubTableColumns: [
            { Name: 'AttributeId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'AttributeName',
                Label: 'Название атрибута',
                Type: 'autocomplete',
                Editable: true,
                Width: 200,
                AutocompleteUrl: '/ProductAttributes/GetAutocomplete',
                AutocompleteHidden: 'AttributeId'
            },
            { Name: 'ProductId', Type: 'number', Hidden: true, Editable: true },
            { Name: 'Value', Label: 'Значение', Editable: true, EditHidden: true, Width: 424 }
        ],
        
        */



        OnSelectRow: id => {
            $('#products-images-table').jqGrid('setGridParam', { postData: { ProductId: id } }).trigger('reloadGrid');
        }
    });

    //createTable({
    //    Title: 'Изображения продуктов',
    //    Element: '#products-images-table',
    //    Pager: '#products-images-table-pagination',
    //    IsViewable: true,
    //    IsEditable: true,
    //    IsCreatable: true,
    //    IsDeletable: true,
    //    IsFilterable: true,
    //    IsWithFile: true,
    //    DataUrl: '/ProductImageKeyLinks/GetList',
    //    CreateUrl: '/ProductImageKeyLinks/Create',
    //    UpdateUrl: '/ProductImageKeyLinks/Update',
    //    DeleteUrl: '/ProductImageKeyLinks/Delete',
    //    Columns: [
    //        { Name: 'Id', Label: '№', Type: 'number', Width: 60, Hidden: true },
    //        { Name: 'ProductId', Type: 'number', Hidden: true, Editable: true },
    //        {
    //            Name: 'ProductName',
    //            Label: 'Продукт',
    //            Type: 'autocomplete',
    //            Editable: true,
    //            Width: 120,
    //            AutocompleteUrl: '/Products/GetForAutocompleteByNamePattern',
    //            AutocompleteHidden: 'ProductId',
    //            Formatter: productLinkFormatter,
    //            Unformatter: linkUnFormatter
    //        },
    //        {
    //            Name: 'KeyId',
    //            Label: 'Ключ',
    //            Type: 'select',
    //            Editable: true,
    //            Sortable: false,
    //            SelectValues: getSelectValues('/ProductImageKeys/GetSelect'),
    //            Width: 120
    //        },
    //        {
    //            Name: 'SetValue',
    //            Label: 'Установить изображение',
    //            Type: 'custom',
    //            Width: 155,
    //            Align: 'center',
    //            Formatter: loadImageDialogFormatter,
    //            Sortable: false,
    //            Search: false
    //        },
    //        {
    //            Name: 'Base64Value',
    //            Label: 'Изображение',
    //            Type: 'image',
    //            Width: 100,
    //            Search: false,
    //            Sortable: false,
    //            Editable: false,
    //            EditHidden: true,
    //            Formatter: base64ImageValueFormatter
    //        },
    //        { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 50, Editable: true, Sortable: false },
    //        { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
    //        { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
    //    ]
    //});
}

function initAttributesTable() {
    createTable({
        Element: '#attributes-table',
        Pager: '#attributes-table-pagination',
        IsFilterable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        SortingColumn: 'Name',
        DataUrl: '/ProductAttributes/GetList',
        CreateUrl: '/ProductAttributes/Create',
        UpdateUrl: '/ProductAttributes/Update',
        DeleteUrl: '/ProductAttributes/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Hidden: true },
            { Name: 'Key', Label: 'Ключ', Type: 'text', Width: 150, Editable: true, MaxLength: 256 },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 300, Editable: true, MaxLength: 256 }
        ]
    });
}

function initOthersTable() {
    createTable({
        Title: 'Статус продукта',
        Element: '#products-statuses-table',
        Pager: '#products-statuses-table-pagination',
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        DataUrl: '/ProductStatuses/GetList',
        CreateUrl: '/ProductStatuses/Create',
        UpdateUrl: '/ProductStatuses/Update',
        DeleteUrl: '/ProductStatuses/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Hidden: true },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 300, Editable: true, MaxLength: 256 }
        ]
    });

    createTable({
        Title: 'Ключи изображений продукта',
        Element: '#images-table',
        Pager: '#images-table-pagination',
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/ProductImageKeys/GetList',
        CreateUrl: '/ProductImageKeys/Create',
        UpdateUrl: '/ProductImageKeys/Update',
        DeleteUrl: '/ProductImageKeys/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Hidden: true },
            { Name: 'Key', Label: 'Ключ', Type: 'text', Width: 150, Editable: true, MaxLength: 256 },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 300, Editable: true, MaxLength: 256 }
        ]
    });
}

function loadImageDialogFormatter(value, options, data) {
    return `<button onclick="loadImageDialogShow(event, ${data.Id
        });" class="cell-button" title="Открыть окно загрузки изображения">
                Установить
            </button>`;
}

function base64ImageValueFormatter(value) {
    return `<img style="width:100px; height:auto;" src="data:image/jpg;base64, ${value}"></img>`;
}

function loadImageDialogShow(event, id) {
    event.preventDefault();
    event.stopPropagation();

    $('#product-id').val(id);
    $('#load-image-dialog').dialog('open');
}


function productIsDeletedFormatter(value, options, data) {
    return `<button onclick="deleteProduct(event, ${data.Id});" class="cell-button" 
                title="${value ? 'Восстановить' : 'Удалить'}">${value ? 'Восстановить' : 'Удалить'}
            </button>`;
}

function deleteProduct(event, id) {
    event.preventDefault();
    event.stopPropagation();

    postText('/Products/Delete',
        { id: id },
        () => {
            $('#products-table').trigger('reloadGrid');
        });
}