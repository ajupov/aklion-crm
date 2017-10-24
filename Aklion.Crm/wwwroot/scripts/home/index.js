'use strict';

const messages = {
    errorMessage: 'Произошла ошибка'
};

const urls = {
    getUsersTable: 'Users/GetTable'
};

const ui = {
    mainBlock: $('#main-block'),
    usersAnchor: $('#users-anchor')
}

$(document).ready(() => {
    ui.usersAnchor.click(e => {
        e.preventDefault();
        e.stopPropagation();

        createTable(urls.getUsersTable);
    });

    ui.usersAnchor.trigger('click');
});

function createTable(getTableUrl) {
    $.get(getTableUrl,
        result => {
            ui.mainBlock.html(result);
            const $table = ui.mainBlock.find('table.c-table');

            initDropdowns();
            updateBody($table);
        },
        'html').fail(
        result => {
            console.error(result);
            ui.mainBlock.html(messages.errorMessage);
        });
}

function updateBody($table) {
    const data = {
        
    };

    const $body = $table.find('tbody[data-role="table-body"]');

    const dataUrl = $table.attr('data-url');
    const isEditable = $table.attr('data-editable') === 'true';

    $.get(dataUrl,
        data,
        result => {
            if (result.Items === null && result.Items === undefined) {
                return;
            }

            let html = '';

            $.each(result.Items,
                (index, item) => {
                    html +=
                        `<tr>
                            <td class="left-align">${item.Id}</td>
                            <td class="left-align">${item.Email}</td>
                            <td class="left-align">+7${item.Phone}</td>
                            <td class="left-align">${item.Surname}</td>
                            <td class="left-align">${item.Name}</td>
                            <td class="left-align">${item.Patronymic}</td>
                            <td class="center-align">${toGenderString(item.Gender)}</td>
                            <td class="center-align">${toDateString(item.BirthDate)}</td>
                            <td class="center-align">${toYesNoString(item.IsEmailConfirmed)}</td>
                            <td class="center-align">${toYesNoString(item.IsPhoneConfirmed)}</td>
                            <td class="center-align">${toYesNoString(item.IsLocked)}</td>
                            <td class="center-align">${toYesNoString(item.IsDeleted)}</td>
                            <td class="center-align">${toDateTimeString(item.CreateDate)}</td>
                            <td class="center-align">${toDateTimeString(item.ModifyDate)}</td>`;

                    if (isEditable) {
                        html +=
                            `<td class="center-align">
                                <button type="button" title="Изменить" onclick="editRow(${item.Id});">
                                    <i class="fa fa-pencil"></i>
                                </button>
                                <button type="button" title="Удалить" onclick="deleteRow(${item.Id});">
                                    <i class="fa fa-times"></i>
                                </button>
                            </td>`;
                    }

                    html += '</tr>';
                }
            );

            $body.empty();
            $body.append(html);
        },
        'json').fail(
        result => {
            console.error(result);
            $body.html(messages.errorMessage);
        });
}

function setPage(element) {
    const $table = $(element.closest('table'));
    const $element = $(element);

    //const page = $element.attr('data-page');
    const input = $table.find('[data-role="page"]')[0];
    //$(input).val(page);

    updateBody($table);
}

