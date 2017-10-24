using System.Collections.Generic;
using System.Linq;
using ViewListModel = Aklion.Crm.Models.BaseListModel<Aklion.Crm.Models.Users.User>;
using DomainModel = Aklion.Crm.Domain.Models.User.UserModel;
using DomainListModel2 = System.Collections.Generic.KeyValuePair<int, System.Collections.Generic.List<Aklion.Crm.Domain.Models.User.UserModel>>;

namespace Aklion.Crm.Mappers.User
{
    public static class UsersMapper
    {
        #region Domain to View
        public static ViewListModel Map(this DomainListModel2 model, int page, int size)
        {
            return model.Value != null 
                ? new ViewListModel(model.Value.Map(), model.Key, page, size)
                : null;
        }

        private static List<Models.Users.User> Map(this IEnumerable<DomainModel> models)
        {
            return models?.Select(Map).ToList();
        }

        public static Models.Users.User Map(this DomainModel model)
        {
            return model != null
                ? new Models.Users.User
                {
                     Id = model.Id,
                    Email = model.Email,
                    Phone = model.Phone,
                    Surname = model.Surname,
                    Name = model.Name,
                    Patronymic = model.Patronymic,
                    Gender = model.Gender,
                    BirthDate = model.BirthDate,
                    IsEmailConfirmed = model.IsEmailConfirmed,
                    IsPhoneConfirmed = model.IsPhoneConfirmed,
                    IsLocked = model.IsLocked,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate,
                }
                : null;
        }
        #endregion

        #region View to Domain
        public static DomainModel Map(this Models.Users.User model)
        {
            return model != null
                ? new DomainModel
                {
                    Id = model.Id,
                    Email = model.Email,
                    Phone = model.Phone,
                    Surname = model.Surname,
                    Name = model.Name,
                    Patronymic = model.Patronymic,
                    Gender = model.Gender,
                    BirthDate = model.BirthDate,
                    IsEmailConfirmed = model.IsEmailConfirmed,
                    IsPhoneConfirmed = model.IsPhoneConfirmed,
                    IsLocked = model.IsLocked,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate,
                }
                : null;
        }
        #endregion
    }
}