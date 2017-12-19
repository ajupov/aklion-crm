﻿'use strict';

const ui = {
    productTable: $('#product-table'),
    categoryTable: $('#category-table'),
    productCategoryTable: $('#product-category-table'),
    tagTable: $('#tag-table'),
    productTagTable: $('#product-tag-table'),
    attributeTable: $('#attribute-table'),
    productAttributeTable: $('#product-attribute-table')
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
        DataUrl: '/Products/GetList',
        CreateUrl: '/Products/Create',
        UpdateUrl: '/Products/Update',
        DeleteUrl: '/Products/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'Name', Label: 'Имя', Type: 'text', Width: 130, Editable: true, MaxLength: 256 },
            {
                Name: 'Type', Label: 'Тип', Type: 'select', SelectValues: '0:;1:Товар;2:Услуга', Width: 70,
                Editable: true, Sortable: false
            },
            {
                Name: 'Description', Label: 'Описание', Type: 'textarea', Hidden: true, EditHidden: true, Editable: true,
                MaxLength: 4000
            },
            { Name: 'Price', Label: 'Стоимость', Type: 'money', Width: 80, Editable: true },
            {
                Name: 'Status', Label: 'Тип', Type: 'select', SelectValues: '0:;1:В наличии;2:На складе;3:На заказ;4:Отсутствует',
                Width: 100, Editable: true, Sortable: false
            },
            { Name: 'VendorCode', Label: 'Артикул', Type: 'text', Width: 100, Editable: true, MaxLength: 16 },
            { Name: 'ParentId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'ParentName', Label: 'Родительский продукт', Type: 'autocomplete', Editable: true, Width: 130,
                AutocompleteUrl: '/Products/GetForAutocompleteByNamePattern', AutocompleteHidden: 'ParentId'
            },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 100 }
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
        DataUrl: '/Categories/GetList',
        CreateUrl: '/Categories/Create',
        UpdateUrl: '/Categories/Update',
        DeleteUrl: '/Categories/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'Name', Label: 'Имя', Type: 'text', Width: 130, Editable: true, MaxLength: 256 },
            { Name: 'ParentId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'ParentName', Label: 'Название родителя', Type: 'autocomplete', Editable: true, Width: 130,
                AutocompleteUrl: '/Administration/Categories/GetForAutocompleteByNamePattern', AutocompleteHidden: 'ParentId'
            },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 100 }
        ]
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
        DataUrl: '/ProductCategories/GetList',
        CreateUrl: '/ProductCategories/Create',
        UpdateUrl: '/ProductCategories/Update',
        DeleteUrl: '/ProductCategories/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'ProductId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'ProductName', Label: 'Название продукта', Type: 'autocomplete', Editable: true, Width: 130,
                AutocompleteUrl: '/Administration/Products/GetForAutocompleteByNamePattern', AutocompleteHidden: 'ProductId'
            },
            { Name: 'CategoryId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'CategoryName', Label: 'Название категории', Type: 'autocomplete', Editable: true, Width: 130,
                AutocompleteUrl: '/Administration/Categories/GetForAutocompleteByNamePattern', AutocompleteHidden: 'CategoryId'
            },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 100 }
        ]
    });
}

function initTagsTable() {
    createTable({
        Title: 'Теги',
        Element: '#tag-table',
        Pager: '#tag-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/Tags/GetList',
        CreateUrl: '/Tags/Create',
        UpdateUrl: '/Tags/Update',
        DeleteUrl: '/Tags/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'Name', Label: 'Имя', Type: 'text', Width: 130, Editable: true, MaxLength: 256 },
            { Name: 'IsDeleted', Label: 'Удален', Type: 'checkbox', Width: 50, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 100 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Hidden: true, EditHidden: true }
        ]
    });

    createTable({
        Title: 'Теги продуктов',
        Element: '#product-tag-table',
        Pager: '#product-tag-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/Administration/ProductTags/GetList',
        CreateUrl: '/Administration/ProductTags/Create',
        UpdateUrl: '/Administration/ProductTags/Update',
        DeleteUrl: '/Administration/ProductTags/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 70 },
            { Name: 'ProductId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'ProductName', Label: 'Название продукта', Type: 'autocomplete', Editable: true, Width: 130,
                AutocompleteUrl: '/Products/GetForAutocompleteByNamePattern', AutocompleteHidden: 'ProductId'
            },
            { Name: 'TagId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'TagName', Label: 'Название категории', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Tags/GetForAutocompleteByNamePattern', AutocompleteHidden: 'TagId'
            },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 100 }
        ]
    });
}

function initAttributesTable() {
    createTable({
        Title: 'Атрибуты',
        Element: '#attribute-table',
        Pager: '#attribute-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/Attributes/GetList',
        CreateUrl: '/Attributes/Create',
        UpdateUrl: '/Attributes/Update',
        DeleteUrl: '/Attributes/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'Name', Label: 'Имя', Type: 'text', Width: 130, Editable: true, MaxLength: 256 },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 100 }
        ]
    });

    createTable({
        Title: 'Атрибуты продуктов',
        Element: '#product-attribute-table',
        Pager: '#product-attribute-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/ProductAttributes/GetList',
        CreateUrl: '/ProductAttributes/Create',
        UpdateUrl: '/ProductAttributes/Update',
        DeleteUrl: '/ProductAttributes/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'ProductId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'ProductName', Label: 'Название продукта', Type: 'autocomplete', Editable: true, Width: 130,
                AutocompleteUrl: '/Products/GetForAutocompleteByNamePattern', AutocompleteHidden: 'ProductId'
            },
            { Name: 'AttributeId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'AttributeName', Label: 'Название атрибута', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Attributes/GetForAutocompleteByNamePattern', AutocompleteHidden: 'AttributeId'
            },
            { Name: 'Value', Label: 'Значение', Type: 'text', Width: 130, Editable: true, MaxLength: 256 },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 100 }
        ]
    });
}