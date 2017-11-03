using System;
using Aklion.Crm.Enums;

namespace Aklion.Crm.Domain.User
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public bool IsPhoneConfirmed { get; set; }

        public bool IsLocked { get; set; }

        public bool IsDeleted { get; set; }

        public string AvatarUrl { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}