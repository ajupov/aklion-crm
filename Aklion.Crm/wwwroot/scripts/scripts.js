﻿'use strict';

const commonUi = {
    navbar: $('#navbar'),
    mobileMenuLink: $('#mobile-menu-link'),
    dropdownMenuLink: $('#dropdown-menu-link'),
    dropdownMenuContent: $('#dropdown-menu-content'),
    tel: $('[type="tel"]'),
    datepicker: $('.datepicker')
};

$(document).ready(() => {
    commonUi.mobileMenuLink.click(() => {
        commonUi.navbar.toggleClass('responsive');
    });

    commonUi.dropdownMenuLink.click('*',
        e => {
            e.preventDefault();
            e.stopPropagation();

            commonUi.dropdownMenuContent.toggle();
        });

    new Inputmask('+79999999999', { greedy: false }).mask(commonUi.tel);

    initDatePicker(commonUi.datepicker);
});

$(document).click(() => {
    commonUi.dropdownMenuContent.hide();
});

function openTab(event, tabName) {
    $('.tab-button').removeClass('active');
    $(event).addClass('active');

    $('.tab').hide();
    $(`#${tabName}`).show();
}