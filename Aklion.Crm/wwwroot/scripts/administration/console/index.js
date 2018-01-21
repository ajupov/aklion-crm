'use strict';

function initGeneral() {
    const html =
        `<p>Всего пользователей: 0</p><p>Всего магазинов: 0</p><p>Всего клиентов: 0</p><p>Всего заказов: 0</p>`;

    $('#general-div').html(html);
}

function initAuditLogsTable() {
    createTable({
        Title: 'Аудит действий',
        Element: '#audit-logs-table',
        Pager: '#audit-logs-table-pagination',
        IsViewable: true,
        IsFilterable: true,
        DataUrl: '/Administration/AuditLogs/GetList',
        SortingColumn: 'TimeStamp',
        Columns: [
            { Name: 'Id', Label: '№', Type: 'number', Width: 35 },
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
                Name: 'ObjectType', Label: 'Объект', Type: 'select', Editable: true, Sortable: false,
                SelectValues: getSelectValues('/Administration/AuditLogs/GetObjectTypesForSelect'), Width: 120
            },
            {
                Name: 'ActionType', Label: 'Действие', Type: 'select', Editable: true, Sortable: false,
                SelectValues: getSelectValues('/Administration/AuditLogs/GetActionTypesForSelect'), Width: 120
            },
            { Name: 'OldValue', Label: 'Старое значение', Hidden: true },
            { Name: 'NewValue', Label: 'Новое значение', Hidden: true },
            { Name: 'TimeStamp', Label: 'Метка времени', Type: 'datetime', Width: 110 }
        ]
    });
}