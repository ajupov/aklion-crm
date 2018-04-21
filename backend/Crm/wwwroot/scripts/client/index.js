function initClientsTable() {
    createTable({
        Element: '#clients-table',
        Pager: '#clients-table-pagination',
        SearchDialog: '#clients-table-search',
        SearchDialogActivator: '#clients-table-search-activator',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        CanExtractFilters: true,
        DataUrl: '/Clients/GetList',
        CreateUrl: '/Clients/Create',
        UpdateUrl: '/Clients/Update',
        DeleteUrl: '/Clients/Delete',
        ViewFormFilterParam: 'ClientId',
        ViewFormAdditionalFieldKey: 'AttributeName',
        ViewFormAdditionalFieldValue: 'Value',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Hidden: true },
            { Name: 'Name', Label: 'Наименование', Type: 'text', Width: 400, Editable: true, MaxLength: 256 },
            { Name: 'CreateDate', Label: 'Создан', Type: 'datetime', Width: 120 },
            {
                Name: 'IsDeleted',
                Label: 'Удалён',
                Width: 100,
                Align: 'center',
                Type: 'checkbox',
                Formatter: clientIsDeletedFormatter,
                Sortable: false
            },
            {
                Name: 'Attributes',
                Label: 'Атрибуты',
                Type: 'filterlist',
                FilterSelectDataUrl: '/ClientAttributes/GetSelect',
                Hidden: true,
                FilterHidden: true
            }
        ],
        HasSubTable: true,
        HasSubTablePager: true,
        SubTableLink: 'ClientId',
        SubTableDataUrl: '/ClientAttributeLinks/GetList',
        SubTableIsCreatable: true,
        SubTableCreateUrl: '/ClientAttributeLinks/Create',
        SubTableIsEditable: true,
        SubTableUpdateUrl: '/ClientAttributeLinks/Update',
        SubTableIsDeletable: true,
        SubTableDeleteUrl: '/ClientAttributeLinks/Delete',
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
                AutocompleteUrl: '/ClientAttributes/GetAutocomplete',
                AutocompleteHidden: 'AttributeId'
            },
            { Name: 'ClientId', Type: 'number', Hidden: true, Editable: true },
            { Name: 'Value', Label: 'Значение', Editable: true, EditHidden: true, Width: 424 }
        ]
    });
}

function initAttributesTable() {
    createTable({
        Element: '#attributes-table',
        Pager: '#attributes-table-pagination',
        IsFilterable: true,
        IsCreatable: true,
        IsEditable: true,
        IsDeletable: true,
        SortingColumn: 'Name',
        DataUrl: '/ClientAttributes/GetList',
        CreateUrl: '/ClientAttributes/Create',
        UpdateUrl: '/ClientAttributes/Update',
        DeleteUrl: '/ClientAttributes/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Hidden: true },
            { Name: 'Key', Label: 'Ключ', Type: 'text', Width: 150, Editable: true, MaxLength: 256 },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 400, Editable: true, MaxLength: 256 }
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

    postText('/Clients/Delete',
        { id: id },
        () => {
            $('#clients-table').trigger('reloadGrid');
        });
}