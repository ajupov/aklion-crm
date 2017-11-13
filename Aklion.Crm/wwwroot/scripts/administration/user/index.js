'use strict';

const ui = {
    usersTable: $('#users-table')
}

$(document).ready(() => {
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
            { Name: 'Id', Label: '№', Type: 'number', Width: 70 },
            { Name: 'Login', Label: 'Логин', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'Email', Label: 'Электронный адрес', Type: 'email', Width: 130, Editable: true, MaxLength: 256 },
            { Name: 'Phone', Label: 'Телефон', Type: 'text', Width: 80, Editable: true, MaxLength: 10 },
            { Name: 'Surname', Label: 'Фамилия', Type: 'text', Width: 130, Editable: true, MaxLength: 256 },
            { Name: 'Name', Label: 'Имя', Type: 'text', Width: 130, Editable: true, MaxLength: 256 },
            { Name: 'Patronymic', Label: 'Отчество', Type: 'text', Width: 130, Editable: true, MaxLength: 256 },
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
            { Name: 'IsLocked', Label: 'Заблокирован', Type: 'checkbox', Width: 100, Editable: true, Sortable: false },
            { Name: 'IsDeleted', Label: 'Удален', Type: 'checkbox', Width: 70, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 120 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Hidden: true, EditHidden: true }
        ]
    });
});