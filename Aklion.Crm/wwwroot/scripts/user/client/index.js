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
        CanExtractFilters: true,
        DataUrl: '/Clients/GetList',
        CreateUrl: '/Clients/Create',
        UpdateUrl: '/Clients/Update',
        DeleteUrl: '/Clients/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 45, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ],
        OnSelectRow: id => {
            $('#clients-attributes-table').jqGrid('setGridParam', { postData: { ClientId: id } }).trigger('reloadGrid');
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
        DataUrl: '/ClientAttributeLinks/GetList',
        CreateUrl: '/ClientAttributeLinks/Create',
        UpdateUrl: '/ClientAttributeLinks/Update',
        DeleteUrl: '/ClientAttributeLinks/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'ClientId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'ClientName', Label: 'Клиент', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Clients/GetForAutocompleteByNamePattern', AutocompleteHidden: 'ClientId',
                Formatter: clientLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'AttributeId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'AttributeDescription', Label: 'Атрибут', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/ClientAttributes/GetForAutocompleteByDescriptionPattern',
                AutocompleteHidden: 'AttributeId'
            },
            { Name: 'Value', Label: 'Значение', Width: 250, Editable: true, EditHidden: true },
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
        DataUrl: '/ClientAttributes/GetList',
        CreateUrl: '/ClientAttributes/Create',
        UpdateUrl: '/ClientAttributes/Update',
        DeleteUrl: '/ClientAttributes/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'Description', Label: 'Описание', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 45, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ]
    });
}