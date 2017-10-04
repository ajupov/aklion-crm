﻿'use strict';

$(document).ready(() => {
    $('.button-collapse').sideNav();
    //$('select').material_select();
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