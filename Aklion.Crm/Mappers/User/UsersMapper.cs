using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Domain.Models.User;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Users;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

namespace Aklion.Crm.Mappers.User
{
    public static class UsersMapper
    {
        public static PagingModel<UserModel> Map(this Paging<Domain.Models.User.User> model, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<UserModel>(model.List.Map(), model.TotalCount, page, size);
        }

        private static List<UserModel> Map(this IEnumerable<Domain.Models.User.User> models)
        {
            return models?.Select(Map).ToList();
        }

        public static UserModel Map(this Domain.Models.User.User model)
        {
            return model == null
                ? null
                : new UserModel
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
                    ModifyDate = model.ModifyDate
                };
        }

        public static UserParameter Map(this UserParameterModel model)
        {
            return model == null
                ? null
                : new UserParameter
                {
                    IsSearch = model.IsSearch,
                    Timestamp = model.Timestamp,
                    SortingColumn = model.SortingColumn,
                    SortingOrder = model.SortingOrder,
                    Page = model.Page - 1,
                    Size = model.Size
                };
        }
    }
}