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
            { Name: 'CreateUserId', Label: 'Создатель', Type: 'number', Width: 70 },
            { Name: 'Name', Label: 'Имя', Type: 'text', Width: 130, Editable: true, MaxLength: 256 },
            { Name: 'ApiSecret', Label: 'АПИ-секрет', Type: 'text', Width: 130 },
            { Name: 'IsLocked', Label: 'Заблокирован', Type: 'checkbox', Width: 100, Editable: true },
            { Name: 'IsDeleted', Label: 'Удален', Type: 'checkbox', Width: 70, Editable: true },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 120 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Hidden: true, EditHidden: true }
        ]
    });
});