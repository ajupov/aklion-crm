'use strict';

$(document).ready(() => {
    debugger;

    switch (window.location.pathname.toLowerCase()) {
        case '/account':
            
            $.get('/account/get',
                {},
                result => {
                    debugger;
                    history.pushState('', 'Аккаунт', 'eeee');
                });
    default:
    }
});

var datePickerOptions = {
    selectMonths: true,
    selectYears: 15,
    labelMonthNext: 'Следующий месяц',
    labelMonthPrev: 'Предыдущий месяц',
    labelMonthSelect: 'Выберите месяц',
    labelYearSelect: 'Выберите год',
    monthsFull: [
        'Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь', 'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'
    ],
    monthsShort: ['Янв', 'Фев', 'Мар', 'Апр', 'Май', 'Июн', 'Июл', 'Авг', 'Сен', 'Окт', 'Ноя', 'Дек'],
    weekdaysFull: ['Воскресенье', 'Понедельник', 'Вторник', 'Среда', 'Четверг', 'Пятница', 'Суббота'],
    weekdaysShort: ['Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'],
    weekdaysLetter: ['В', 'П', 'В', 'С', 'Ч', 'П', 'С'],
    today: 'Сегодня',
    clear: '',
    close: 'Закрыть',
    format: 'dd.mm.yyyy',
    firstDay: 1,
    onSet: function (data) {
        if (data.select > 0) {
            this.close();
        }
    }
};

/* Enums */
const genderEnum = {
    None: 0,
    Male: 1,
    Female: 2
};

const operandTypeEnum = {
    None: 0,
    Bool: 1,
    Byte: 2,
    Short: 3,
    Integer: 4,
    Decimal: 5,
    Float: 6,
    Double: 7,
    String: 8,
    Date: 9,
    DateTime: 10
};

const filterOperationEnum = {
    None: 0,
    Equals: 1,
    NonEquals: 2,
    Greater: 3,
    Less: 4,
    Between: 5,
    Begins: 6,
    Ends: 7,
    Contains: 8
}

function initDropdowns() {
    $('.dropdown-button').dropdown({
        inDuration: 0,
        outDuration: 0,
        constrainWidth: false,
        alignment: 'left'
    });
    $('.not-closing-dropdown + .dropdown-content').click(e => e.stopPropagation());
}


function setOperands(element) {
    const $element = $(element);
    const $tr = $element.closest('tr');

    const $operandTd = $tr.find('[data-role="operand-td"]');
    const $operand2And = $tr.find('[data-role="operand-2-and"]');
    const $operand2 = $tr.find('[data-role="operand-2"]');

    if (parseInt($element.val()) === filterOperationEnum.Between) {
        $operandTd.css('width', '260px');
        $operand2And.show();
        $operand2.show();
    } else {
        $operandTd.css('width', '145px');
        $operand2And.hide();
        $operand2.hide();
    }
}

function resetFilter(element) {
    const $element = $(element);

    const $dropdownContent = $element.closest('.filter-dropdown-content');
    const $select = $dropdownContent.find('select');
    const $input = $dropdownContent.find('input');

    const $operandTd = $dropdownContent.find('[data-role="operand-td"]');
    const $operand2And = $dropdownContent.find('[data-role="operand-2-and"]');
    const $operand2 = $dropdownContent.find('[data-role="operand-2"]');

    $select.val('');
    $input.val('');

    $operandTd.css('width', '145px');
    $operand2And.hide();
    $operand2.hide();
}



function addSortingRow(element) {
    const $element = $(element);
    const $dropdownContent = $element.closest('.sorting-dropdown-content');

    const $fieldSelect = $dropdownContent.find('select[data-role="sorting-field-selector"]');
    const $orderSelect = $dropdownContent.find('select[data-role="sorting-order-selector"]');

    const $table = $dropdownContent.find('table[data-role="table-sorting"]');

    const $exists = $table.find(`input[value="${$fieldSelect.val()}"]`);

    if ($exists.length > 0) {
        return;
    }

    const row = 
        `<tr>
            <td>
                <span>${$fieldSelect.children('option:selected').text()}</span>
                <input type="hidden" value="${$fieldSelect.val()}"/>    
            </td>
            <td>
                <span>${$orderSelect.children('option:selected').text()}</span>
                <input type="hidden" value="${$orderSelect.val()}"/>    
            </td>
            <td style="width: 15px;">
                <button onclick="deleteSortingRow(this);">
                    <i class="fa fa-remove"></i>
                </button>    
            </td>
        </tr>`;

    $table.append(row);
}

function deleteSortingRow(element) {
    const $element = $(element);
    $element.closest('tr').remove();
}

function resetSorting(element) {
    const $element = $(element);
    const $dropdownContent = $element.closest('.sorting-dropdown-content');

    $dropdownContent.find('table').empty();
}

/* Converter */
function toYesNoString(value) {
    return value ? 'Да' : 'Нет';
}

function toGenderString(gender) {
    return gender === genderEnum.Male
        ? 'Мужской'
        : gender === genderEnum.Female
            ? 'Женский'
            : 'Неизвестно';
}

function toDateString(date) {
    return new Date(date).toLocaleDateString('ru-RU');
}

function toDateTimeString(dateTime) {
    const newDateTime = new Date(dateTime);

    return newDateTime.getYear() > 0
        ? `${newDateTime.toLocaleDateString('ru-RU')} ${newDateTime.toLocaleTimeString('en-GB')}`
        : '';
}