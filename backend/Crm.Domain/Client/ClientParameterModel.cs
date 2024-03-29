﻿using System;
using Infrastructure.Dao.Attributes;

namespace Crm.Domain.Client
{
    [WhereCombination("and")]
    public class ClientParameterModel
    {
        [Where("@Id is null or c.Id = @Id")]
        public int? Id { get; set; }

        [Where("@StoreId is null or c.StoreId = @StoreId")]
        public int? StoreId { get; set; }

        [Where("@Name is null or c.Name like @Name + '%'")]
        public string Name { get; set; }

        [Where("@IsDeleted is null or c.IsDeleted = @IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Where("@CreateDate is null or convert(date, c.CreateDate) = convert(date, @CreateDate)")]
        public DateTime? CreateDate { get; set; }

        [Where("@ModifyDate is null or convert(date, c.ModifyDate) = convert(date, @ModifyDate)")]
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