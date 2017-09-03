'use strict';

var sharedUi = {
    modals: $('.modal'),
    modalTriggers: $('.modal-trigger'),
    modalCloses: $('.modal-close')
};

$(document).ready(() => {
    sharedUi.modalTriggers.click(e => {
        e.preventDefault();

        var selector = `#${$(e.target).attr('data-modal-id')}`;
        var modal = $(selector);
        modal.show();
    });

    sharedUi.modalCloses.click(e => {
        e.preventDefault();

        $(e.target).closest('.modal').hide();
    });

    $(window).click(e => {
        if ($(e.target).hasClass('modal')) {
            sharedUi.modals.hide();
        }
    });
});