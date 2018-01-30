using System.ComponentModel.DataAnnotations;

namespace Aklion.Crm.Enums
{
    public enum Permission : byte
    {
        [Display(Name = "")]
        None = 0,

        //Администратор
        [Display(Name = "Администратор")]
        Admin = 1,

        //Главная учетная запись
        [Display(Name = "Управление пользователями")]
        UserManagement = 2,

        [Display(Name = "Управление магазинами")]
        StoreManagement = 3,

        //Менеджер по продуктам
        [Display(Name = "Управление продуктами")]
        ProductManagement = 7,

        //Менеджер по клиентам
        [Display(Name = "Управление клиентами")]
        ClientManagement = 14,

        //Менеджер по продажам
        [Display(Name = "Управление заказами")]
        OrderManagement = 17
    }
}