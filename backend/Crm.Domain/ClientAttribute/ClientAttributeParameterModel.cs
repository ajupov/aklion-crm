﻿using System;
using Infrastructure.Dao.Attributes;

namespace Crm.Domain.ClientAttribute
{
    [WhereCombination("and")]
    public class ClientAttributeParameterModel
    {
        [Where("@Id is null or ca.Id = @Id")]
        public int? Id { get; set; }

        [Where("@StoreId is null or ca.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@StoreName is null or s.Name like @StoreName + '%'")]
        public string StoreName { get; set; }

        [Where("@Key is null or ca.[Key] like @Key + '%'")]
        public string Key { get; set; }

        [Where("@Name is null or ca.Name like @Name + '%'")]
        public string Name { get; set; }

        [Where("@IsDeleted is null or ca.IsDeleted = @IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Where("@CreateDate is null or convert(date, ca.CreateDate) = convert(date, @CreateDate)")]
        public DateTime? CreateDate { get; set; }

        [Where("@ModifyDate is null or convert(date, ca.ModifyDate) = convert(date, @ModifyDate)")]
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