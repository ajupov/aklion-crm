'use strict';

const ui = {
    usersTable: $('#users-table'),
    userPermissionTable: $('#users-permissions-table')
}

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
        DataUrl: '/Users/GetList',
        UpdateUrl: '/Users/Update',
        DeleteUrl: '/Users/Delete',
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
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 100 }
        ]
    });
}

function initPermissionsTable() {
    createTable({
        Title: 'Права пользователей',
        Element: '#users-permissions-table',
        Pager: '#users-permissions-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/UserPermissions/GetList',
        CreateUrl: '/UserPermissions/Create',
        UpdateUrl: '/UserPermissions/Update',
        DeleteUrl: '/UserPermissions/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'UserId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'UserLogin', Label: 'Логин пользователя', Type: 'autocomplete', Editable: true, Width: 130,
                AutocompleteUrl: '/Administration/Users/GetForAutocompleteByLoginPattern', AutocompleteHidden: 'UserId',
                Formatter: userLinkFormatter, Unformatter: userLinkUnFormatter
            },
            {
                Name: 'Permission', Label: 'Право', Type: 'select', Editable: true, Sortable: false,
                SelectValues: getSelectValues('/Administration/Permissions/GetList'), Width: 240
            },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 100 }
        ]
    });
}

function userLinkFormatter(value, options, data) {
    return `<a href="/Users?Id=${data.UserId}">${data.UserLogin}</a>`;
}

function userLinkUnFormatter(value, options, cell) {
    return $('a', cell).text();
}