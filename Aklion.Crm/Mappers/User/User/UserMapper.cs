using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Infrastructure.DateTime;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Pagingation;
using UserModel = Aklion.Crm.Models.User.User.UserModel;
using UserParameterModel = Aklion.Crm.Models.Administration.User.UserParameterModel;

namespace Aklion.Crm.Mappers.User.User
{
    public static class UserMapper
    {
        public static PagingModel<UserModel> Map(this Paging<Models.Administration.User.UserModel> model, int storeId, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<UserModel>(model.List.Map(storeId), model.TotalCount, page, size);
        }

        private static List<UserModel> Map(this IEnumerable<Models.Administration.User.UserModel> models, int storeId)
        {
            return models?.Select(x => x.Map(storeId)).ToList();
        }

        public static UserModel Map(this Models.Administration.User.UserModel model, int storeId)
        {
            return model == null
                ? null
                : new UserModel
                {
                    Id = model.Id,
                    Login = model.Login,
                    Email = model.Email,
                    Phone = model.Phone,
                    Surname = model.Surname,
                    Name = model.Name,
                    Patronymic = model.Patronymic,
                    Gender = model.Gender,
                    BirthDate = model.BirthDate,
                    CreateDate = model.CreateDate
                };
        }

        public static UserParameterModel Map(this Models.User.User.UserParameterModel model, int storeId)
        {
            return model == null
                ? null
                : new UserParameterModel
                {
                    Id = model.Id,
                    Login = model.Login,
                    Email = model.Email,
                    Phone = model.Phone,
                    Surname = model.Surname,
                    Name = model.Name,
                    Patronymic = model.Patronymic,
                    Gender = model.Gender,
                    BirthDate = model.BirthDate.ToNullableDate(),
                    IsEmailConfirmed = false,
                    IsPhoneConfirmed = false,
                    IsLocked = false,
                    IsDeleted = false,
                    CreateDate = model.CreateDate.ToNullableDate(),
                    ModifyDate = null,
                    IsSearch = model.IsSearch,
                    Timestamp = model.Timestamp,
                    SortingColumn = model.SortingColumn,
                    SortingOrder = model.SortingOrder,
                    Page = model.Page - 1,
                    Size = model.Size
                };
        }

        public static void Map(this UserModel viewModel, Models.Administration.User.UserModel domainModel, int storeId)
        {
            domainModel.Id = viewModel.Id;
            domainModel.Login = viewModel.Login;
            domainModel.Email = viewModel.Email;
            domainModel.Phone = viewModel.Phone;
            domainModel.Surname = viewModel.Surname;
            domainModel.Name = viewModel.Name;
            domainModel.Patronymic = viewModel.Patronymic;
            domainModel.Gender = viewModel.Gender;
            domainModel.BirthDate = viewModel.BirthDate;
            domainModel.CreateDate = viewModel.CreateDate;
        }
    }
}