'use strict';

const ui = {
    usersTable: $('#users-table'),
    postTable: $('#post-table'),
    userPostTable: $('#user-post-table'),
    userPermissionTable: $('#user-permission-table')
}

$(document).ready(() => {
    $('.tab-button').first().click();
});

function initUsersTable() {
    createTable({
        Title: 'Пользователи',
        Element: '#users-table',
        Pager: '#users-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: false,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/Administration/Users/GetList',
        UpdateUrl: '/Administration/Users/Update',
        DeleteUrl: '/Administration/Users/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'Login', Label: 'Логин', Type: 'text', Width: 100, Editable: true, MaxLength: 256 },
            { Name: 'Email', Label: 'Электронный адрес', Type: 'email', Width: 130, Editable: true, MaxLength: 256 },
            { Name: 'Phone', Label: 'Телефон', Type: 'text', Width: 70, Editable: true, MaxLength: 10 },
            { Name: 'Surname', Label: 'Фамилия', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'Name', Label: 'Имя', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'Patronymic', Label: 'Отчество', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            {
                Name: 'Gender', Label: 'Пол', Type: 'select', SelectValues: '0:;1:Мужской;2:Женский', Width: 80,
                Editable: true, Sortable: false
            },
            { Name: 'BirthDate', Label: 'Дата рождения', Type: 'date', Width: 100, Editable: true },
            {
                Name: 'IsEmailConfirmed', Label: 'Email подтвержден', Type: 'checkbox', Hidden: true, Editable: true,
                EditHidden: true
            },
            {
                Name: 'IsPhoneConfirmed', Label: 'Телефон подтвержден', Type: 'checkbox', Hidden: true, Editable: true,
                EditHidden: true
            },
            { Name: 'IsLocked', Label: 'Заблокирован', Type: 'checkbox', Width: 80, Editable: true, Sortable: false },
            { Name: 'IsDeleted', Label: 'Удален', Type: 'checkbox', Width: 50, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 100 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Hidden: true, EditHidden: true }
        ]
    });
}

function initPostsTable() {
    createTable({
        Title: 'Должности',
        Element: '#post-table',
        Pager: '#post-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/Administration/Posts/GetList',
        CreateUrl: '/Administration/Posts/Create',
        UpdateUrl: '/Administration/Posts/Update',
        DeleteUrl: '/Administration/Posts/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'Name', Label: 'Имя', Type: 'text', Width: 160, Editable: true, MaxLength: 256 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Название магазина', Type: 'autocomplete', Editable: true, Width: 130,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId',
                Formatter: storeLinkFormatter, Unformatter: storeLinkUnFormatter
            },
            { Name: 'IsDeleted', Label: 'Удален', Type: 'checkbox', Width: 50, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 100 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Hidden: true, EditHidden: true }
        ],
        OnSelectRow: id => {
            ui.userPostTable.jqGrid('setGridParam', { postData: { StoreId: id } }).trigger('reloadGrid');
        }
    });

    createTable({
        Title: 'Должности пользователей',
        Element: '#user-post-table',
        Pager: '#user-post-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/Administration/UserPosts/GetList',
        CreateUrl: '/Administration/UserPosts/Create',
        UpdateUrl: '/Administration/UserPosts/Update',
        DeleteUrl: '/Administration/UserPosts/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'UserId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'UserLogin', Label: 'Логин пользователя', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Users/GetForAutocompleteByLoginPattern', AutocompleteHidden: 'UserId',
                Formatter: userLinkFormatter, Unformatter: userLinkUnFormatter
            },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Название магазина', Type: 'autocomplete', Editable: true, Width: 130,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId',
                Formatter: storeLinkFormatter, Unformatter: storeLinkUnFormatter
            },
            { Name: 'PostId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'PostName', Label: 'Название должности', Type: 'autocomplete', Editable: true, Width: 160,
                AutocompleteUrl: '/Administration/Posts/GetForAutocompleteByNamePattern', AutocompleteHidden: 'PostId',
                DependentFields: ['StoreId']
            },
            { Name: 'IsDeleted', Label: 'Удален', Type: 'checkbox', Width: 50, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 100 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Hidden: true, EditHidden: true }
        ]
    });
}

function initPermissionsTable() {
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
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'UserId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'UserLogin', Label: 'Логин пользователя', Type: 'autocomplete', Editable: true, Width: 130,
                AutocompleteUrl: '/Administration/Users/GetForAutocompleteByLoginPattern', AutocompleteHidden: 'UserId',
                Formatter: userLinkFormatter, Unformatter: userLinkUnFormatter
            },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Название магазина', Type: 'autocomplete', Editable: true, Width: 130,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId',
                Formatter: storeLinkFormatter, Unformatter: storeLinkUnFormatter
            },
            {
                Name: 'Permission', Label: 'Право', Type: 'select', Editable: true, Sortable: false,
                SelectValues: getSelectValues('/Administration/Permissions/GetList'), Width: 240
            },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 100 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Hidden: true, EditHidden: true }
        ]
    });
}

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