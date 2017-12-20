using System;
using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.Client;
using Aklion.Infrastructure.Mapper;
using DomainClientModel = Aklion.Crm.Domain.Client.ClientModel;
using DomainClientParameterModel = Aklion.Crm.Domain.Client.ClientParameterModel;
using DomainClientAutocompleteParameterModel = Aklion.Crm.Domain.Client.ClientAutocompleteParameterModel;

namespace Aklion.Crm.Mappers.Administration.Client
{
    public static class ClientMapper
    {
        public static PagingModel<ClientModel> MapNew(this Tuple<int, List<DomainClientModel>> tuple, int? page, int? size)
        {
            return new PagingModel<ClientModel>(tuple.Item2.MapListNew<ClientModel>(), tuple.Item1, page, size);
        }

        public static DomainClientModel MapNew(this ClientModel model)
        {
            return model.MapNew<DomainClientModel>();
        }

        public static DomainClientModel MapFrom(this DomainClientModel domainModel, ClientModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainClientParameterModel MapNew(this ClientParameterModel model)
        {
            return model.MapNew<DomainClientParameterModel>();
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