'use strict';

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
            { Name: 'CreateUserId', Label: 'Создатель', Type: 'number', Hidden: true },
            {
                Name: 'CreateUserLogin', Label: 'Логин создателя', Type: 'autocomplete', Width: 130,
                Formatter: createUserIdFormatter, AutocompleteUrl: '/Administration/Users/GetForAutocompleteByLoginPattern',
                Editable: true, AutocompleteHidden: 'CreateUserId'
            },
            { Name: 'CreateUserId', Type: 'number', Hidden: true, Editable: true },
            { Name: 'ApiSecret', Label: 'АПИ-секрет', Type: 'text', Hidden: true, EditHidden: true },
            { Name: 'IsLocked', Label: 'Заблокирован', Type: 'checkbox', Width: 100, Editable: true },
            { Name: 'IsDeleted', Label: 'Удален', Type: 'checkbox', Width: 70, Editable: true },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 120 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Hidden: true, EditHidden: true }
        ]
    });
});

function createUserIdFormatter(value, options, data) {
    return `<a href="/Administration/Users?Id=${data.CreateUserId}">${data.CreateUserLogin}</a>`;
}