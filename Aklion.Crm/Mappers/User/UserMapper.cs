using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Models;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;
using Aklion.Infrastructure.Utils.DateTime;
using UserModel = Aklion.Crm.Models.Administration.User.UserModel;
using UserParameterModel = Aklion.Crm.Domain.User.UserParameterModel;

namespace Aklion.Crm.Mappers.User
{
    public static class UserMapper
    {
        public static PagingModel<UserModel> Map(this Paging<Domain.User.UserModel> model, int page, int size)
        {
            return model == null
                ? null
                : new PagingModel<UserModel>(model.List.Map(), model.TotalCount, page, size);
        }

        private static List<UserModel> Map(this IEnumerable<Domain.User.UserModel> models)
        {
            return models?.Select(Map).ToList();
        }

        public static UserModel Map(this Domain.User.UserModel model)
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
                    IsEmailConfirmed = model.IsEmailConfirmed,
                    IsPhoneConfirmed = model.IsPhoneConfirmed,
                    IsLocked = model.IsLocked,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate
                };
        }

        public static UserParameterModel Map(this Models.Administration.User.UserParameterModel model)
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
                    IsEmailConfirmed = model.IsEmailConfirmed,
                    IsPhoneConfirmed = model.IsPhoneConfirmed,
                    IsLocked = model.IsLocked,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate.ToNullableDate(),
                    ModifyDate = model.ModifyDate.ToNullableDate(),
                    IsSearch = model.IsSearch,
                    Timestamp = model.Timestamp,
                    SortingColumn = model.SortingColumn,
                    SortingOrder = model.SortingOrder,
                    Page = model.Page - 1,
                    Size = model.Size
                };
        }

        public static void Map(this UserModel viewModel, Domain.User.UserModel domainModel)
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
            domainModel.IsEmailConfirmed = viewModel.IsEmailConfirmed;
            domainModel.IsPhoneConfirmed = viewModel.IsPhoneConfirmed;
            domainModel.IsLocked = viewModel.IsLocked;
            domainModel.IsDeleted = viewModel.IsDeleted;
            domainModel.CreateDate = viewModel.CreateDate;
            domainModel.ModifyDate = viewModel.ModifyDate;
        }
    }
}