using System;
using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.Client;
using Aklion.Infrastructure.Mapper;
using DomainClientModel = Aklion.Crm.Domain.Client.ClientModel;
using DomainClientParameterModel = Aklion.Crm.Domain.Client.ClientParameterModel;
using DomainClientAutocompleteParameterModel = Aklion.Crm.Domain.Client.ClientAutocompleteParameterModel;

namespace Aklion.Crm.Mappers.User.Client
{
    public static class ClientMapper
    {
        public static PagingModel<ClientModel> MapNew(this Tuple<int, List<DomainClientModel>> tuple, int? page, int? size)
        {
            return new PagingModel<ClientModel>(tuple.Item2.MapListNew<ClientModel>(), tuple.Item1, page, size);
        }

        public static DomainClientModel MapNew(this ClientModel model, int storeId)
        {
            var result = model.MapNew<DomainClientModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainClientModel MapFrom(this DomainClientModel domainModel, ClientModel model, int storeId)
        {
            var result = domainModel.MapFrom(model);
            result.StoreId = storeId;

            return result;
        }

        public static DomainClientParameterModel MapNew(this ClientParameterModel model, int storeId)
        {
            var result = model.MapParameterNew<DomainClientParameterModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainClientAutocompleteParameterModel MapNew(this string pattern, int storeId)
        {
            return new DomainClientAutocompleteParameterModel
            {
                Name = pattern,
                StoreId = storeId,
                IsDeleted = false
            };
        }
    }
}