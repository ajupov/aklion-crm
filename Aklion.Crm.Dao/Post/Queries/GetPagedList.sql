select
	count(0)
	from dbo.Post as p
		inner join dbo.Store as s on
			p.StoreId = s.Id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or p.Id = @Id)
			and (coalesce(@StoreId, 0) = 0 or p.StoreId = @StoreId)
			and (coalesce(@StoreName, '') = '' or s.[Name] like @StoreName + '%')
			and (coalesce(@Name, '') = '' or p.[Name] like @Name + '%')
			and (@IsDeleted is null or s.IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, s.CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, s.ModifyDate) = convert(date, @ModifyDate)));

select
	p.Id,
	p.StoreId,
	s.[Name]	as StoreName,
    p.[Name],
    p.IsDeleted,
    p.CreateDate,
    p.ModifyDate
	from dbo.Post as p
		inner join dbo.Store as s on
			p.StoreId = s.Id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or p.Id = @Id)
			and (coalesce(@StoreId, 0) = 0 or p.StoreId = @StoreId)
			and (coalesce(@StoreName, '') = '' or s.[Name] like @StoreName + '%')
			and (coalesce(@Name, '') = '' or p.[Name] like @Name + '%')
			and (@IsDeleted is null or s.IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, s.CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, s.ModifyDate) = convert(date, @ModifyDate)))
	order by
		case when @SortingColumn = 'Id' and @SortingOrder = 'asc' then p.Id end,
		case when @SortingColumn = 'Id' and @SortingOrder = 'desc' then p.Id end desc,
		case when @SortingColumn = 'StoreId' and @SortingOrder = 'asc' then p.StoreId end,
		case when @SortingColumn = 'StoreId' and @SortingOrder = 'desc' then p.StoreId end desc,
		case when @SortingColumn = 'StoreName' and @SortingOrder = 'asc' then s.[Name] end,
		case when @SortingColumn = 'StoreName' and @SortingOrder = 'desc' then s.[Name] end desc,
		case when @SortingColumn = 'Name' and @SortingOrder = 'asc' then p.[Name] end,
		case when @SortingColumn = 'Name' and @SortingOrder = 'desc' then p.[Name] end desc,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'asc' then p.IsDeleted end,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'desc' then p.IsDeleted end desc,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'asc' then p.CreateDate end,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'desc' then p.CreateDate end desc,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'asc' then p.ModifyDate end,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'desc' then p.ModifyDate end desc
	offset iif(@Page * @Size > 0, @Page * @Size, 0) rows
 	fetch next iif(@Size > 0, @Size, convert(int, 0x7fffffff)) rows only;