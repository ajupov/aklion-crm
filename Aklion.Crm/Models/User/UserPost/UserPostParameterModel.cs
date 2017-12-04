namespace Aklion.Crm.Models.User.UserPost
{
    public class UserPostParameterModel : ParameterModel
    {
        public int? Id { get; set; }

        public int? UserId { get; set; }

        public string UserLogin { get; set; }

        public int? PostId { get; set; }

        public string PostName { get; set; }

        public string CreateDate { get; set; }
    }
}