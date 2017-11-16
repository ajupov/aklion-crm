using System;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

namespace Aklion.Crm.Domain.ProductAttribute
{
    public class ProductAttributeParameterModel : ParameterModel
    {
        public int? Id { get; set; }

        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public int? ProductId { get; set; }

        public string ProductName { get; set; }

        public int? AttributeId { get; set; }

        public string AttributeName { get; set; }

        public string Value { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}