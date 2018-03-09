using Crm.Domain.User;
using Crm.Models.Account;
using Infrastructure.Mapper;

namespace Crm.Mappers.Account
{
    public static class AccountMapper
    {
        public static UserModel MapNew(this RegisterModel model)
        {
            return model.MapNew<UserModel>();
        }

        public static ChangePersonalInfoModel MapNew(this UserModel model)
        {
            return model.MapNew<ChangePersonalInfoModel>();
        }

        public static UserModel MapFrom(this UserModel domainModel, ChangePersonalInfoModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }
    }
}