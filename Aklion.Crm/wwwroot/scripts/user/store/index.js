'use strict';

const ui = {
    storeTable: $('#store-table')
}

$(document).ready(() => {
    $('.tab-button').first().click();
});

function initStoresTable() {
    createTable({
        Title: 'Магазины',
        Element: '#store-table',
        Pager: '#store-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/Stores/GetList',
        CreateUrl: '/Stores/Create',
        UpdateUrl: '/Stores/Update',
        DeleteUrl: '/Stores/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 70 },
            { Name: 'Name', Label: 'Имя', Type: 'text', Width: 130, Editable: true, MaxLength: 256 },
            { Name: 'ApiSecret', Label: 'АПИ-секрет', Type: 'text', Width: 150, Sortable: false, Search: false },
            {
                Name: 'GenerateApiSecret', Label: 'Сгенерировать АПИ-секрет', Type: 'custom', Width: 150, Align: 'center',
                Formatter: generateApiSecretFormatter, Sortable: false, Search: false
            },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 120 }
        ]
    });
}

function generateApiSecretFormatter(value, options, data) {
    return `<button onclick="generateApiSecret(event, ${data.Id});" class="cell-button" title="Сгенерировать">
                <i class="fa fa-refresh"></i>
                Сгенерировать
            </button>`;
}

function generateApiSecret(event, id) {
    event.preventDefault();
    event.stopPropagation();

    postText('/Stores/GenerateApiSecret', { id: id }, result => {
        if (result !== null && result !== undefined && result.length > 0) {
            const formField = $('#ViewGrid_store-table td#v_ApiSecret span');
            formField.html(result);
            ui.storeTable.trigger('reloadGrid');
        }
    });
}