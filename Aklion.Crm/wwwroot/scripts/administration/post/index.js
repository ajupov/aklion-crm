'use strict';

const ui = {
    postTable: $('#post-table'),
    userPostTable: $('#user-post-table')
}

$(document).ready(() => {
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
            { Name: 'Id', Label: '№', Type: 'number', Width: 70 },
            { Name: 'Name', Label: 'Имя', Type: 'text', Width: 130, Editable: true, MaxLength: 256 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Название магазина', Type: 'autocomplete', Editable: true, Width: 125,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId'
            },
            { Name: 'IsDeleted', Label: 'Удален', Type: 'checkbox', Width: 70, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 120 },
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
            { Name: 'Id', Label: '№', Type: 'number', Width: 70 },
            { Name: 'UserId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'UserLogin', Label: 'Логин пользователя', Type: 'autocomplete', Editable: true, Width: 130,
                AutocompleteUrl: '/Administration/Users/GetForAutocompleteByLoginPattern', AutocompleteHidden: 'UserId'
            },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Название магазина', Type: 'autocomplete', Editable: true, Width: 125,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId'
            },
            { Name: 'PostId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'PostName', Label: 'Название должности', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Posts/GetForAutocompleteByNamePattern', AutocompleteHidden: 'PostId',
                DependentFields: ['StoreId']
            },
            { Name: 'IsDeleted', Label: 'Удален', Type: 'checkbox', Width: 70, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 120 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Hidden: true, EditHidden: true }
        ]
    });
});