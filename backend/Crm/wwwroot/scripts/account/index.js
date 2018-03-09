'use strict';

$(document).ready(() => {

    $('#load-image-link').click(e => {
        event.preventDefault();
        event.stopPropagation();

        $('#load-image-dialog').dialog('open');
    });
});