'use strict';

function initClientsTable() {
    createTable({
        Element: '#clients-table',
        Pager: '#clients-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        CanExtractFilters: true,
        DataUrl: '/Clients/GetList',
        CreateUrl: '/Clients/Create',
        UpdateUrl: '/Clients/Update',
        DeleteUrl: '/Clients/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            { Name: 'CreateDate', Label: 'Создан', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Изменён', Type: 'datetime', Hidden: true, EditHidden: true },
            { Name: 'Name', Label: 'Наименование', Type: 'text', Width: 1000, Editable: true, MaxLength: 256 },
            {
                Name: 'IsDeleted', Label: ' ', Type: 'checkbox', Width: 100, Align: 'center',
                Formatter: clientIsDeletedFormatter, Sortable: false
            }
        ],

        HasSubTable: true,
        HasSubTablePager: true,
        SubTableDataUrl: '/ClientAttributeLinks/GetList',
        SubTableIsViewable: true,
        SubTableIsCreatable: true,
        SubTableCreateUrl: '/ClientAttributeLinks/Create',
        SubTableIsEditable: true,
        SubTableUpdateUrl: '/ClientAttributeLinks/Update',
        SubTableIsDeletable: true,
        SubTableDeleteUrl: '/ClientAttributeLinks/Delete',
        SubTableIsFilterable: false,
        SubTableSortingColumn: 'AttributeName',

        ViewFormFilterParam: 'ClientId',
        ViewFormAdditionalFieldKey: 'AttributeName', 
        ViewFormAdditionalFieldValue: 'Value',

        SubTableColumns: [
            { Name: 'CreateDate', Label: 'Создан', Type: 'datetime', Hidden: true, EditHidden: true },
            { Name: 'AttributeId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'AttributeName', Label: 'Атрибут', Type: 'autocomplete', Editable: true, Width: 200,
                AutocompleteUrl: '/ClientAttributes/GetAutocomplete', AutocompleteHidden: 'AttributeId'
            },
            { Name: 'ClientId', Type: 'number', Hidden: true, Editable: true },
            { Name: 'Value', Label: 'Значение', Editable: true, EditHidden: true, Width: 400 },
            {
                Name: 'IsDeleted', Label: ' ', Type: 'checkbox', Width: 100, Align: 'center',
                Formatter: clientAttributeLinksIsDeletedFormatter, Sortable: false
            }
        ]
    });
}

function clientIsDeletedFormatter(value, options, data) {
    return `<button onclick="deleteClient(event, ${data.Id});" class="cell-button" 
                title="${value ? 'Восстановить' : 'Удалить'}">${value ? 'Восстановить' : 'Удалить'}
            </button>`;
}

function deleteClient(event, id) {
    event.preventDefault();
    event.stopPropagation();

    postText('/Clients/Delete', { id: id }, result => {
        $('#clients-table').trigger('reloadGrid');

        const text = result === 'true' ? 'Восстановить' : 'Удалить';
        const button = $('#ViewGrid_clients-table td#v_IsDeleted span button');
        button.attr('title', text);
        button.text(text);
    });
}

function clientAttributeLinksIsDeletedFormatter(value, options, data) {
    return `<button onclick="deleteClientAttributeLink(event, ${data.Id}, ${data.ClientId});" class="cell-button" 
                title="${value ? 'Восстановить' : 'Удалить'}">${value ? 'Восстановить' : 'Удалить'}
            </button>`;
}

function deleteClientAttributeLink(event, id, clientId) {
    event.preventDefault();
    event.stopPropagation();

    postText('/ClientAttributeLinks/Delete', { id: id }, result => {
        $(`#clients-table_${clientId}`).trigger('reloadGrid');

        const text = result === 'true' ? 'Восстановить' : 'Удалить';
        const button = $(`#ViewGrid_clients-table_${clientId}-sub-table td#v_IsDeleted span button`);
        debugger;
        button.attr('title', text);
        button.text(text);
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
            { Name: 'Key', Label: 'Ключ', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 50, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ]
    });
}