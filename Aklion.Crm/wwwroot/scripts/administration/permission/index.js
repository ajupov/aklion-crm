'use strict';

const ui = {
    userPostTable: $('#user-permission-table')
}

$(document).ready(() => {
    const permissionSelectValues = getSelectValues('/Administration/Permissions/GetList');

    createTable({
        Title: 'Права пользователей',
        Element: '#user-permission-table',
        Pager: '#user-permission-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/Administration/UserPermissions/GetList',
        CreateUrl: '/Administration/UserPermissions/Create',
        UpdateUrl: '/Administration/UserPermissions/Update',
        DeleteUrl: '/Administration/UserPermissions/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 70 },
            { Name: 'UserId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'UserLogin', Label: 'Логин пользователя', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Users/GetForAutocompleteByLoginPattern', AutocompleteHidden: 'UserId',
                Formatter: userLinkFormatter, Unformatter: userLinkUnFormatter
            },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Название магазина', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId',
                Formatter: storeLinkFormatter, Unformatter: storeLinkUnFormatter
            },
            {
                Name: 'Permission', Label: 'Право', Type: 'select', Editable: true, Sortable: false,
                SelectValues: permissionSelectValues, Width: 240
            },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 120 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Hidden: true, EditHidden: true }
        ]
    });
});

function storeLinkFormatter(value, options, data) {
    return `<a href="/Administration/Stores?Id=${data.StoreId}">${data.StoreName}</a>`;
}

function storeLinkUnFormatter(value, options, cell) {
    return $('a', cell).text();
}

function userLinkFormatter(value, options, data) {
    return `<a href="/Administration/Users?Id=${data.UserId}">${data.UserLogin}</a>`;
}

function userLinkUnFormatter(value, options, cell) {
    return $('a', cell).text();
}