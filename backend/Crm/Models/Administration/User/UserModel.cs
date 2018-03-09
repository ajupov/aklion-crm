using Crm.Enums;

namespace Crm.Models.Administration.User
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public Gender Gender { get; set; }

        public string BirthDate { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public bool IsPhoneConfirmed { get; set; }

        public bool IsLocked { get; set; }

        public bool IsDeleted { get; set; }

        public string AvatarUrl { get; set; }

        public string CreateDate { get; set; }

        public string ModifyDate { get; set; }
    }
}