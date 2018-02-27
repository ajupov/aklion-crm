using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.Client;
using Aklion.Infrastructure.Mapper;
using DomainClientAutocompleteParameterModel = Aklion.Crm.Domain.Client.ClientAutocompleteParameterModel;
using DomainClientModel = Aklion.Crm.Domain.Client.ClientModel;
using DomainClientParameterModel = Aklion.Crm.Domain.Client.ClientParameterModel;

namespace Aklion.Crm.Mappers.User.Client
{
    public static class ClientMapper
    {
        public static PagingModel<ClientModel> MapNew(this (int TotalCount, List<DomainClientModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<ClientModel>(tuple.List.MapListNew<ClientModel>(), tuple.TotalCount, page, size);
        }

        public static DomainClientModel MapNew(this ClientModel model, int storeId)
        {
            var result = model.MapNew<DomainClientModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainClientModel MapFrom(this DomainClientModel domainModel, ClientModel model)
        {
            return Mapper.MapFrom(domainModel, model);
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