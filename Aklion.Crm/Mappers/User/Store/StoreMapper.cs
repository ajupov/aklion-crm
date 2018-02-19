using System;
using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.Store;
using Aklion.Infrastructure.Mapper;
using DomainStoreModel = Aklion.Crm.Domain.Store.StoreModel;
using DomainStoreByUserModel = Aklion.Crm.Domain.Store.StoreByUserModel;
using DomainStoreParameterModel = Aklion.Crm.Domain.Store.StoreByUserParameterModel;
using DomainStoreAutocompleteParameterModel = Aklion.Crm.Domain.Store.StoreAutocompleteParameterModel;

namespace Aklion.Crm.Mappers.User.Store
{
    public static class StoreMapper
    {
        public static PagingModel<StoreModel> MapNew(this Tuple<int, List<DomainStoreByUserModel>> tuple, int? page, int? size)
        {
            return new PagingModel<StoreModel>(tuple.Item2.MapListNew<StoreModel>(), tuple.Item1, page, size);
        }

        public static DomainStoreModel MapNew(this StoreModel model, int storeId)
        {
            return model.MapNew<DomainStoreModel>();
        }

        public static DomainStoreModel MapFrom(this DomainStoreModel domainModel, StoreModel model, int storeId)
        {
            return domainModel.MapFrom(model);
        }

        public static DomainStoreParameterModel MapNew(this StoreParameterModel model, int storeId)
        {
            return model.MapParameterNew<DomainStoreParameterModel>();
        }

        public static DomainStoreAutocompleteParameterModel MapNew(this string pattern)
        {
            return new DomainStoreAutocompleteParameterModel {Name = pattern};
        }
    }
}