'use strict';

function initGeneral() {
    getJson('/Console/GetGeneralCounts', null, (result) => {
        const html = `
            <p>Всего пользователей: ${result.UsersCount}</p>
            <p>Всего магазинов: ${result.StoresCount}</p>
            <p>Всего продуктов: ${result.ProductsCount}</p>
            <p>Всего клиентов: ${result.ClientsCount}</p>
            <p>Всего заказов: ${result.OrdersCount}</p>`;

        $('#general-div').html(html);
    });
}