﻿using Aklion.Crm.Enums;

namespace Aklion.Crm.Models.User.User
{
    public class UserParameterModel : ParameterModel
    {
        public int? Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public Gender? Gender { get; set; }

        public string BirthDate { get; set; }

        public string CreateDate { get; set; }
    }
}