using System;
using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.ClientAttribute;
using Aklion.Infrastructure.Mapper;
using DomainClientAttributeModel = Aklion.Crm.Domain.ClientAttribute.ClientAttributeModel;
using DomainClientAttributeParameterModel = Aklion.Crm.Domain.ClientAttribute.ClientAttributeParameterModel;
using DomainClientAttributeAutocompleteParameterModel = Aklion.Crm.Domain.ClientAttribute.ClientAttributeAutocompleteParameterModel;

namespace Aklion.Crm.Mappers.User.ClientAttribute
{
    public static class ClientAttributeMapper
    {
        public static PagingModel<ClientAttributeModel> MapNew(this Tuple<int, List<DomainClientAttributeModel>> tuple, int? page, int? size)
        {
            return new PagingModel<ClientAttributeModel>(tuple.Item2.MapListNew<ClientAttributeModel>(), tuple.Item1, page, size);
        }

        public static DomainClientAttributeModel MapNew(this ClientAttributeModel model, int storeId)
        {
            var result = model.MapNew<DomainClientAttributeModel>();
            result.StoreId = storeId;

            return result;
        }

        public static DomainClientAttributeModel MapFrom(this DomainClientAttributeModel domainModel, ClientAttributeModel model, int storeId)
        {
            var result = domainModel.MapFrom(model);
            result.StoreId = storeId;

            return result;
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
                Description = pattern,
                StoreId = storeId,
                IsDeleted = false
            };
        }
    }
}