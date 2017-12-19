using System;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.Store
{
    [WhereCombination("and")]
    public class StoreParameterModel
    {
        [Where("@Id is null or s.Id = @Id")]
        public int? Id { get; set; }

        [Where("@Name is null or s.Name = @Name")]
        public string Name { get; set; }

        [Where("@ApiSecret is null or s.ApiSecret = @ApiSecret")]
        public string ApiSecret { get; set; }

        [Where("@IsLocked is null or s.IsLocked = @IsLocked")]
        public bool? IsLocked { get; set; }

        [Where("@IsDeleted is null or s.IsDeleted = @IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Where("@CreateDate is null or convert(date, s.CreateDate) = convert(date, @CreateDate)")]
        public DateTime? CreateDate { get; set; }

        [Where("@ModifyDate is null or convert(date, s.ModifyDate) = convert(date, @ModifyDate)")]
        public DateTime? ModifyDate { get; set; }

        [SortingColumn]
        public string SortingColumn { get; set; }

        [SortingOrder]
        public string SortingOrder { get; set; }

        [Page]
        public int? Page { get; set; }

        [Size]
        public int? Size { get; set; }
    }
}