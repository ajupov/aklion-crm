using System.Collections.Generic;
using Crm.Models;
using Crm.Models.Administration.Store;
using Infrastructure.Mapper;
using DomainStoreAutocompleteParameterModel = Crm.Domain.Store.StoreAutocompleteParameterModel;
using DomainStoreModel = Crm.Domain.Store.StoreModel;
using DomainStoreParameterModel = Crm.Domain.Store.StoreParameterModel;

namespace Crm.Mappers.Administration.Store
{
    public static class StoreMapper
    {
        public static PagingModel<StoreModel> MapNew(this (int TotalCount, List<DomainStoreModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<StoreModel>(tuple.Item2.MapListNew<StoreModel>(), tuple.Item1, page, size);
        }

        public static DomainStoreModel MapNew(this StoreModel model)
        {
            return model.MapNew<DomainStoreModel>();
        }

        public static DomainStoreModel MapFrom(this DomainStoreModel domainModel, StoreModel model)
        {
            var apiSecret = domainModel.ApiSecret;
            var result = Mapper.MapFrom(domainModel, model);
            domainModel.ApiSecret = apiSecret;
            return result;
        }

        public static DomainStoreParameterModel MapNew(this StoreParameterModel model)
        {
            return model.MapParameterNew<DomainStoreParameterModel>();
        }

        public static DomainStoreAutocompleteParameterModel MapNew(this string pattern)
        {
            return new DomainStoreAutocompleteParameterModel {Name = pattern};
        }
    }
}