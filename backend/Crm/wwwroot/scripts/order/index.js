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
        DataUrl: '/Orders/GetList',
        CreateUrl: '/orders/Create',
        UpdateUrl: '/orders/Update',
        DeleteUrl: '/orders/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'ClientId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'ClientName', Label: 'Клиент', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Clients/GetAutocomplete', AutocompleteHidden: 'ClientId',
                Formatter: clientLinkFormatter, Unformatter: linkUnFormatter
            },
            {
                Name: 'SourceId', Label: 'Источник', Type: 'select', Editable: true, Sortable: false, Width: 120,
                SelectValues: getSelectValues('/OrderSources/GetSelect')
            },
            {
                Name: 'StatusId', Label: 'Статус', Type: 'select', Editable: true, Sortable: false, Width: 120,
                SelectValues: getSelectValues('/OrderStatuses/GetSelect')
            },
            { Name: 'TotalSum', Label: 'Сумма', Type: 'money', Width: 80, Editable: true },
            { Name: 'DiscountSum', Label: 'Скидка', Type: 'money', Width: 80, Editable: true },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 50, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ]
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
        DataUrl: '/OrderItems/GetList',
        CreateUrl: '/OrderItems/Create',
        UpdateUrl: '/OrderItems/Update',
        DeleteUrl: '/OrderItems/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'OrderId', Label: '№ заказа', Type: 'number', Width: 80, Editable: true },
            { Name: 'ProductId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'ProductName', Label: 'Продукт', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Products/GetForAutocompleteByNamePattern', AutocompleteHidden: 'ProductId',
                Formatter: productLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'Price', Label: 'Стоимость', Type: 'money', Width: 80, Editable: true },
            { Name: 'Count', Label: 'Количество', Type: 'number', Width: 80, Editable: true },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 50, Editable: true, Sortable: false },
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
        DataUrl: '/OrderAttributeLinks/GetList',
        CreateUrl: '/OrderAttributeLinks/Create',
        UpdateUrl: '/OrderAttributeLinks/Update',
        DeleteUrl: '/OrderAttributeLinks/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'ProductId', Type: 'number', Hidden: true, Editable: true },
            { Name: 'OrderId', Label: '№ заказа', Type: 'number', Width: 80, Editable: true },
            { Name: 'AttributeId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'AttributeDescription', Label: 'Атрибут', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/OrderAttributes/GetForAutocompleteByDescriptionPattern',
                AutocompleteHidden: 'AttributeId'
            },
            { Name: 'Value', Label: 'Значение', Width: 250, Editable: true, EditHidden: true },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 50, Editable: true, Sortable: false },
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
        IsFilterable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        SortingColumn: 'Name',
        DataUrl: '/OrderAttributes/GetList',
        CreateUrl: '/OrderAttributes/Create',
        UpdateUrl: '/OrderAttributes/Update',
        DeleteUrl: '/OrderAttributes/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Hidden: true },
            { Name: 'Key', Label: 'Ключ', Type: 'text', Width: 150, Editable: true, MaxLength: 256 },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 300, Editable: true, MaxLength: 256 }
        ]
    });
}

function initOthersTable() {
    createTable({
        Title: 'Источник заказа',
        Element: '#orders-sources-table',
        Pager: '#orders-sources-table-pagination',
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        DataUrl: '/OrderSources/GetList',
        CreateUrl: '/OrderSources/Create',
        UpdateUrl: '/OrderSources/Update',
        DeleteUrl: '/OrderSources/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Hidden: true },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 300, Editable: true, MaxLength: 256 }
        ]
    });

    createTable({
        Title: 'Статус заказа',
        Element: '#orders-statuses-table',
        Pager: '#orders-statuses-table-pagination',
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        DataUrl: '/OrderStatuses/GetList',
        CreateUrl: '/OrderStatuses/Create',
        UpdateUrl: '/OrderStatuses/Update',
        DeleteUrl: '/OrderStatuses/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Hidden: true },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 300, Editable: true, MaxLength: 256 }
        ]
    });
}