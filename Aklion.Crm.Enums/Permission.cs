using System.ComponentModel.DataAnnotations;

namespace Aklion.Crm.Enums
{
    public enum Permission
    {
        [Display(Name = "")]
        None = 0,

        //Администратор
        [Display(Name = "Администратор")]
        Admin = 1,

        //Владелец магазаина
        [Display(Name = "Управление правами")]
        PermissionManagement = 2,

        [Display(Name = "Управление магазинами")]
        StoreManagement = 3,

        [Display(Name = "Управление пользователями")]
        UserManagement = 4,
        
        //Менеджер по кадрам
        [Display(Name = "Управление должностями")]
        PostManagement = 5,

        [Display(Name = "Управление должностями пользователя")]
        UserPostManagement = 6,

        //Менеджер по продуктам
        [Display(Name = "Управление продуктами")]
        ProductManagement = 7,

        [Display(Name = "Управление категориями")]
        CategoryManagement = 8,

        [Display(Name = "Управление категориями продукта")]
        ProductCategoryManagement = 9,

        [Display(Name = "Управление атрибутами")]
        AttributeManagement = 10,

        [Display(Name = "Управление атрибутами продукта")]
        ProductAttributeManagement = 11,

        [Display(Name = "Управление тэгами")]
        TagManagement = 12,

        [Display(Name = "Управление тэгами продукта")]
        ProductTagManagement = 13,

        //Менеджер по клиентам
        [Display(Name = "Управление клиентами")]
        ClientManagement = 14,

        [Display(Name = "Управление адресами клиента")]
        ClientAddressManagement = 15,

        [Display(Name = "Управление телефонами клиента")]
        ClientPhoneManagement = 16,

        //Менеджер по продажам
        [Display(Name = "Управление заказами")]
        OrderManagement = 17,

        [Display(Name = "Управление позициями заказа")]
        OrderItemManagement = 18
    }
}