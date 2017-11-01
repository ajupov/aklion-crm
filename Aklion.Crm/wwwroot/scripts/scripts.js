'use strict';

const commonUi = {
    navbar: $('#navbar'),
    mobileMenuLink: $('#mobile-menu-link'),
    dropdownMenuLink: $('#dropdown-menu-link'),
    dropdownMenuContent: $('#dropdown-menu-content')
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
});

$(document).click(() => {
    commonUi.dropdownMenuContent.hide();
});