﻿using Crm.Enums;

namespace Crm.Models.User.User
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

        public bool IsDeleted { get; set; }

        public string AvatarUrl { get; set; }

        public string CreateDate { get; set; }
    }
}