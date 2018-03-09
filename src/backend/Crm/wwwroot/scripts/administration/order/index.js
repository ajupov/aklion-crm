'use strict';

function initOrdersTable() {
    createTable({
        Title: 'Заказы',
        Element: '#orders-table',
        Pager: '#orders-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        CanExtractFilters: true,
        DataUrl: '/Administration/Orders/GetList',
        CreateUrl: '/Administration/Orders/Create',
        UpdateUrl: '/Administration/Orders/Update',
        DeleteUrl: '/Administration/Orders/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Магазин', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Stores/GetAutocomplete', AutocompleteHidden: 'StoreId',
                Formatter: administrationStoreLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'ClientId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'ClientName', Label: 'Клиент', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Clients/GetAutocomplete', AutocompleteHidden: 'ClientId',
                DependentFields: ['StoreId'], Formatter: administrationClientLinkFormatter, Unformatter: linkUnFormatter
            },
            {
                Name: 'SourceId', Label: 'Источник', Type: 'select', Editable: true, Sortable: false, Width: 120,
                DependentFields: ['StoreId'], SelectValues: getSelectValues('/Administration/OrderSources/GetSelect')
            },
            {
                Name: 'StatusId', Label: 'Статус', Type: 'select', Editable: true, Sortable: false, Width: 120,
                DependentFields: ['StoreId'], SelectValues: getSelectValues('/Administration/OrderStatuses/GetSelect')
            },
            { Name: 'TotalSum', Label: 'Сумма', Type: 'money', Width: 80, Editable: true },
            { Name: 'DiscountSum', Label: 'Скидка', Type: 'money', Width: 80, Editable: true },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 55, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ],
        OnSelectRow: id => {
            $('#orders-items-table').jqGrid('setGridParam', { postData: { OrderId: id } }).trigger('reloadGrid');
            $('#orders-attributes-table').jqGrid('setGridParam', { postData: { OrderId: id } }).trigger('reloadGrid');
        }
    });

    createTable({
        Title: 'Позиции заказа',
        Element: '#orders-items-table',
        Pager: '#orders-items-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/Administration/OrderItems/GetList',
        CreateUrl: '/Administration/OrderItems/Create',
        UpdateUrl: '/Administration/OrderItems/Update',
        DeleteUrl: '/Administration/OrderItems/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Магазин', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Stores/GetAutocomplete', AutocompleteHidden: 'StoreId',
                Formatter: administrationStoreLinkFormatter, Unformatter: linkUnFormatter
            },
            {
                Name: 'OrderId', Label: '№ заказа', Type: 'autocomplete', Width: 80, Editable: true,
                AutocompleteUrl: '/Administration/Orders/GetAutocomplete', AutocompleteHidden: 'OrderId',
                DependentFields: ['StoreId'], Formatter: administrationOrderLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'ProductId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'ProductName', Label: 'Продукт', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Products/GetAutocomplete', AutocompleteHidden: 'ProductId',
                DependentFields: ['StoreId'], Formatter: administrationProductLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'Price', Label: 'Стоимость', Type: 'money', Width: 80, Editable: true },
            { Name: 'Count', Label: 'Количество', Type: 'number', Width: 80, Editable: true },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 55, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ]
    });

    createTable({
        Title: 'Атрибуты заказов',
        Element: '#orders-attributes-table',
        Pager: '#orders-attributes-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/Administration/OrderAttributeLinks/GetList',
        CreateUrl: '/Administration/OrderAttributeLinks/Create',
        UpdateUrl: '/Administration/OrderAttributeLinks/Update',
        DeleteUrl: '/Administration/OrderAttributeLinks/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Магазин', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Stores/GetAutocomplete', AutocompleteHidden: 'StoreId',
                Formatter: administrationStoreLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'ProductId', Type: 'number', Hidden: true, Editable: true },
            { Name: 'OrderId', Label: '№ заказа', Type: 'number', Width: 80, Editable: true },
            { Name: 'AttributeId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'AttributeName', Label: 'Атрибут', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/OrderAttributes/GetAutocomplete',
                AutocompleteHidden: 'AttributeId', DependentFields: ['StoreId']
            },
            { Name: 'Value', Label: 'Значение', Width: 250, Editable: true, EditHidden: true },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 55, Editable: true, Sortable: false },
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
        DataUrl: '/Administration/OrderAttributes/GetList',
        CreateUrl: '/Administration/OrderAttributes/Create',
        UpdateUrl: '/Administration/OrderAttributes/Update',
        DeleteUrl: '/Administration/OrderAttributes/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Магазин', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Stores/GetAutocomplete', AutocompleteHidden: 'StoreId',
                Formatter: administrationStoreLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'Key', Label: 'Ключ', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 55, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ]
    });
}

function initOthersTable() {
    createTable({
        Title: 'Источник заказа',
        Element: '#orders-sources-table',
        Pager: '#orders-sources-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/Administration/OrderSources/GetList',
        CreateUrl: '/Administration/OrderSources/Create',
        UpdateUrl: '/Administration/OrderSources/Update',
        DeleteUrl: '/Administration/OrderSources/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Магазин', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Stores/GetAutocomplete', AutocompleteHidden: 'StoreId',
                Formatter: administrationStoreLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ]
    });

    createTable({
        Title: 'Статус заказа',
        Element: '#orders-statuses-table',
        Pager: '#orders-statuses-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/Administration/OrderStatuses/GetList',
        CreateUrl: '/Administration/OrderStatuses/Create',
        UpdateUrl: '/Administration/OrderStatuses/Update',
        DeleteUrl: '/Administration/OrderStatuses/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Название магазина', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Stores/GetAutocomplete', AutocompleteHidden: 'StoreId',
                Formatter: administrationStoreLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ]
    });
}