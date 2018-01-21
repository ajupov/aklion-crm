'use strict';

function initProductsTable() {
    createTable({
        Title: 'Продукты',
        Element: '#products-table',
        Pager: '#products-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        CanExtractFilters: true,
        DataUrl: '/Administration/Products/GetList',
        CreateUrl: '/Administration/Products/Create',
        UpdateUrl: '/Administration/Products/Update',
        DeleteUrl: '/Administration/Products/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 35 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Магазин', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId',
                Formatter: administrationStoreLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'ParentId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'ParentName', Label: 'Родительский продукт', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Products/GetForAutocompleteByNamePattern', AutocompleteHidden: 'ParentId',
                DependentFields: ['StoreId']
            },
            {
                Name: 'StatusId', Label: 'Статус', Type: 'select', Editable: true, Sortable: false,
                SelectValues: getSelectValues('/Administration/ProductStatuses/GetForSelect'), Width: 120
            },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'Price', Label: 'Стоимость', Type: 'money', Width: 80, Editable: true },
            { Name: 'VendorCode', Label: 'Артикул', Type: 'text', Width: 120, Editable: true, MaxLength: 16 },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 45, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ],
        OnSelectRow: id => {
            $('#products-attributes-table').jqGrid('setGridParam', { postData: { ProductId: id } }).trigger('reloadGrid');
            $('#products-images-table').jqGrid('setGridParam', { postData: { ProductId: id } }).trigger('reloadGrid');
        }
    });

    createTable({
        Title: 'Атрибуты продуктов',
        Element: '#products-attributes-table',
        Pager: '#products-attributes-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/Administration/ProductAttributeLinks/GetList',
        CreateUrl: '/Administration/ProductAttributeLinks/Create',
        UpdateUrl: '/Administration/ProductAttributeLinks/Update',
        DeleteUrl: '/Administration/ProductAttributeLinks/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 35 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Магазин', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId',
                Formatter: administrationStoreLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'ProductId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'ProductName', Label: 'Продукт', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Products/GetForAutocompleteByNamePattern', AutocompleteHidden: 'ProductId',
                DependentFields: ['StoreId'], Formatter: administrationProductLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'AttributeId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'AttributeDescription', Label: 'Атрибут', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/ProductAttributes/GetForAutocompleteByDescriptionPattern',
                AutocompleteHidden: 'AttributeId', DependentFields: ['StoreId']
            },
            { Name: 'Value', Label: 'Значение', Width: 250, Editable: true, EditHidden: true },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 45, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ]
    });

    createTable({
        Title: 'Изображения продуктов',
        Element: '#products-images-table',
        Pager: '#products-images-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        IsWithFile: true,
        DataUrl: '/Administration/ProductImageKeyLinks/GetList',
        CreateUrl: '/Administration/ProductImageKeyLinks/Create',
        UpdateUrl: '/Administration/ProductImageKeyLinks/Update',
        DeleteUrl: '/Administration/ProductImageKeyLinks/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 35 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Магазин', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId',
                Formatter: administrationStoreLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'ProductId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'ProductName', Label: 'Продукт', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Products/GetForAutocompleteByNamePattern', AutocompleteHidden: 'ProductId',
                DependentFields: ['StoreId'], Formatter: administrationProductLinkFormatter, Unformatter: linkUnFormatter
            },
            {
                Name: 'KeyId', Label: 'Ключ', Type: 'select', Editable: true, Sortable: false,
                SelectValues: getSelectValues('/Administration/ProductImageKeys/GetForSelect'), Width: 120
            },
            {
                Name: 'SetValue', Label: 'Установить изображение', Type: 'custom', Width: 150, Align: 'center',
                Formatter: loadImageDialogFormatter, Sortable: false, Search: false
            },
            {
                Name: 'Value', Label: 'Изображение', Type: 'file', Width: 250, Search: false, Sortable: false,
                Editable: false, EditHidden: true
            },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 45, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ]
    });
}

function initAttributesTable() {
    createTable({
        Title: 'Атрибуты',
        Element: '#attributes-table',
        Pager: '#attributes-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/Administration/ProductAttributes/GetList',
        CreateUrl: '/Administration/ProductAttributes/Create',
        UpdateUrl: '/Administration/ProductAttributes/Update',
        DeleteUrl: '/Administration/ProductAttributes/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 35 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Магазин', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId',
                Formatter: administrationStoreLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'Description', Label: 'Описание', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 45, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ]
    });
}

function initOthersTable() {
    createTable({
        Title: 'Статус продукта',
        Element: '#products-statuses-table',
        Pager: '#products-statuses-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/Administration/ProductStatuses/GetList',
        CreateUrl: '/Administration/ProductStatuses/Create',
        UpdateUrl: '/Administration/ProductStatuses/Update',
        DeleteUrl: '/Administration/ProductStatuses/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 35 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Магазин', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId',
                Formatter: administrationStoreLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ]
    });

    createTable({
        Title: 'Ключи изображений продукта',
        Element: '#images-table',
        Pager: '#images-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/Administration/ProductImageKeys/GetList',
        CreateUrl: '/Administration/ProductImageKeys/Create',
        UpdateUrl: '/Administration/ProductImageKeys/Update',
        DeleteUrl: '/Administration/ProductImageKeys/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 35 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Магазин', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId',
                Formatter: administrationStoreLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'Description', Label: 'Описание', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 45, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ]
    });
}

function loadImageDialogFormatter(value, options, data) {
    return `<button onclick="loadImageDialogShow(event, ${data.Id});" class="cell-button" title="Открыть окно загрузки изображения">
                Установить
            </button>`;
}

function loadImageDialogShow(event, id) {
    event.preventDefault();
    event.stopPropagation();

    $('#load-image-dialog').dialog('open');

    //{
    //    Name: 'ImageFile', Label: 'Файл', Type: 'file', Hidden: true, Editable: true, EditHidden: true
    //},
    //https://stackoverflow.com/questions/39354100/unable-to-insert-file-stream-into-sql-filetable-using-dapper-net

    //postText('/Administration/Stores/GenerateApiSecret', { id: id }, result => {
    //    if (result !== null && result !== undefined && result.length > 0) {
    //        const formField = $('#ViewGrid_stores-table td#v_ApiSecret span');
    //        formField.html(result);
    //        $('#stores-table').trigger('reloadGrid');
    //    }
    //});
}