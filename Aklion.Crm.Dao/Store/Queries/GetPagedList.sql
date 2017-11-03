select
	count(0)
	from dbo.Store
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or Id = @Id)
			and (coalesce(@CreateUserId, 0) = 0 or CreateUserId = @CreateUserId)
			and (coalesce(@Name, '') = '' or [Name] like @Name + '%')
			and (coalesce(@ApiSecret, '') = '' or ApiSecret like @ApiSecret + '%')
			and (@IsLocked is null or IsLocked = @IsLocked)
			and (@IsDeleted is null or IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, ModifyDate) = convert(date, @ModifyDate)));

select
	Id,
	CreateUserId,
    [Name],
    ApiSecret,
    IsLocked,
    IsDeleted,
    CreateDate,
    ModifyDate
	from dbo.Store
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or Id = @Id)
			and (coalesce(@CreateUserId, 0) = 0 or CreateUserId = @CreateUserId)
			and (coalesce(@Name, '') = '' or [Name] like @Name + '%')
			and (coalesce(@ApiSecret, '') = '' or ApiSecret like @ApiSecret + '%')
			and (@IsLocked is null or IsLocked = @IsLocked)
			and (@IsDeleted is null or IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, ModifyDate) = convert(date, @ModifyDate)))
	order by
		case when @SortingColumn = 'Id' and @SortingOrder = 'asc' then Id end,
		case when @SortingColumn = 'Id' and @SortingOrder = 'desc' then Id end desc,
		case when @SortingColumn = 'CreateUserId' and @SortingOrder = 'asc' then CreateUserId end,
		case when @SortingColumn = 'CreateUserId' and @SortingOrder = 'desc' then CreateUserId end desc,
		case when @SortingColumn = 'Name' and @SortingOrder = 'asc' then [Name] end,
		case when @SortingColumn = 'Name' and @SortingOrder = 'desc' then [Name] end desc,
		case when @SortingColumn = 'ApiSecret' and @SortingOrder = 'asc' then ApiSecret end,
		case when @SortingColumn = 'ApiSecret' and @SortingOrder = 'desc' then ApiSecret end desc,
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