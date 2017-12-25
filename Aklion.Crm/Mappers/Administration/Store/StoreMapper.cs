﻿using System;
using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Store;
using Aklion.Infrastructure.Mapper;
using DomainStoreModel = Aklion.Crm.Domain.Store.StoreModel;
using DomainStoreParameterModel = Aklion.Crm.Domain.Store.StoreParameterModel;
using DomainStoreAutocompleteParameterModel = Aklion.Crm.Domain.Store.StoreAutocompleteParameterModel;

namespace Aklion.Crm.Mappers.Administration.Store
{
    public static class StoreMapper
    {
        public static PagingModel<StoreModel> MapNew(this Tuple<int, List<DomainStoreModel>> tuple, int? page, int? size)
        {
            return new PagingModel<StoreModel>(tuple.Item2.MapListNew<StoreModel>(), tuple.Item1, page, size);
        }

        public static DomainStoreModel MapNew(this StoreModel model)
        {
            return model.MapNew<DomainStoreModel>();
        }

        public static DomainStoreModel MapFrom(this DomainStoreModel domainModel, StoreModel model)
        {
            return Mapper.MapFrom(domainModel, model);
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