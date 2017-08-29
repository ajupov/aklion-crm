using System.Collections.Generic;
using System.Linq;
using ApiOrganization = Aklion.Crm.ApiV1.Models.Organization;
using DaoOrganization = Aklion.Crm.Dao.Organization.Models.Organization;

namespace Aklion.Crm.ApiV1.Mappers
{
    public static class OrganizationMapper
    {
        public static List<DaoOrganization> Map(this List<ApiOrganization> models)
        {
            return models?.Select(Map).ToList();
        }

        public static List<ApiOrganization> Map(this List<DaoOrganization> models)
        {
            return models?.Select(Map).ToList();
        }

        public static DaoOrganization Map(this ApiOrganization model)
        {
            return model == null
                ? null
                : new DaoOrganization
                {
                    Id = model.Id,
                    Name = model.Name,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate
                };
        }

        public static ApiOrganization Map(this DaoOrganization model)
        {
            return model == null
                ? null
                : new ApiOrganization
                {
                    Id = model.Id,
                    Name = model.Name,
                    IsDeleted = model.IsDeleted,
                    CreateDate = model.CreateDate,
                    ModifyDate = model.ModifyDate
                };
        }
    }
}