select
	count(0)
	from dbo.ProductCategory as pc
		inner join dbo.Store as s on
			pc.StoreId = s.Id
		inner join dbo.Product as p on
			pc.ProductId = p.Id
		inner join dbo.Category as c on
			pc.CategoryId = c.Id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or pc.Id = @Id)
			and (coalesce(@StoreId, 0) = 0 or pc.StoreId = @StoreId)
			and (coalesce(@StoreName, '') = '' or s.[Name] like @StoreName + '%')
			and (coalesce(@ProductId, 0) = 0 or pc.ProductId = @ProductId)
			and (coalesce(@ProductName, '') = '' or p.[Name] like @ProductName + '%')
			and (coalesce(@CategoryId, 0) = 0 or pc.CategoryId = @CategoryId)
			and (coalesce(@CategoryName, '') = '' or c.[Name] like @CategoryName + '%')
			and (@IsDeleted is null or pc.IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, pc.CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, pc.ModifyDate) = convert(date, @ModifyDate)));

select
	pc.Id,
    pc.StoreId,
	s.[Name]	as StoreName,
    pc.ProductId,
	p.[Name]	as ProductName,
    pc.CategoryId,
	c.[Name]	as CategoryName,
	pc.IsDeleted,
    pc.CreateDate,
    pc.ModifyDate
	from dbo.ProductCategory as pc
		inner join dbo.Store as s on
			pc.StoreId = s.Id
		inner join dbo.Product as p on
			pc.ProductId = p.Id
		inner join dbo.Category as c on
			pc.CategoryId = c.Id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or pc.Id = @Id)
			and (coalesce(@StoreId, 0) = 0 or pc.StoreId = @StoreId)
			and (coalesce(@StoreName, '') = '' or s.[Name] like @StoreName + '%')
			and (coalesce(@ProductId, 0) = 0 or pc.ProductId = @ProductId)
			and (coalesce(@ProductName, '') = '' or p.[Name] like @ProductName + '%')
			and (coalesce(@CategoryId, 0) = 0 or pc.CategoryId = @CategoryId)
			and (coalesce(@CategoryName, '') = '' or c.[Name] like @CategoryName + '%')
			and (@IsDeleted is null or pc.IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, pc.CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, pc.ModifyDate) = convert(date, @ModifyDate)))
	order by
		case when @SortingColumn = 'Id' and @SortingOrder = 'asc' then pc.Id end,
		case when @SortingColumn = 'Id' and @SortingOrder = 'desc' then pc.Id end desc,
		case when @SortingColumn = 'StoreId' and @SortingOrder = 'asc' then pc.StoreId end,
		case when @SortingColumn = 'StoreId' and @SortingOrder = 'desc' then pc.StoreId end desc,
		case when @SortingColumn = 'StoreName' and @SortingOrder = 'asc' then s.[Name] end,
		case when @SortingColumn = 'StoreName' and @SortingOrder = 'desc' then s.[Name] end desc,
		case when @SortingColumn = 'ProductId' and @SortingOrder = 'asc' then pc.ProductId end,
		case when @SortingColumn = 'ProductId' and @SortingOrder = 'desc' then pc.ProductId end desc,
		case when @SortingColumn = 'ProductName' and @SortingOrder = 'asc' then p.[Name] end,
		case when @SortingColumn = 'ProductName' and @SortingOrder = 'desc' then p.[Name] end desc,
		case when @SortingColumn = 'CategoryId' and @SortingOrder = 'asc' then pc.CategoryId end,
		case when @SortingColumn = 'CategoryId' and @SortingOrder = 'desc' then pc.CategoryId end desc,
		case when @SortingColumn = 'CategoryName' and @SortingOrder = 'asc' then c.[Name] end,
		case when @SortingColumn = 'CategoryName' and @SortingOrder = 'desc' then c.[Name] end desc,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'asc' then pc.IsDeleted end,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'desc' then pc.IsDeleted end desc,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'asc' then pc.CreateDate end,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'desc' then pc.CreateDate end desc,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'asc' then pc.ModifyDate end,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'desc' then pc.ModifyDate end desc
	offset iif(@Page * @Size > 0, @Page * @Size, 0) rows
 	fetch next iif(@Size > 0, @Size, convert(int, 0x7fffffff)) rows only;