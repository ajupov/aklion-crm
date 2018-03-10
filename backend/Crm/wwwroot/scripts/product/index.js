'use strict';

function initProductsTable() {
    createTable({
        Title: 'Продукты',
        Element: '#products-table',
        Pager: '#products-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        CanExtractFilters: true,
        DataUrl: '/Products/GetList',
        CreateUrl: '/Products/Create',
        UpdateUrl: '/Products/Update',
        DeleteUrl: '/Products/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'ParentId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'ParentName', Label: 'Родительский продукт', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Products/GetForAutocompleteByNamePattern', AutocompleteHidden: 'ParentId'
            },
            {
                Name: 'StatusId', Label: 'Статус', Type: 'select', Editable: true, Sortable: false,
                SelectValues: getSelectValues('/ProductStatuses/GetForSelect'), Width: 120
            },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'Price', Label: 'Стоимость', Type: 'money', Width: 80, Editable: true },
            { Name: 'VendorCode', Label: 'Артикул', Type: 'text', Width: 120, Editable: true, MaxLength: 16 },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 50, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ],
        OnSelectRow: id => {
            $('#products-attributes-table').jqGrid('setGridParam', { postData: { ProductId: id } }).trigger('reloadGrid');
            $('#products-images-table').jqGrid('setGridParam', { postData: { ProductId: id } }).trigger('reloadGrid');
        }
    });

    createTable({
        Title: 'Атрибуты продуктов',
        Element: '#products-attributes-table',
        Pager: '#products-attributes-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/ProductAttributeLinks/GetList',
        CreateUrl: '/ProductAttributeLinks/Create',
        UpdateUrl: '/ProductAttributeLinks/Update',
        DeleteUrl: '/ProductAttributeLinks/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'ProductId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'ProductName', Label: 'Продукт', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Products/GetForAutocompleteByNamePattern', AutocompleteHidden: 'ProductId',
                Formatter: productLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'AttributeId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'AttributeDescription', Label: 'Атрибут', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/ProductAttributes/GetForAutocompleteByDescriptionPattern',
                AutocompleteHidden: 'AttributeId'
            },
            { Name: 'Value', Label: 'Значение', Width: 250, Editable: true, EditHidden: true },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 50, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ]
    });

    createTable({
        Title: 'Изображения продуктов',
        Element: '#products-images-table',
        Pager: '#products-images-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        IsWithFile: true,
        DataUrl: '/ProductImageKeyLinks/GetList',
        CreateUrl: '/ProductImageKeyLinks/Create',
        UpdateUrl: '/ProductImageKeyLinks/Update',
        DeleteUrl: '/ProductImageKeyLinks/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'ProductId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'ProductName', Label: 'Продукт', Type: 'autocomplete', Editable: true, Width: 120,
                AutocompleteUrl: '/Products/GetForAutocompleteByNamePattern', AutocompleteHidden: 'ProductId',
                Formatter: productLinkFormatter, Unformatter: linkUnFormatter
            },
            {
                Name: 'KeyId', Label: 'Ключ', Type: 'select', Editable: true, Sortable: false,
                SelectValues: getSelectValues('/ProductImageKeys/GetForSelect'), Width: 120
            },
            {
                Name: 'SetValue', Label: 'Установить изображение', Type: 'custom', Width: 155, Align: 'center',
                Formatter: loadImageDialogFormatter, Sortable: false, Search: false
            },
            {
                Name: 'Base64Value', Label: 'Изображение', Type: 'image', Width: 100, Search: false, Sortable: false,
                Editable: false, EditHidden: true, Formatter: base64ImageValueFormatter
            },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 50, Editable: true, Sortable: false },
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
        DataUrl: '/ProductAttributes/GetList',
        CreateUrl: '/ProductAttributes/Create',
        UpdateUrl: '/ProductAttributes/Update',
        DeleteUrl: '/ProductAttributes/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'Description', Label: 'Описание', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 50, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ]
    });
}

function initOthersTable() {
    createTable({
        Title: 'Статус продукта',
        Element: '#products-statuses-table',
        Pager: '#products-statuses-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/ProductStatuses/GetList',
        CreateUrl: '/ProductStatuses/Create',
        UpdateUrl: '/ProductStatuses/Update',
        DeleteUrl: '/ProductStatuses/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ]
    });

    createTable({
        Title: 'Ключи изображений продукта',
        Element: '#images-table',
        Pager: '#images-table-pagination',
        IsViewable: true,
        IsEditable: true,
        IsCreatable: true,
        IsDeletable: true,
        IsFilterable: true,
        DataUrl: '/ProductImageKeys/GetList',
        CreateUrl: '/ProductImageKeys/Create',
        UpdateUrl: '/ProductImageKeys/Update',
        DeleteUrl: '/ProductImageKeys/Delete',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'Name', Label: 'Название', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'Description', Label: 'Описание', Type: 'text', Width: 120, Editable: true, MaxLength: 256 },
            { Name: 'IsDeleted', Label: 'Удалён', Type: 'checkbox', Width: 50, Editable: true, Sortable: false },
            { Name: 'CreateDate', Label: 'Дата создания', Type: 'datetime', Width: 110 },
            { Name: 'ModifyDate', Label: 'Дата изменения', Type: 'datetime', Width: 110 }
        ]
    });
}

function loadImageDialogFormatter(value, options, data) {
    return `<button onclick="loadImageDialogShow(event, ${data.Id});" class="cell-button" title="Открыть окно загрузки изображения">
                Установить
            </button>`;
}

function base64ImageValueFormatter(value) {
    return `<img style="width:100px; height:auto;" src="data:image/jpg;base64, ${value}"></img>`;
}

function loadImageDialogShow(event, id) {
    event.preventDefault();
    event.stopPropagation();

    $('#product-id').val(id);
    $('#load-image-dialog').dialog('open');
}