'use strict';

function initClientsTable() {
    createTable({
        Title: 'Клиенты',
        Element: '#clients-table',
        Pager: '#clients-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/Administration/Clients/GetList',
        CreateUrl: '/Administration/Clients/Create',
        UpdateUrl: '/Administration/Clients/Update',
        DeleteUrl: '/Administration/Clients/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Название магазина', Type: 'autocomplete', Editable: true, Width: 130,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId',
                Formatter: administrationStoreLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'Name', Label: 'Имя', Type: 'text', Width: 130, Editable: true, MaxLength: 256 },
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
        DataUrl: '/Administration/ClientAttributes/GetList',
        CreateUrl: '/Administration/ClientAttributes/Create',
        UpdateUrl: '/Administration/ClientAttributes/Update',
        DeleteUrl: '/Administration/ClientAttributes/Delete',
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
            $('#clients-attributes-table').jqGrid('setGridParam', { postData: { AttributeId: id } }).trigger('reloadGrid');
        }
    });

    createTable({
        Title: 'Атрибуты клиентов',
        Element: '#clients-attributes-table',
        Pager: '#clients-attributes-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/Administration/ClientAttributeLinks/GetList',
        CreateUrl: '/Administration/ClientAttributeLinks/Create',
        UpdateUrl: '/Administration/ClientAttributeLinks/Update',
        DeleteUrl: '/Administration/ClientAttributeLinks/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Название магазина', Type: 'autocomplete', Editable: true, Width: 130,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId',
                Formatter: administrationStoreLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'ClientId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'ClientName', Label: 'Название клиента', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Clients/GetForAutocompleteByNamePattern', AutocompleteHidden: 'ClientId',
                DependentFields: ['StoreId'], Formatter: administrationClientLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'AttributeId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'AttributeDescription', Label: 'Описание атрибута', Type: 'autocomplete', Editable: true, Width: 160,
                AutocompleteUrl: '/Administration/ClientAttributes/GetForAutocompleteByDescriptionPattern',
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
}