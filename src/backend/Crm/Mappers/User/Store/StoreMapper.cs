using System.Collections.Generic;
using Crm.Models;
using Crm.Models.User.Store;
using Infrastructure.Mapper;
using DomainStoreAutocompleteParameterModel = Crm.Domain.Store.StoreAutocompleteParameterModel;
using DomainStoreByUserModel = Crm.Domain.Store.StoreByUserModel;
using DomainStoreModel = Crm.Domain.Store.StoreModel;
using DomainStoreParameterModel = Crm.Domain.Store.StoreByUserParameterModel;

namespace Crm.Mappers.User.Store
{
    public static class StoreMapper
    {
        public static PagingModel<StoreModel> MapNew(this (int TotalCount, List<DomainStoreByUserModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<StoreModel>(tuple.List.MapListNew<StoreModel>(), tuple.TotalCount, page, size);
        }

        public static DomainStoreModel MapNew(this StoreModel model)
        {
            return model.MapParameterNew<DomainStoreModel>();
        }

        public static DomainStoreModel MapFrom(this DomainStoreModel domainModel, StoreModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainStoreParameterModel MapNew(this StoreParameterModel model, int userId)
        {
            var result = model.MapParameterNew<DomainStoreParameterModel>();
            result.UserId = userId;

            return result;
        }

        public static DomainStoreAutocompleteParameterModel MapNew(this string pattern, int userId)
        {
            return new DomainStoreAutocompleteParameterModel
            {
                Name = pattern
            };
        }
    }
}