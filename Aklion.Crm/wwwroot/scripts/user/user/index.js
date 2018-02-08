'use strict';

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
        CanExtractFilters: true,
        DataUrl: '/Users/GetList',
        UpdateUrl: '/Users/Update',
        DeleteUrl: '/Users/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'Login', Label: 'Логин', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'Email', Label: 'Email', Type: 'email', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'Phone', Label: 'Телефон', Type: 'text', Width: 70, Editable: true, MaxLength: 10 },
            { Name: 'Surname', Label: 'Фамилия', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'Name', Label: 'Имя', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'Patronymic', Label: 'Отчество', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            {
                Name: 'Gender', Label: 'Пол', Type: 'select', SelectValues: '0:;1:Мужской;2:Женский', Width: 80,
                Editable: true, Sortable: false
            },
            { Name: 'BirthDate', Label: 'Дата рождения', Type: 'date', Width: 110, Editable: true },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 55, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ],
        OnSelectRow: id => {
            $('#users-attributes-table').jqGrid('setGridParam', { postData: { UserId: id } }).trigger('reloadGrid');
            $('#users-permissions-table').jqGrid('setGridParam', { postData: { UserId: id } }).trigger('reloadGrid');
        }
    });

    createTable({
        Title: 'Атрибуты пользователей',
        Element: '#users-attributes-table',
        Pager: '#users-attributes-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/UserAttributeLinks/GetList',
        CreateUrl: '/UserAttributeLinks/Create',
        UpdateUrl: '/UserAttributeLinks/Update',
        DeleteUrl: '/UserAttributeLinks/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'UserId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'UserLogin', Label: 'Пользователь', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Users/GetForAutocompleteByLoginPattern', AutocompleteHidden: 'UserId',
                Formatter: userLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'AttributeId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'AttributeDescription', Label: 'Атрибут', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/UserAttributes/GetForAutocompleteByDescriptionPattern', AutocompleteHidden: 'AttributeId'
            },
            { Name: 'Value', Label: 'Значение', Width: 250, Editable: true, EditHidden: true },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 55, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ]
    });

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
                Name: 'UserLogin', Label: 'Пользователь', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Users/GetForAutocompleteByLoginPattern', AutocompleteHidden: 'UserId',
                Formatter: userLinkFormatter, Unformatter: linkUnFormatter
            },
            {
                Name: 'Permission', Label: 'Право', Type: 'select', Editable: true, Sortable: false,
                SelectValues: getSelectValues('/Permissions/GetForSelect'), Width: 120
            },
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
        DataUrl: '/UserAttributes/GetList',
        CreateUrl: '/UserAttributes/Create',
        UpdateUrl: '/UserAttributes/Update',
        DeleteUrl: '/UserAttributes/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'Description', Label: 'Описание', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 55, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ]
    });
}