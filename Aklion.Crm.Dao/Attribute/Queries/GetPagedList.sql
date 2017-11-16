select
	count(0)
	from dbo.Attribute as a
		inner join dbo.Store as s on
			a.StoreId = s.Id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or a.Id = @Id)
			and (coalesce(@StoreId, 0) = 0 or a.StoreId = @StoreId)
			and (coalesce(@StoreName, '') = '' or s.[Name] like @StoreName + '%')
			and (coalesce(@Name, '') = '' or a.[Name] like @Name + '%')
			and (@IsDeleted is null or a.IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, a.CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, a.ModifyDate) = convert(date, @ModifyDate)));

select
	a.Id,
	a.StoreId,
	s.[Name]	as StoreName,
    a.[Name],
    a.IsDeleted,
    a.CreateDate,
    a.ModifyDate
	from dbo.Attribute as a
		inner join dbo.Store as s on
			a.StoreId = s.Id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or a.Id = @Id)
			and (coalesce(@StoreId, 0) = 0 or a.StoreId = @StoreId)
			and (coalesce(@StoreName, '') = '' or s.[Name] like @StoreName + '%')
			and (coalesce(@Name, '') = '' or a.[Name] like @Name + '%')
			and (@IsDeleted is null or a.IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, a.CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, a.ModifyDate) = convert(date, @ModifyDate)))
	order by
		case when @SortingColumn = 'Id' and @SortingOrder = 'asc' then a.Id end,
		case when @SortingColumn = 'Id' and @SortingOrder = 'desc' then a.Id end desc,
		case when @SortingColumn = 'StoreId' and @SortingOrder = 'asc' then a.StoreId end,
		case when @SortingColumn = 'StoreId' and @SortingOrder = 'desc' then a.StoreId end desc,
		case when @SortingColumn = 'StoreName' and @SortingOrder = 'asc' then s.[Name] end,
		case when @SortingColumn = 'StoreName' and @SortingOrder = 'desc' then s.[Name] end desc,
		case when @SortingColumn = 'Name' and @SortingOrder = 'asc' then a.[Name] end,
		case when @SortingColumn = 'Name' and @SortingOrder = 'desc' then a.[Name] end desc,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'asc' then a.IsDeleted end,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'desc' then a.IsDeleted end desc,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'asc' then a.CreateDate end,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'desc' then a.CreateDate end desc,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'asc' then a.ModifyDate end,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'desc' then a.ModifyDate end desc
	offset iif(@Page * @Size > 0, @Page * @Size, 0) rows
 	fetch next iif(@Size > 0, @Size, convert(int, 0x7fffffff)) rows only;