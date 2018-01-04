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
        DataUrl: '/Administration/Products/GetList',
        CreateUrl: '/Administration/Products/Create',
        UpdateUrl: '/Administration/Products/Update',
        DeleteUrl: '/Administration/Products/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Название магазина', Type: 'autocomplete', Editable: true, Width: 130,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId',
                Formatter: administrationStoreLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'ParentId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'ParentName', Label: 'Родительский продукт', Type: 'autocomplete', Editable: true, Width: 130,
                AutocompleteUrl: '/Administration/Products/GetForAutocompleteByNamePattern', AutocompleteHidden: 'ParentId',
                DependentFields: ['StoreId']
            },
            {
                Name: 'StatusId', Label: 'Статус', Type: 'select', Editable: true, Sortable: false,
                SelectValues: getSelectValues('/Administration/ProductStatuses/GetForSelect'), Width: 240
            },
            { Name: 'Name', Label: 'Имя', Type: 'text', Width: 130, Editable: true, MaxLength: 256 },
            { Name: 'Price', Label: 'Стоимость', Type: 'money', Width: 140, Editable: true },
            { Name: 'VendorCode', Label: 'Артикул', Type: 'text', Width: 100, Editable: true, MaxLength: 16 },
            { Name: 'IsDeleted', Label: 'Удален', Type: 'checkbox', Width: 50, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 100 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 100 }
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
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Название магазина', Type: 'autocomplete', Editable: true, Width: 130,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId',
                Formatter: administrationStoreLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'Name', Label: 'Имя', Type: 'text', Width: 140, Editable: true, MaxLength: 256 },
            { Name: 'Description', Label: 'Описание', Type: 'text', Width: 140, Editable: true, MaxLength: 256 },
            { Name: 'IsDeleted', Label: 'Удален', Type: 'checkbox', Width: 50, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 100 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 100 }
        ],
        OnSelectRow: id => {
            $('#products-attributes-table').jqGrid('setGridParam', { postData: { AttributeId: id } }).trigger('reloadGrid');
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
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Название магазина', Type: 'autocomplete', Editable: true, Width: 130,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId',
                Formatter: administrationStoreLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'ProductId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'ProductName', Label: 'Название продукта', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Products/GetForAutocompleteByNamePattern', AutocompleteHidden: 'ProductId',
                DependentFields: ['StoreId'], Formatter: administrationProductLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'AttributeId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'AttributeDescription', Label: 'Описание атрибута', Type: 'autocomplete', Editable: true, Width: 160,
                AutocompleteUrl: '/Administration/ProductAttributes/GetForAutocompleteByDescriptionPattern',
                AutocompleteHidden: 'AttributeId', DependentFields: ['StoreId']
            },
            { Name: 'Value', Label: 'Значение', Width: 550, Editable: true, EditHidden: true },
            { Name: 'IsDeleted', Label: 'Удален', Type: 'checkbox', Width: 50, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 100 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 100 }
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
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Название магазина', Type: 'autocomplete', Editable: true, Width: 130,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId',
                Formatter: administrationStoreLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'Name', Label: 'Имя', Type: 'text', Width: 140, Editable: true, MaxLength: 256 },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 100 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 100 }
        ]
    });
}