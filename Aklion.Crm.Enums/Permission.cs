using System.ComponentModel.DataAnnotations;

namespace Aklion.Crm.Enums
{
    public enum Permission : byte
    {
        //Привязка
        [Display(Name = "")]
        None = 0,

        //Администратор
        [Display(Name = "Администратор")]
        Admin = 1,

        //Главная учетная запись
        [Display(Name = "Управление пользователями")]
        UserManagement = 2,
        
        //Главная учетная запись
        [Display(Name = "Управление магазинами")]
        StoreManagement = 3,

        //Менеджер по продуктам
        [Display(Name = "Управление продуктами")]
        ProductManagement = 4,

        //Менеджер по клиентам
        [Display(Name = "Управление клиентами")]
        ClientManagement = 5,

        //Менеджер по продажам
        [Display(Name = "Управление заказами")]
        OrderManagement = 6
    }
}