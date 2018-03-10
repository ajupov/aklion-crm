'use strict';

function initStoresTable() {
    createTable({
        Title: 'Магазины',
        Element: '#stores-table',
        Pager: '#stores-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        CanExtractFilters: true,
        DataUrl: '/Administration/Stores/GetList',
        CreateUrl: '/Administration/Stores/Create',
        UpdateUrl: '/Administration/Stores/Update',
        DeleteUrl: '/Administration/Stores/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'ApiSecret', Label: 'АПИ-секрет', Type: 'text', Width: 155, Editable: false, Sortable: false, Search: false },
            {
                Name: 'GenerateApiSecret', Label: 'Сгенерировать АПИ-секрет', Type: 'custom', Width: 155, Align: 'center',
                Formatter: generateApiSecretFormatter, Sortable: false, Search: false
            },
            { Name: 'IsLocked', Label: 'Заблокирован', Type: 'checkbox', Width: 75, Editable: true, Sortable: false },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 50, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ]
    });
}

function generateApiSecretFormatter(value, options, data) {
    return `<button onclick="generateApiSecret(event, ${data.Id});" class="cell-button" title="Сгенерировать">
                Сгенерировать
            </button>`;
}

function generateApiSecret(event, id) {
    event.preventDefault();
    event.stopPropagation();

    postText('/Administration/Stores/GenerateApiSecret', { id: id }, result => {
        if (result !== null && result !== undefined && result.length > 0) {
            const formField = $('#ViewGrid_stores-table td#v_ApiSecret span');
            formField.html(result);
            $('#stores-table').trigger('reloadGrid');
        }
    });
}