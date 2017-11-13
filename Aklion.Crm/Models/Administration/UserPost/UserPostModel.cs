using System;

namespace Aklion.Crm.Models.Administration.UserPost
{
    public class UserPostModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string UserLogin { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public int PostId { get; set; }

        public string PostName { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}