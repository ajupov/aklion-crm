﻿using System;
using Infrastructure.Dao.Attributes;

namespace Crm.Domain.OrderAttribute
{
    [WhereCombination("and")]
    public class OrderAttributeParameterModel
    {
        [Where("@Id is null or oa.Id = @Id")]
        public int? Id { get; set; }

        [Where("@StoreId is null or oa.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@StoreName is null or s.Name like @StoreName + '%'")]
        public string StoreName { get; set; }

        [Where("@Key is null or oa.[Key] like @Key + '%'")]
        public string Key { get; set; }

        [Where("@Name is null or oa.Name like @Name + '%'")]
        public string Name { get; set; }

        [Where("@IsDeleted is null or oa.IsDeleted = @IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Where("@CreateDate is null or convert(date, oa.CreateDate) = convert(date, @CreateDate)")]
        public DateTime? CreateDate { get; set; }

        [Where("@ModifyDate is null or convert(date, oa.ModifyDate) = convert(date, @ModifyDate)")]
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