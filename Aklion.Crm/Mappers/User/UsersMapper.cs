﻿using System.Collections.Generic;
using System.Linq;
using Aklion.Crm.Domain.Models.User;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Users;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;
using Aklion.Infrastructure.Utils.DateTime;

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

        public static UserParameter Map(this UserParameterModel model)
        {
            return model == null
                ? null
                : new UserParameter
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

        public static void Map(this UserModel viewModel, Domain.Models.User.User domainModel)
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