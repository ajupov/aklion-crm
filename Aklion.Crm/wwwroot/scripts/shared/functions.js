'use strict';

function getHtml(address, data, successFunction, errorFunction) {
    $.get(address, data,
        result => successFunction(result),
        'html').fail(error => errorFunction(error));
}

function getJson(address, data, successFunction, errorFunction) {
    $.get(address, data,
        result => successFunction(result),
        'json').fail(error => errorFunction(error));
}

function postJson(address, data, successFunction, errorFunction) {
    $.post(address, data,
        result => successFunction(result),
        'json').fail(error => errorFunction(error));
}