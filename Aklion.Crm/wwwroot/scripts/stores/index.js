'use strict';

var storesUi = {
    stores: $('#stores'),
    addStoreForm: $('#add-store-form'),
    addStoreModal: $('#add-store-modal')
};

function getList() {
    getHtml(urls.storesGetList,
        {},
        result => storesUi.stores.html(result),
        error => storesUi.stores.html(error));
}

function getItem(id) {
    getHtml(urls.storesGet,
        {},
        result => storesUi.stores.html(result),
        error => storesUi.stores.html(error));
    alert(id);
}

function deleteItem(id) {
    alert(id);
}

$(document).ready(() => {
    getList();

    storesUi.addStoreForm.submit(e => {
        e.preventDefault();

        if (storesUi.addStoreForm.valid()) {
            postJson(urls.storesAdd,
                storesUi.addStoreForm.serialize(),
                () => {
                    storesUi.addStoreModal.hide();
                    storesUi.addStoreForm[0].reset();
                    getList();
                },
                () => alert('Произошла ошбка'));
        }

        return;
    });
});