select top 1
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
	from dbo.[User]
	where [Login] = @login;