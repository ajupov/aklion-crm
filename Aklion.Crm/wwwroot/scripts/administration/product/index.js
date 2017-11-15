'use strict';

const ui = {
    storeTable: $('#product-table'),
    categoryTable: $('#category-table'),
    productCategoryTable: $('#product-category-table')
}

$(document).ready(() => {
    $('.tab-button').first().click();
});

function initProductsTable() {
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
}

function initCategoriesTable() {
    createTable({
        Title: 'Категории',
        Element: '#category-table',
        Pager: '#category-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/Administration/Categories/GetList',
        CreateUrl: '/Administration/Categories/Create',
        UpdateUrl: '/Administration/Categories/Update',
        DeleteUrl: '/Administration/Categories/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 70 },
            { Name: 'Name', Label: 'Имя', Type: 'text', Width: 130, Editable: true, MaxLength: 256 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Название магазина', Type: 'autocomplete', Editable: true, Width: 125,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId',
                Formatter: storeLinkFormatter, Unformatter: storeLinkUnFormatter
            },
            { Name: 'IsDeleted', Label: 'Удален', Type: 'checkbox', Width: 70, Editable: true, Sortable: false },
            { Name: 'ParentId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'ParentName', Label: 'Название родителя', Type: 'autocomplete', Editable: true, Width: 125,
                AutocompleteUrl: '/Administration/Categories/GetForAutocompleteByNamePattern', AutocompleteHidden: 'ParentId'
            },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 120 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Hidden: true, EditHidden: true }
        ],
        OnSelectRow: id => {
            ui.productCategoryTable.jqGrid('setGridParam', { postData: { StoreId: id } }).trigger('reloadGrid');
        }
    });

    createTable({
        Title: 'Категории продуктов',
        Element: '#product-category-table',
        Pager: '#product-category-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/Administration/ProductCategories/GetList',
        CreateUrl: '/Administration/ProductCategories/Create',
        UpdateUrl: '/Administration/ProductCategories/Update',
        DeleteUrl: '/Administration/ProductCategories/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 70 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Название магазина', Type: 'autocomplete', Editable: true, Width: 125,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId',
                Formatter: storeLinkFormatter, Unformatter: storeLinkUnFormatter
            },
            { Name: 'ProductId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'ProductName', Label: 'Название продукта', Type: 'autocomplete', Editable: true, Width: 130,
                AutocompleteUrl: '/Administration/Products/GetForAutocompleteByNamePattern', AutocompleteHidden: 'ProductId',
                DependentFields: ['StoreId']
            },
            { Name: 'CategoryId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'CategoryName', Label: 'Название категории', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Administration/Categories/GetForAutocompleteByNamePattern', AutocompleteHidden: 'CategoryId',
                DependentFields: ['StoreId']
            },
            { Name: 'IsDeleted', Label: 'Удален', Type: 'checkbox', Width: 70, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 120 },
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