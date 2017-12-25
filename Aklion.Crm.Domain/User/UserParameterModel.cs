using System;
using Aklion.Crm.Enums;
using Aklion.Infrastructure.Dao.Attributes;

namespace Aklion.Crm.Domain.User
{
    [WhereCombination("and")]
    public class UserParameterModel
    {
        [Where("@Id is null or u.Id = @Id")]
        public int? Id { get; set; }

        [Where("@Login is null or u.Login like @Login + '%'")]
        public string Login { get; set; }

        [Where("@Email is null or u.Email like @Email + '%'")]
        public string Email { get; set; }

        [Where("@Phone is null or u.Phone like @Phone + '%'")]
        public string Phone { get; set; }

        [Where("@Surname is null or u.Surname like @Surname + '%'")]
        public string Surname { get; set; }

        [Where("@Name is null or u.Name like @Name + '%'")]
        public string Name { get; set; }

        [Where("@Patronymic is null or u.Patronymic like @Patronymic + '%'")]
        public string Patronymic { get; set; }

        [Where("coalesce(@Gender, 0) = 0 or u.Gender = @Gender")]
        public Gender? Gender { get; set; }

        [Where("@BirthDate is null or u.BirthDate = @BirthDate")]
        public DateTime? BirthDate { get; set; }

        [Where("@IsEmailConfirmed is null or u.IsEmailConfirmed = @IsEmailConfirmed")]
        public bool? IsEmailConfirmed { get; set; }

        [Where("@IsPhoneConfirmed is null or u.IsPhoneConfirmed = @IsPhoneConfirmed")]
        public bool? IsPhoneConfirmed { get; set; }

        [Where("@IsLocked is null or u.IsLocked = @IsLocked")]
        public bool? IsLocked { get; set; }

        [Where("@IsDeleted is null or u.IsDeleted = @IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Where("@CreateDate is null or convert(date, u.CreateDate) = convert(date, @CreateDate)")]
        public DateTime? CreateDate { get; set; }

        [Where("@ModifyDate is null or convert(date, u.ModifyDate) = convert(date, @ModifyDate)")]
        public DateTime? ModifyDate { get; set; }

        [SortingColumn]
        public string SortingColumn { get; set; }

        [SortingOrder]
        public string SortingOrder { get; set; }

        [Page]
        public int? Page { get; set; }

        [Size]
        public int? Size { get; set; }
    }
}