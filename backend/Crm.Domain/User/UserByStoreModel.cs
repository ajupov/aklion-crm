using System;
using Crm.Enums;
using Infrastructure.Dao.Attributes;

namespace Crm.Domain.User
{
    [Table("dbo.[User] as u")]
    [Join("inner join dbo.UserPermission as up on u.Id = up.UserId")]
    public class UserByStoreModel
    {
        [Column("u.Id")]
        [Identificator]
        public int Id { get; set; }

        [Column("up.StoreId")]
        public int StoreId { get; set; }

        [Column("u.Login")]
        [AutocompleteOrSelect("u.Login")]
        public string Login { get; set; }

        [Column("u.PasswordHash")]
        public string PasswordHash { get; set; }

        [Column("u.Email")]
        public string Email { get; set; }

        [Column("u.Phone")]
        public string Phone { get; set; }

        [Column("u.Surname")]
        public string Surname { get; set; }

        [Column("u.Name")]
        public string Name { get; set; }

        [Column("u.Patronymic")]
        public string Patronymic { get; set; }

        [Column("u.Gender")]
        public Gender Gender { get; set; }

        [Column("u.BirthDate")]
        public DateTime? BirthDate { get; set; }

        [Column("u.IsEmailConfirmed")]
        public bool IsEmailConfirmed { get; set; }

        [Column("u.IsPhoneConfirmed")]
        public bool IsPhoneConfirmed { get; set; }

        [Column("u.IsLocked")]
        public bool IsLocked { get; set; }

        [Column("u.IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("u.AvatarUrl")]
        public string AvatarUrl { get; set; }

        [Column("u.CreateDate")]
        public DateTime CreateDate { get; set; }

        [Column("u.ModifyDate")]
        public DateTime? ModifyDate { get; set; }
    }
}