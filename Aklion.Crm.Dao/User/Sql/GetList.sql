select
	count(0)
	from dbo.[User];

select
	Id,
    [Login],
    PasswordHash,
    Email,
    Phone,
    Surname,
    [Name],
    Patronymic,
    Gender,
    BirthDate,
    IsEmailConfirmed,
    IsPhoneConfirmed,
    IsLocked,
    IsDeleted,
    AvatarUrl,
    CreateDate,
    ModifyDate
	from dbo.[User];