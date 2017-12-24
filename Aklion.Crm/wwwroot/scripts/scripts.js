'use strict';

const commonUi = {
    navbar: $('#navbar'),
    mobileMenuLink: $('#mobile-menu-link'),
    dropdownMenuLink: $('#dropdown-menu-link'),
    dropdownMenuContent: $('#dropdown-menu-content'),
    tel: $('[type="tel"]'),
    datepicker: $('.datepicker'),
    tabButton: $('.tab-button'),
    tabs: $('.tab')
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

    commonUi.tabButton.first().click();
});

$(document).click(() => {
    commonUi.dropdownMenuContent.hide();
});

function openTab(event, tabName) {
    commonUi.tabButton.removeClass('active');
    $(event).addClass('active');

    commonUi.tabs.hide();
    $(`#${tabName}`).show();
}