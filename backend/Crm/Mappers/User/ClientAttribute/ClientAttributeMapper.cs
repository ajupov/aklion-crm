using System.Collections.Generic;
using System.Linq;
using Crm.Models;
using Crm.Models.User.ClientAttribute;
using Infrastructure.Mapper;
using DomainClientAttributeAutocompleteParameterModel = Crm.Domain.ClientAttribute.ClientAttributeAutocompleteParameterModel;
using ClientAttributeSelectParameterModel = Crm.Domain.ClientAttribute.ClientAttributeSelectParameterModel;
using DomainClientAttributeModel = Crm.Domain.ClientAttribute.ClientAttributeModel;
using DomainClientAttributeParameterModel = Crm.Domain.ClientAttribute.ClientAttributeParameterModel;

namespace Crm.Mappers.User.ClientAttribute
{
    public static class ClientAttributeMapper
    {
        public static PagingModel<ClientAttributeModel> MapNew(this (int TotalCount, List<DomainClientAttributeModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<ClientAttributeModel>(tuple.List.MapListNew<ClientAttributeModel>(), tuple.TotalCount, page, size);
        }

        public static DomainClientAttributeModel MapNew(this ClientAttributeModel model, int storeId)
        {
            var result = model.MapNew<DomainClientAttributeModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainClientAttributeModel MapFrom(this DomainClientAttributeModel domainModel, ClientAttributeModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainClientAttributeParameterModel MapNew(this ClientAttributeParameterModel model, int storeId)
        {
            var result = model.MapParameterNew<DomainClientAttributeParameterModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainClientAttributeAutocompleteParameterModel MapNew(this string pattern, int storeId)
        {
            return new DomainClientAttributeAutocompleteParameterModel
            {
                Name = pattern,
                StoreId = storeId,
                IsDeleted = false
            };
        }

        public static ClientAttributeSelectParameterModel MapNew(this int storeId)
        {
            return new ClientAttributeSelectParameterModel
            {
                StoreId = storeId,
                IsDeleted = false
            };
        }

        public static Dictionary<string, int> MapNew(this Dictionary<string, int> models)
        {
            models.TryAdd(string.Empty, 0);

            return models.OrderBy(k => k.Key).ToDictionary(k => k.Key, v => v.Value);
        }
    }
}