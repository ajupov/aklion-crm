using System.ComponentModel.DataAnnotations;

namespace Crm.Enums
{
    public enum TokenType : byte
    {
        [Display(Name = "Не выбрано")]
        None = 0,

        [Display(Name = "Подтверждение Email")]
        EmailConfirmation = 1,

        [Display(Name = "Подтверждение телефона")]
        PhoneConfirmation = 2,

        [Display(Name = "Сброс пароля")]
        PasswordReset = 3
    }
}