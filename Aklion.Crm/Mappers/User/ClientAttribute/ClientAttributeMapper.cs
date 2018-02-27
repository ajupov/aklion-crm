using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.ClientAttribute;
using Aklion.Infrastructure.Mapper;
using DomainClientAttributeAutocompleteParameterModel = Aklion.Crm.Domain.ClientAttribute.ClientAttributeAutocompleteParameterModel;
using DomainClientAttributeModel = Aklion.Crm.Domain.ClientAttribute.ClientAttributeModel;
using DomainClientAttributeParameterModel = Aklion.Crm.Domain.ClientAttribute.ClientAttributeParameterModel;

namespace Aklion.Crm.Mappers.User.ClientAttribute
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
    }
}