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
	from dbo.[User]
	order by
		case when @SortingColumn = 'Id' and @SortingOrder = 'asc' then Id end,
		case when @SortingColumn = 'Id' and @SortingOrder = 'desc' then Id end desc,
		case when @SortingColumn = 'Login' and @SortingOrder = 'asc' then [Login] end,
		case when @SortingColumn = 'Login' and @SortingOrder = 'desc' then [Login] end desc,
		case when @SortingColumn = 'Email' and @SortingOrder = 'asc' then Email end,
		case when @SortingColumn = 'Email' and @SortingOrder = 'desc' then Email end desc,
		case when @SortingColumn = 'Phone' and @SortingOrder = 'asc' then Phone end,
		case when @SortingColumn = 'Phone' and @SortingOrder = 'desc' then Phone end desc,
		case when @SortingColumn = 'Surname' and @SortingOrder = 'asc' then Surname end,
		case when @SortingColumn = 'Surname' and @SortingOrder = 'desc' then Surname end desc,
		case when @SortingColumn = 'Name' and @SortingOrder = 'asc' then [Name] end,
		case when @SortingColumn = 'Name' and @SortingOrder = 'desc' then [Name] end desc,
		case when @SortingColumn = 'Patronymic' and @SortingOrder = 'asc' then Patronymic end,
		case when @SortingColumn = 'Patronymic' and @SortingOrder = 'desc' then Patronymic end desc,
		case when @SortingColumn = 'Gender' and @SortingOrder = 'asc' then Gender end,
		case when @SortingColumn = 'Gender' and @SortingOrder = 'desc' then Gender end desc,
		case when @SortingColumn = 'BirthDate' and @SortingOrder = 'asc' then BirthDate end,
		case when @SortingColumn = 'BirthDate' and @SortingOrder = 'desc' then BirthDate end desc,
		case when @SortingColumn = 'IsEmailConfirmed' and @SortingOrder = 'asc' then IsEmailConfirmed end,
		case when @SortingColumn = 'IsEmailConfirmed' and @SortingOrder = 'desc' then IsEmailConfirmed end desc,
		case when @SortingColumn = 'IsPhoneConfirmed' and @SortingOrder = 'asc' then IsPhoneConfirmed end,
		case when @SortingColumn = 'IsPhoneConfirmed' and @SortingOrder = 'desc' then IsPhoneConfirmed end desc,
		case when @SortingColumn = 'IsLocked' and @SortingOrder = 'asc' then IsLocked end,
		case when @SortingColumn = 'IsLocked' and @SortingOrder = 'desc' then IsLocked end desc,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'asc' then IsDeleted end,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'desc' then IsDeleted end desc,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'asc' then CreateDate end,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'desc' then CreateDate end desc,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'asc' then ModifyDate end,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'desc' then ModifyDate end desc
	offset iif(@Page * @Size > 0, @Page * @Size, 0) rows
 	fetch next iif(@Size > 0, @Size, convert(int, 0x7fffffff)) rows only;