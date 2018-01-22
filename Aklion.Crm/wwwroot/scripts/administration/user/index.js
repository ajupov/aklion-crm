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
        DataUrl: '/Administration/Users/GetList',
        UpdateUrl: '/Administration/Users/Update',
        DeleteUrl: '/Administration/Users/Delete',
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
            {
                Name: 'IsEmailConfirmed', Label: 'Email подтвержден', Type: 'checkbox', Hidden: true, Editable: true,
                EditHidden: true
            },
            {
                Name: 'IsPhoneConfirmed', Label: 'Телефон подтвержден', Type: 'checkbox', Hidden: true, Editable: true,
                EditHidden: true
            },
            { Name: 'IsLocked', Label: 'Заблокирован', Type: 'checkbox', Width: 75, Editable: true, Sortable: false },
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
        DataUrl: '/Administration/UserAttributeLinks/GetList',
        CreateUrl: '/Administration/UserAttributeLinks/Create',
        UpdateUrl: '/Administration/UserAttributeLinks/Update',
        DeleteUrl: '/Administration/UserAttributeLinks/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Магазин', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId',
                Formatter: administrationStoreLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'UserId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'UserLogin', Label: 'Пользователь', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Users/GetForAutocompleteByLoginPattern', AutocompleteHidden: 'UserId',
                Formatter: administrationUserLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'AttributeId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'AttributeDescription', Label: 'Атрибут', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/UserAttributes/GetForAutocompleteByDescriptionPattern', AutocompleteHidden: 'AttributeId',
                DependentFields: ['StoreId']
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
        DataUrl: '/Administration/UserPermissions/GetList',
        CreateUrl: '/Administration/UserPermissions/Create',
        UpdateUrl: '/Administration/UserPermissions/Update',
        DeleteUrl: '/Administration/UserPermissions/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Магазин', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId',
                Formatter: administrationStoreLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'UserId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'UserLogin', Label: 'Пользователь', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Users/GetForAutocompleteByLoginPattern', AutocompleteHidden: 'UserId',
                Formatter: administrationUserLinkFormatter, Unformatter: linkUnFormatter
            },
            {
                Name: 'Permission', Label: 'Право', Type: 'select', Editable: true, Sortable: false,
                SelectValues: getSelectValues('/Administration/Permissions/GetForSelect'), Width: 120
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
        DataUrl: '/Administration/UserAttributes/GetList',
        CreateUrl: '/Administration/UserAttributes/Create',
        UpdateUrl: '/Administration/UserAttributes/Update',
        DeleteUrl: '/Administration/UserAttributes/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Магазин', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId',
                Formatter: administrationStoreLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'Description', Label: 'Описание', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 55, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ]
    });
}