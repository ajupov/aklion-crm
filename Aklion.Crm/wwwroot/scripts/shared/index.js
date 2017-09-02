'use strict';

var sharedUi = {
};

function getHtml(address, successFunction, errorFunction) {
    $.get(address,
        result => successFunction(result),
        'html').fail(error => errorFunction(error));
}