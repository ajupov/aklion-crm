using System;
using Aklion.Crm.Attributes;
using Aklion.Crm.Enums;

namespace Aklion.Crm.Models.Users
{
    [Table(true)]
    public class User
    {
        [TableColumn("№", 80)]
        public int Id { get; set; }

        [TableColumn("Email", 160)]
        public string Email { get; set; }

        [TableColumn("Телефон", 100)]
        public string Phone { get; set; }

        [TableColumn("Фамилия", 180)]
        public string Surname { get; set; }

        [TableColumn("Имя", 140)]
        public string Name { get; set; }

        [TableColumn("Отчество", 180)]
        public string Patronymic { get; set; }

        [TableColumn("Пол", 70)]
        public Gender Gender { get; set; }

        [TableColumn("Дата рождения", 75)]
        public DateTime BirthDate { get; set; }

        [TableColumn("Email подтверждён", 100)]
        public bool IsEmailConfirmed { get; set; }

        [TableColumn("Телефон подтверждён", 100)]
        public bool IsPhoneConfirmed { get; set; }

        [TableColumn("Заблокирован", 100)]
        public bool IsLocked { get; set; }

        [TableColumn("Удалён", 60)]
        public bool IsDeleted { get; set; }

        [TableColumn("Дата создания", 130)]
        public DateTime CreateDate { get; set; }

        [TableColumn("Дата изменения", 130)]
        public DateTime ModifyDate { get; set; }
    }
}