'use strict';

const commonUi = {
    navbar: $('#navbar'),
    mobileMenuLink: $('#mobile-menu-link'),
    dropdownMenuLink: $('#dropdown-menu-link'),
    dropdownMenuContent: $('#dropdown-menu-content'),
    tel: $('[type="tel"]'),
    datepicker: $('.datepicker'),
    tabButton: $('.tab-button'),
    tabs: $('.tab'),
    dialog: $('.dialog')
};

$(document).ready(() => {
    commonUi.dialog.dialog({ autoOpen: false, modal: true });
    $('.ui-dialog').css('z-index', '999');

    commonUi.mobileMenuLink.click(() => {
        commonUi.navbar.toggleClass('responsive');
    });

    commonUi.dropdownMenuLink.click('*',
        e => {
            e.preventDefault();
            e.stopPropagation();

            if (commonUi.dropdownMenuContent.css('display') === 'none') {
                getJson('/Account/GetStores', null, (result) => {
                    let html = '<ul>';

                    $.each(result, (infdex, item) => {
                        html += `<li><a onclick="setStore(${item.Id})">${item.Name}</a></li>`;
                    });

                    html += '</ul>';

                    $('#avialable-stores').html(html);
                    commonUi.dropdownMenuContent.show();
                }
                );
            } else {
                commonUi.dropdownMenuContent.hide();
            }
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

function setStore(storeId) {
    postText('/Account/SetStore', { storeId: storeId }, () => {
        window.location.reload();
    });
}