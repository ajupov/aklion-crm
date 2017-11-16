select
	count(0)
	from dbo.ProductTag as pt
		inner join dbo.Store as s on
			pt.StoreId = s.Id
		inner join dbo.Product as p on
			pt.ProductId = p.Id
		inner join dbo.Tag as t on
			pt.TagId = t.Id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or pt.Id = @Id)
			and (coalesce(@StoreId, 0) = 0 or pt.StoreId = @StoreId)
			and (coalesce(@StoreName, '') = '' or s.[Name] like @StoreName + '%')
			and (coalesce(@ProductId, 0) = 0 or pt.ProductId = @ProductId)
			and (coalesce(@ProductName, '') = '' or p.[Name] like @ProductName + '%')
			and (coalesce(@TagId, 0) = 0 or pt.TagId = @TagId)
			and (coalesce(@TagName, '') = '' or t.[Name] like @TagName + '%')
			and (@IsDeleted is null or pt.IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, pt.CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, pt.ModifyDate) = convert(date, @ModifyDate)));

select
	pt.Id,
    pt.StoreId,
	s.[Name]	as StoreName,
    pt.ProductId,
	p.[Name]	as ProductName,
    pt.TagId,
	t.[Name]	as TagName,
	pt.IsDeleted,
    pt.CreateDate,
    pt.ModifyDate
	from dbo.ProductTag as pt
		inner join dbo.Store as s on
			pt.StoreId = s.Id
		inner join dbo.Product as p on
			pt.ProductId = p.Id
		inner join dbo.Tag as t on
			pt.TagId = t.Id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or pt.Id = @Id)
			and (coalesce(@StoreId, 0) = 0 or pt.StoreId = @StoreId)
			and (coalesce(@StoreName, '') = '' or s.[Name] like @StoreName + '%')
			and (coalesce(@ProductId, 0) = 0 or pt.ProductId = @ProductId)
			and (coalesce(@ProductName, '') = '' or p.[Name] like @ProductName + '%')
			and (coalesce(@TagId, 0) = 0 or pt.TagId = @TagId)
			and (coalesce(@TagName, '') = '' or t.[Name] like @TagName + '%')
			and (@IsDeleted is null or pt.IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, pt.CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, pt.ModifyDate) = convert(date, @ModifyDate)))
	order by
		case when @SortingColumn = 'Id' and @SortingOrder = 'asc' then pt.Id end,
		case when @SortingColumn = 'Id' and @SortingOrder = 'desc' then pt.Id end desc,
		case when @SortingColumn = 'StoreId' and @SortingOrder = 'asc' then pt.StoreId end,
		case when @SortingColumn = 'StoreId' and @SortingOrder = 'desc' then pt.StoreId end desc,
		case when @SortingColumn = 'StoreName' and @SortingOrder = 'asc' then s.[Name] end,
		case when @SortingColumn = 'StoreName' and @SortingOrder = 'desc' then s.[Name] end desc,
		case when @SortingColumn = 'ProductId' and @SortingOrder = 'asc' then pt.ProductId end,
		case when @SortingColumn = 'ProductId' and @SortingOrder = 'desc' then pt.ProductId end desc,
		case when @SortingColumn = 'ProductName' and @SortingOrder = 'asc' then p.[Name] end,
		case when @SortingColumn = 'ProductName' and @SortingOrder = 'desc' then p.[Name] end desc,
		case when @SortingColumn = 'TagId' and @SortingOrder = 'asc' then pt.TagId end,
		case when @SortingColumn = 'TagId' and @SortingOrder = 'desc' then pt.TagId end desc,
		case when @SortingColumn = 'TagName' and @SortingOrder = 'asc' then t.[Name] end,
		case when @SortingColumn = 'TagName' and @SortingOrder = 'desc' then t.[Name] end desc,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'asc' then pt.IsDeleted end,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'desc' then pt.IsDeleted end desc,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'asc' then pt.CreateDate end,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'desc' then pt.CreateDate end desc,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'asc' then pt.ModifyDate end,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'desc' then pt.ModifyDate end desc
	offset iif(@Page * @Size > 0, @Page * @Size, 0) rows
 	fetch next iif(@Size > 0, @Size, convert(int, 0x7fffffff)) rows only;