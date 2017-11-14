'use strict';

const ui = {
    storeTable: $('#product-table')
}

$(document).ready(() => {
    createTable({
        Title: 'Продукты',
        Element: '#product-table',
        Pager: '#product-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/Administration/Products/GetList',
        CreateUrl: '/Administration/Products/Create',
        UpdateUrl: '/Administration/Products/Update',
        DeleteUrl: '/Administration/Products/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 70 },
            { Name: 'Name', Label: 'Имя', Type: 'text', Width: 130, Editable: true, MaxLength: 256 },
            {
                Name: 'Type', Label: 'Тип', Type: 'select', SelectValues: '0:;1:Товар;2:Услуга', Width: 80,
                Editable: true, Sortable: false
            },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Название магазина', Type: 'autocomplete', Editable: true, Width: 125,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId',
                Formatter: storeLinkFormatter, Unformatter: storeLinkUnFormatter
            },
            {
                Name: 'Description', Label: 'Описание', Type: 'textarea', Hidden: true, EditHidden: true, Editable: true,
                MaxLength: 4000
            },
            { Name: 'Price', Label: 'Стоимость', Type: 'money', Width: 80, Editable: true },
            {
                Name: 'Status', Label: 'Тип', Type: 'select', SelectValues: '0:;1:В наличии;2:На складе;3:На заказ;4:Отсутствует',
                Width: 80, Editable: true, Sortable: false
            },
            { Name: 'VendorCode', Label: 'Артикул', Type: 'text', Width: 130, Editable: true, MaxLength: 16 },
            { Name: 'ParentId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'ParentName', Label: 'Родительский продукт', Type: 'autocomplete', Editable: true, Width: 125,
                AutocompleteUrl: '/Administration/Products/GetForAutocompleteByNamePattern', AutocompleteHidden: 'ParentId',
                DependentFields: ['StoreId']
            },
            { Name: 'IsDeleted', Label: 'Удален', Type: 'checkbox', Width: 70, Editable: true, Sortable: false },
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
