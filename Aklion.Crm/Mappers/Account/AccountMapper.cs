using Aklion.Crm.Domain.User;
using Aklion.Crm.Models.Account;
using Aklion.Infrastructure.DateTime;
using Aklion.Infrastructure.Password;
using Aklion.Infrastructure.PhoneNumber;

namespace Aklion.Crm.Mappers.Account
{
    public static class AccountMapper
    {
        public static UserModel Map(this RegisterModel model)
        {
            return model == null
                ? null
                : new UserModel
                {
                    Login = model.Login,
                    PasswordHash = PasswordHelper.Generate(model.Password),
                    Email = model.Email,
                    Phone = model.Phone.ExtractPhoneNumber(),
                    Surname = model.Surname,
                    Name = model.Name,
                    Patronymic = model.Patronymic,
                    Gender = model.Gender,
                    BirthDate = model.BirthDateString.ToDate(),
                    IsEmailConfirmed = false,
                    IsPhoneConfirmed = false,
                    IsLocked = false,
                    IsDeleted = false
                };
        }

        public static ChangePersonalInfoModel Map(this UserModel model)
        {
            return model == null
                ? null
                : new ChangePersonalInfoModel
                {
                    Surname = model.Surname,
                    Name = model.Name,
                    Patronymic = model.Patronymic,
                    Gender = model.Gender,
                    BirthDateString = model.BirthDate.ToDateString()
                };
        }

        public static void Map(this ChangePersonalInfoModel model, UserModel domainModel)
        {
            domainModel.Surname = model.Surname;
            domainModel.Name = model.Name;
            domainModel.Patronymic = model.Patronymic;
            domainModel.Gender = model.Gender;
            domainModel.BirthDate = model.BirthDateString.ToDate();
        }
    }
}