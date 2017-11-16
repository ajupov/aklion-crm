select
	count(0)
	from dbo.Tag as t
		inner join dbo.Store as s on
			t.StoreId = s.Id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or t.Id = @Id)
			and (coalesce(@StoreId, 0) = 0 or t.StoreId = @StoreId)
			and (coalesce(@StoreName, '') = '' or s.[Name] like @StoreName + '%')
			and (coalesce(@Name, '') = '' or t.[Name] like @Name + '%')
			and (@IsDeleted is null or t.IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, t.CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, t.ModifyDate) = convert(date, @ModifyDate)));

select
	t.Id,
	t.StoreId,
	s.[Name]	as StoreName,
    t.[Name],
    t.IsDeleted,
    t.CreateDate,
    t.ModifyDate
	from dbo.Tag as t
		inner join dbo.Store as s on
			t.StoreId = s.Id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or t.Id = @Id)
			and (coalesce(@StoreId, 0) = 0 or t.StoreId = @StoreId)
			and (coalesce(@StoreName, '') = '' or s.[Name] like @StoreName + '%')
			and (coalesce(@Name, '') = '' or t.[Name] like @Name + '%')
			and (@IsDeleted is null or t.IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, t.CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, t.ModifyDate) = convert(date, @ModifyDate)))
	order by
		case when @SortingColumn = 'Id' and @SortingOrder = 'asc' then t.Id end,
		case when @SortingColumn = 'Id' and @SortingOrder = 'desc' then t.Id end desc,
		case when @SortingColumn = 'StoreId' and @SortingOrder = 'asc' then t.StoreId end,
		case when @SortingColumn = 'StoreId' and @SortingOrder = 'desc' then t.StoreId end desc,
		case when @SortingColumn = 'StoreName' and @SortingOrder = 'asc' then s.[Name] end,
		case when @SortingColumn = 'StoreName' and @SortingOrder = 'desc' then s.[Name] end desc,
		case when @SortingColumn = 'Name' and @SortingOrder = 'asc' then t.[Name] end,
		case when @SortingColumn = 'Name' and @SortingOrder = 'desc' then t.[Name] end desc,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'asc' then t.IsDeleted end,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'desc' then t.IsDeleted end desc,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'asc' then t.CreateDate end,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'desc' then t.CreateDate end desc,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'asc' then t.ModifyDate end,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'desc' then t.ModifyDate end desc
	offset iif(@Page * @Size > 0, @Page * @Size, 0) rows
 	fetch next iif(@Size > 0, @Size, convert(int, 0x7fffffff)) rows only;