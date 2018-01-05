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
            { Name: 'Id', Label: '№', Type: 'number', Width: 60 },
            { Name: 'StoreId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'StoreName', Label: 'Название магазина', Type: 'autocomplete', Editable: true, Width: 130,
                AutocompleteUrl: '/Administration/Stores/GetForAutocompleteByNamePattern', AutocompleteHidden: 'StoreId',
                Formatter: administrationStoreLinkFormatter, Unformatter: linkUnFormatter
            },
            { Name: 'UserId', Type: 'number', Hidden: true, Editable: true },
            {
                Name: 'UserLogin', Label: 'Логин пользователя', Type: 'autocomplete', Editable: true, Width: 130,
                AutocompleteUrl: '/Administration/Users/GetForAutocompleteByLoginPattern', AutocompleteHidden: 'UserId',
                Formatter: administrationUserLinkFormatter, Unformatter: linkUnFormatter
            },
            {
                Name: 'ActionType', Label: 'Действие', Type: 'select', Editable: true, Sortable: false,
                SelectValues: getSelectValues('/Administration/ActionTypes/GetForSelect'), Width: 240
            },
            { Name: 'OldValue', Label: 'Старое значение', Width: 250 },
            { Name: 'NewValue', Label: 'Новое значение', Width: 250 },
            { Name: 'TimeStamp', Label: 'Метка времени', Type: 'datetime', Width: 100 }
        ]
    });
}