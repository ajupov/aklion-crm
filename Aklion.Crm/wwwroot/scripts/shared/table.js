//function createTable(table, columns, sizes, dataUrl) {
//    const tableId = table.id;

//    table.addClass('bordered striped highlight grid');

//    let html = '';
//    html += getHeadHtml(tableId, columns, dataUrl);
//    html += getFootHtml(tableId, columns, sizes);
//    table.html(html);

//    updateTable(tableId, columns, dataUrl);
//}

//function getHeadHtml(tableId, columns) {
//    let html = '<thead>';
//    html += getHeadSortingHtml(tableId, columns);
//    html += getHeadFilterHtml(tableId, columns);
//    html += '</thead>';

//    return html;
//}
//function getHeadSortingHtml(tableId, columns) {
//    let html = '<tr>';
//    $.each(columns,
//        (index, item) => {
//            html += `<th 
//                ${item.hidden ? 'hidden ' : ''} 
//                width="${item.width}">`;
//            html += item.sorting
//                ? `<button 
//                    class="table-sorting-button" 
//                    data-table-id="${tableId}"
//                    data-label="${item.label}"
//                    data-sorting-order="asc"
//                    data-sorting-column="${item.name}"
//                    data-sorting-enable="false">${item.label}
//                </button>`
//                : `<span>${item.label}</span>`;
//            html += '</th>';
//        });
//    html += '</tr>';

//    return html;
//}
//function getHeadFilterHtml(tableId, columns) {
//    let html = '<tr>';

//    $.each(columns,
//        (index, column) => {
//            html += `<th 
//                ${column.hidden ? 'hidden ' : ''}
//                width="${column.width}">`;

//            if (e.search) {
//                switch (column.type) {
//                case 'int':
//                case 'string':
//                    html += `<input 
//                                type="text"
//                                class="table-filter"
//                                data-table-id="${tableId}"
//                                data-filter-name="${column.name}"/>`;
//                    break;
//                case 'bool':
//                    html += `<select 
//                        class="select table-filter"
//                        data-table-id="${tableId}"
//                        data-filter-name="${column.name}">
//                            <option></option>
//                            <option>Да</option>
//                            <option>Нет</option>
//                    </select>`;
//                case 'datetime':
//                    html += `<input 
//                        type="text"
//                        class="datepicker center-align table-filter" 
//                        data-table-id="${tableId}"
//                        data-filter-name="${column.name}"/>`;
//                }
//            }

//            html += '</th>';
//        });
//    html += '</tr>';

//    return html;
//}

//function getFootHtml(tableId, columns, sizes) {
//    let count = 0;

//    $.each(columns,
//        (index, item) => {
//            if (!item.hidden) {
//                count++;
//            }
//        });

//    let html = '<tfoot>';
//    html += '<tr>';
//    html += `<td colspan="${count}">`;
//    html += '<div>';
//    html += '<button>';
//    html += '<i class="fa fa-fast-backward"></i>';
//    html += '</button>';
//    html += '<button>';
//    html += '<i class="fa fa-backward"></i>';
//    html += '</button>';
//    html += '<div>';
//    html += '<span>';
//    html += 'Стр. ';
//    html += '</span>';
//    html += `<input type="text" value="0">`;
//    html += '<span>';
//    html += ` из 0`;
//    html += '</span>';
//    html += '</div>';
//    html += '<button>';
//    html += '<i class="fa fa-forward"></i>';
//    html += '</button>';
//    html += '<button>';
//    html += '<i class="fa fa-fast-forward"></i>';
//    html += '</button>';
//    html += '<div>';
//    html += '<select>';

//    $.each(sizes,
//        (index, item) => {
//            html += `<option value="${item}">`;
//            html += item;
//            html += '</option>';
//        });

//    html += '</select>';
//    html += '</div>';
//    html += '</div>';
//    html += '<span class="right">';
//    html += `Показано: 0 - 0 из 0`;
//    html += '</span>';
//    html += '</td>';
//    html += '</tr>';
//    html += '</tfoot>';

//    return html;
//}

//function updateTable(tableId, columns, dataUrl, page, size) {
//    var table = $(tableId);

//    var data = {
//        Page: page > 0 ? page : 1,
//        Size: size > 0 ? size : 10
//    };

//    data = Object.assign(data, getFilters(tableId));
//    data = Object.assign(data, getSorting(tableId));

//    $.get(dataUrl,
//        data,
//        result => {
//            updateBody(table, result, columns);
//            updateFoot(table, result);

//            $('.select').material_select();
//            $('.datepicker').pickadate(datePickerOptions);

//        }).fail(e => alert(e));
//}

//function updateBody(table, result, columns) {
//    table.find('tbody').remove();

//    if (result.Items === null || result.Items.length === 0) {
//        return;
//    }

//    var html = '<tbody>';
//    $.each(result.Items,
//        (rowIndex, row) => {
//            html += '<tr>';

//            $.each(columns,
//                (columnIndex, column) => {
//                    if (row.hasOwnProperty(column.name)) {
//                        const value = row[column.name];
//                        html += `<td 
//                                    ${column.key ? ` data-id="${value}"` : ''} 
//                                    style="text-align: ${column.align}"
//                                    ${column.hidden ? ' hidden' : ''}>`;

//                        if (value !== null) {
//                            switch (column.type) {
//                            case 'int':
//                            case 'decimal':
//                            case 'double':
//                            case 'string':
//                                html += value;
//                            case 'date':
//                                html += new Date(value).toLocaleDateString('ru-RU');

//                            case 'datetime':
//                                html += new Date(value).toLocaleDateString('ru-RU') + ' ' +
//                                    new Date(value).toLocaleTimeString('en-GB');
//                            case 'time':
//                                html += new Date(value).toLocaleTimeString('en-GB');
//                            case 'bool':
//                                html += value ? 'Да' : 'Нет';
//                            }
//                        }

//                        html += '</td>';
//                    }
//                });

//            html += '</tr>';
//        });
//    html += '</tbody>';

//    table.find('thead').after(html);
//}

//function updateFoot(table, result) {
    
//}