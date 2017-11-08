select
	count(0)
	from dbo.Store as s
		inner join dbo.[User] as u on
			s.CreateUserId = u.Id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or s.Id = @Id)
			and (coalesce(@CreateUserId, 0) = 0 or s.CreateUserId = @CreateUserId)
			and (coalesce(@Name, '') = '' or s.[Name] like @Name + '%')
			and (coalesce(@CreateUserLogin, '') = '' or u.[Login] like @CreateUserLogin + '%')
			and (coalesce(@ApiSecret, '') = '' or s.ApiSecret like @ApiSecret + '%')
			and (@IsLocked is null or s.IsLocked = @IsLocked)
			and (@IsDeleted is null or s.IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, s.CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, s.ModifyDate) = convert(date, @ModifyDate)));

select
	s.Id,
	s.CreateUserId,
	u.[Login]		as CreateUserLogin,
    s.[Name],
    s.ApiSecret,
    s.IsLocked,
    s.IsDeleted,
    s.CreateDate,
    s.ModifyDate
	from dbo.Store as s
		inner join dbo.[User] as u on
			s.CreateUserId = u.Id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or s.Id = @Id)
			and (coalesce(@CreateUserId, 0) = 0 or s.CreateUserId = @CreateUserId)
			and (coalesce(@Name, '') = '' or s.[Name] like @Name + '%')
			and (coalesce(@CreateUserLogin, '') = '' or u.[Login] like @CreateUserLogin + '%')
			and (coalesce(@ApiSecret, '') = '' or s.ApiSecret like @ApiSecret + '%')
			and (@IsLocked is null or s.IsLocked = @IsLocked)
			and (@IsDeleted is null or s.IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, s.CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, s.ModifyDate) = convert(date, @ModifyDate)))
	order by
		case when @SortingColumn = 'Id' and @SortingOrder = 'asc' then s.Id end,
		case when @SortingColumn = 'Id' and @SortingOrder = 'desc' then s.Id end desc,
		case when @SortingColumn = 'CreateUserId' and @SortingOrder = 'asc' then s.CreateUserId end,
		case when @SortingColumn = 'CreateUserId' and @SortingOrder = 'desc' then s.CreateUserId end desc,
		case when @SortingColumn = 'CreateUserLogin' and @SortingOrder = 'asc' then u.[Login] end,
		case when @SortingColumn = 'CreateUserLogin' and @SortingOrder = 'desc' then u.[Login] end desc,
		case when @SortingColumn = 'Name' and @SortingOrder = 'asc' then s.[Name] end,
		case when @SortingColumn = 'Name' and @SortingOrder = 'desc' then s.[Name] end desc,
		case when @SortingColumn = 'ApiSecret' and @SortingOrder = 'asc' then s.ApiSecret end,
		case when @SortingColumn = 'ApiSecret' and @SortingOrder = 'desc' then s.ApiSecret end desc,
		case when @SortingColumn = 'IsLocked' and @SortingOrder = 'asc' then s.IsLocked end,
		case when @SortingColumn = 'IsLocked' and @SortingOrder = 'desc' then s.IsLocked end desc,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'asc' then s.IsDeleted end,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'desc' then s.IsDeleted end desc,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'asc' then s.CreateDate end,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'desc' then s.CreateDate end desc,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'asc' then s.ModifyDate end,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'desc' then s.ModifyDate end desc
	offset iif(@Page * @Size > 0, @Page * @Size, 0) rows
 	fetch next iif(@Size > 0, @Size, convert(int, 0x7fffffff)) rows only;