using System.ComponentModel.DataAnnotations;

namespace Aklion.Crm.Enums
{
    public enum AuditLogObjectType : byte
    {
        [Display(Name = "Не выбрано")]
        None = 0,

        [Display(Name = "Пользователь")]
        User = 1,

        [Display(Name = "Токен пользователя")]
        UserToken = 2,

        [Display(Name = "Право пользователя")]
        UserPermission = 3,

        [Display(Name = "Атрибут пользователя")]
        UserAttribute = 4,

        [Display(Name = "Значение атрибута пользователя")]
        UserAttributeLink = 5,

        [Display(Name = "Магазин")]
        Store = 6,

        [Display(Name = "Продукт")]
        Product = 7,

        [Display(Name = "Статус продукта")]
        ProductStatus = 8,

        [Display(Name = "Атрибут продукта")]
        ProductAttribute = 9,

        [Display(Name = "Значение атрибута продукта")]
        ProductAttributeLink = 10,

        [Display(Name = "Клиент")]
        Client = 11,

        [Display(Name = "Атрибут клиента")]
        ClientAttribute = 12,

        [Display(Name = "Значение атрибута клиента")]
        ClientAttributeLink = 13,

        [Display(Name = "Заказ")]
        Order = 14,

        [Display(Name = "Источник заказа")]
        OrderSource = 15,

        [Display(Name = "Статус заказа")]
        OrderStatus = 16,

        [Display(Name = "Атрибут заказа")]
        OrderAttribute = 17,

        [Display(Name = "Значение атрибута заказа")]
        OrderAttributeLink = 18
    }
}