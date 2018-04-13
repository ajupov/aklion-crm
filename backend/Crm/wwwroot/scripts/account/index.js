'use strict';

$(document).ready(() => {

    $('#load-image-link').click(event => {
        event.preventDefault();
        event.stopPropagation();

        $('#load-image-dialog').dialog('open');
    });
});