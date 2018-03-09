using System.Collections.Generic;
using Crm.Models;
using Crm.Models.Administration.ClientAttribute;
using Infrastructure.Mapper;
using DomainClientAttributeAutocompleteParameterModel =
    Crm.Domain.ClientAttribute.ClientAttributeAutocompleteParameterModel;
using DomainClientAttributeModel = Crm.Domain.ClientAttribute.ClientAttributeModel;
using DomainClientAttributeParameterModel = Crm.Domain.ClientAttribute.ClientAttributeParameterModel;

namespace Crm.Mappers.Administration.ClientAttribute
{
    public static class ClientAttributeMapper
    {
        public static PagingModel<ClientAttributeModel> MapNew(this (int TotalCount, List<DomainClientAttributeModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<ClientAttributeModel>(tuple.List.MapListNew<ClientAttributeModel>(), tuple.TotalCount, page, size);
        }

        public static DomainClientAttributeModel MapNew(this ClientAttributeModel model)
        {
            return model.MapNew<DomainClientAttributeModel>();
        }

        public static DomainClientAttributeModel MapFrom(this DomainClientAttributeModel domainModel, ClientAttributeModel model)
        {
            return Mapper.MapFrom(domainModel, model);
        }

        public static DomainClientAttributeParameterModel MapNew(this ClientAttributeParameterModel model)
        {
            return model.MapParameterNew<DomainClientAttributeParameterModel>();
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