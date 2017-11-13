'use strict';

const ui = {
    storesTable: $('#stores-table')
}

$(document).ready(() => {
    createTable({
        Title: 'Магазины',
        Element: '#stores-table',
        Pager: '#stores-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/Administration/Stores/GetList',
        CreateUrl: '/Administration/Stores/Create',
        UpdateUrl: '/Administration/Stores/Update',
        DeleteUrl: '/Administration/Stores/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 70 },
            { Name: 'Name', Label: 'Имя', Type: 'text', Width: 130, Editable: true, MaxLength: 256 },
            { Name: 'CreateUserId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'CreateUserLogin', Label: 'Логин создателя', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Users/GetForAutocompleteByLoginPattern', AutocompleteHidden: 'CreateUserId',
                Formatter: userLinkFormatter, Unformatter: userLinkUnFormatter
            },
            { Name: 'ApiSecret', Label: 'АПИ-секрет', Type: 'text', Width: 150, Sortable: false, Search: false },
            {
                Name: 'GenerateApiSecret', Label: 'Сгенерировать АПИ-секрет', Type: 'custom', Width: 150, Align: 'center',
                Formatter: generateApiSecretFormatter, Sortable: false, Search: false
            },
            { Name: 'IsLocked', Label: 'Заблокирован', Type: 'checkbox', Width: 100, Editable: true, Sortable: false },
            { Name: 'IsDeleted', Label: 'Удален', Type: 'checkbox', Width: 70, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 120 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Hidden: true, EditHidden: true }
        ]
    });
});

function userLinkFormatter(value, options, data) {
    return `<a href="/Administration/Users?Id=${data.CreateUserId}">${data.CreateUserLogin}</a>`;
}

function userLinkUnFormatter(value, options, cell) {
    return $('a', cell).text();
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

    postText('/Administration/Stores/GenerateApiSecret', { id: id }, result => {
        if (result !== null && result !== undefined && result.length > 0) {
            const formField = $('#ViewGrid_stores-table td#v_ApiSecret span');
            formField.html(result);
            ui.storesTable.trigger('reloadGrid');
        }
    });
}