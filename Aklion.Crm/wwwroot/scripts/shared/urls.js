'use strict';

var currentHost = `${window.location.protocol}//${window.location.host}`;

var urls = {
    storesGetList: `${currentHost}/Stores/GetList`,
    storesGet: `${currentHost}/Stores/Get`,
    storesAdd: `${currentHost}/Stores/Add`,

    socket: `ws://${window.location.host}/ws`,
    audioNewMessage: `${currentHost}/audio/NewMessage.mp3`
};