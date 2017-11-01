﻿using System;
using Aklion.Crm.Enums;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

namespace Aklion.Crm.Domain.Models.User
{
    public class UserParameter : Parameter
    {
        public int? Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public Gender? Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public bool? IsEmailConfirmed { get; set; }

        public bool? IsPhoneConfirmed { get; set; }

        public bool? IsLocked { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}