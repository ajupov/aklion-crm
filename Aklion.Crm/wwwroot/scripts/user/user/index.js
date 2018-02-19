'use strict';

function initUsersTable() {
    createTable({
        Title: 'Пользователи',
        Element: '#users-table',
        Pager: '#users-table-pagination',
        IsViewable: true,
        IsCreatable: true,
        IsEditable: false,
        IsDeletable: true,
        IsFilterable: true,
        CanExtractFilters: true,
        SortingColumn: 'Login',
        DataUrl: '/Users/GetList',
        CreateUrl: '/Users/Create',
        DeleteUrl: '/Users/Delete',
        Columns: [
            { Name: 'Login', Label: 'Логин', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'Email', Label: 'Email', Type: 'email', Width: 120, Editable: true, MaxLength: 256 },
            {
                Name: 'Phone', Label: 'Телефон', Type: 'custom', Width: 75, Editable: true, MaxLength: 10,
                Formatter: phoneFormatter, Unformatter: phoneUnFormatter
            },
            { Name: 'Surname', Label: 'Фамилия', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'Name', Label: 'Имя', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'Patronymic', Label: 'Отчество', Width: 120, Editable: true, MaxLength: 256 },
            {
                Name: 'Gender', Label: 'Пол', Type: 'select', SelectValues: '0:;1:Мужской;2:Женский', Width: 80,
                Editable: true, Sortable: false
            },
            { Name: 'BirthDate', Label: 'Дата рождения', Type: 'date', Width: 110, Editable: true },
            {
                Name: 'IsDeleted', Label: 'Статус удалённости', Type: 'checkbox', Width: 120, Align: 'center',
                Formatter: userSwitchIsDeletedFormatter, Sortable: false
            }
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
            {
                Name: 'IsDeleted', Label: 'Статус удалённости', Type: 'checkbox', Width: 120, Align: 'center',
                Formatter: userAttributeSwitchIsDeletedFormatter, Sortable: false
            }
        ]
    });

    createTable({
        Title: 'Права пользователей',
        Element: '#users-permissions-table',
        IsViewable: true,
        IsEditable: false,
        IsCreatable: false,
        IsDeletable: false,
        IsFilterable: false,
        DataUrl: '/UserPermissions/GetList',
        Columns: [
            { Name: 'UserId', Type: 'number', Hidden: true },
            { Name: 'Permission', Type: 'number', Hidden: true },
            { Name: 'PermissionName', Label: 'Право', Width: 250, Filterable: false, Sortable: false },
            {
                Name: 'IsExist', Label: 'Наличие', Type: 'custom', Width: 120, Align: 'center',
                Formatter: userPermissionSwitchFormatter, Filterable: false, Sortable: false
            }
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
        IsDeletable: false,
        IsFilterable: true,
        SortingColumn: 'Name',
        DataUrl: '/UserAttributes/GetList',
        CreateUrl: '/UserAttributes/Create',
        UpdateUrl: '/UserAttributes/Update',
        Columns: [
            { Name: 'Name', Label: 'Ключ', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'Description', Label: 'Название', Width: 240, Editable: true, MaxLength: 256 },
            {
                Name: 'IsDeleted', Label: 'Статус удалённости', Type: 'checkbox', Width: 120, Align: 'center',
                Formatter: attributeSwitchIsDeletedFormatter, Sortable: false
            }
        ]
    });
}

function userSwitchIsDeletedFormatter(value, options, data) {
    return `<button onclick="userSwitchIsDeleted(event, ${data.Id});" class="cell-button" 
                title="${value ? 'Восстановить' : 'Удалить'}">${value ? 'Восстановить' : 'Удалить'}
            </button>`;
}

function attributeSwitchIsDeletedFormatter(value, options, data) {
    return `<button onclick="attributeSwitchIsDeleted(event, ${data.Id});" class="cell-button" 
                title="${value ? 'Восстановить' : 'Удалить'}">${value ? 'Восстановить' : 'Удалить'}
            </button>`;
}

function userAttributeSwitchIsDeletedFormatter(value, options, data) {
    return `<button onclick="userAttributeSwitchIsDeleted(event, ${data.Id});" class="cell-button" 
                title="${value ? 'Восстановить' : 'Удалить'}">${value ? 'Восстановить' : 'Удалить'}
            </button>`;
}

function userSwitchIsDeleted(event, id) {
    event.preventDefault();
    event.stopPropagation();

    postText('/Users/SwitchIsDeleted', { id: id }, result => {
        $('#users-table').trigger('reloadGrid');

        const text = result === 'true' ? 'Восстановить' : 'Удалить';
        const button = $('#ViewGrid_users-table td#v_IsDeleted span button');
        button.attr('title', text);
        button.text(text);
    });
}

function attributeSwitchIsDeleted(event, id) {
    event.preventDefault();
    event.stopPropagation();

    postText('/UserAttributes/SwitchIsDeleted', { id: id }, result => {
        $('#attributes-table').trigger('reloadGrid');

        const text = result === 'true' ? 'Восстановить' : 'Удалить';
        const button = $('#ViewGrid_attributes-table td#v_IsDeleted span button');
        button.attr('title', text);
        button.text(text);
    });
}

function userAttributeSwitchIsDeleted(event, id) {
    event.preventDefault();
    event.stopPropagation();

    postText('/UserAttributeLinks/SwitchIsDeleted', { id: id }, result => {
        $('#users-attributes-table').trigger('reloadGrid');

        const text = result === 'true' ? 'Восстановить' : 'Удалить';
        const button = $('#ViewGrid_users-attributes-table td#v_IsDeleted span button');
        button.attr('title', text);
        button.text(text);
    });
}

function onChangePermission(userId, permission) {
    postJson('/UserPermissions/Switch', { userId: userId, permission: permission });
}

function userPermissionSwitchFormatter(value, options, data) {
    return `<input type="checkbox"${data.IsExist ? 'checked="checked"' : ''} onchange="onChangePermission(${data.UserId}, ${data.Permission})" />`;
}