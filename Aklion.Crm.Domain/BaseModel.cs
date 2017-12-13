using System;

namespace Aklion.Crm.Domain
{
    public class BaseModel
    {
        public int Id { get; }

        public DateTime CreateDate { get; }

        public DateTime? ModifyDate { get; }
    }
}