select
	count(0)
	from dbo.ProductAttribute as pa
		inner join dbo.Store as s on
			pa.StoreId = s.Id
		inner join dbo.Product as p on
			pa.ProductId = p.Id
		inner join dbo.Attribute as a on
			pa.AttributeId = a.Id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or pa.Id = @Id)
			and (coalesce(@StoreId, 0) = 0 or pa.StoreId = @StoreId)
			and (coalesce(@StoreName, '') = '' or s.[Name] like @StoreName + '%')
			and (coalesce(@ProductId, 0) = 0 or pa.ProductId = @ProductId)
			and (coalesce(@ProductName, '') = '' or p.[Name] like @ProductName + '%')
			and (coalesce(@AttributeId, 0) = 0 or pa.AttributeId = @AttributeId)
			and (coalesce(@AttributeName, '') = '' or a.[Name] like @AttributeName + '%')
			and (coalesce(@Value, '') = '' or pa.[Value] like @Value + '%')
			and (@IsDeleted is null or pa.IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, pa.CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, pa.ModifyDate) = convert(date, @ModifyDate)));

select	
	pa.Id,
    pa.StoreId,
	s.[Name]	as StoreName,
    pa.ProductId,
	p.[Name]	as ProductName,
    pa.AttributeId,
	a.[Name]	as AttributeName,
	pa.[Value],
	pa.IsDeleted,
    pa.CreateDate,
    pa.ModifyDate
	from dbo.ProductAttribute as pa
		inner join dbo.Store as s on
			pa.StoreId = s.Id
		inner join dbo.Product as p on
			pa.ProductId = p.Id
		inner join dbo.Attribute as a on
			pa.AttributeId = a.Id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or pa.Id = @Id)
			and (coalesce(@StoreId, 0) = 0 or pa.StoreId = @StoreId)
			and (coalesce(@StoreName, '') = '' or s.[Name] like @StoreName + '%')
			and (coalesce(@ProductId, 0) = 0 or pa.ProductId = @ProductId)
			and (coalesce(@ProductName, '') = '' or p.[Name] like @ProductName + '%')
			and (coalesce(@AttributeId, 0) = 0 or pa.AttributeId = @AttributeId)
			and (coalesce(@AttributeName, '') = '' or a.[Name] like @AttributeName + '%')
			and (coalesce(@Value, '') = '' or pa.[Value] like @Value + '%')
			and (@IsDeleted is null or pa.IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, pa.CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, pa.ModifyDate) = convert(date, @ModifyDate)))
	order by
		case when @SortingColumn = 'Id' and @SortingOrder = 'asc' then pa.Id end,
		case when @SortingColumn = 'Id' and @SortingOrder = 'desc' then pa.Id end desc,
		case when @SortingColumn = 'StoreId' and @SortingOrder = 'asc' then pa.StoreId end,
		case when @SortingColumn = 'StoreId' and @SortingOrder = 'desc' then pa.StoreId end desc,
		case when @SortingColumn = 'StoreName' and @SortingOrder = 'asc' then s.[Name] end,
		case when @SortingColumn = 'StoreName' and @SortingOrder = 'desc' then s.[Name] end desc,
		case when @SortingColumn = 'ProductId' and @SortingOrder = 'asc' then pa.ProductId end,
		case when @SortingColumn = 'ProductId' and @SortingOrder = 'desc' then pa.ProductId end desc,
		case when @SortingColumn = 'ProductName' and @SortingOrder = 'asc' then p.[Name] end,
		case when @SortingColumn = 'ProductName' and @SortingOrder = 'desc' then p.[Name] end desc,
		case when @SortingColumn = 'AttributeId' and @SortingOrder = 'asc' then pa.AttributeId end,
		case when @SortingColumn = 'AttributeId' and @SortingOrder = 'desc' then pa.AttributeId end desc,
		case when @SortingColumn = 'AttributeName' and @SortingOrder = 'asc' then a.[Name] end,
		case when @SortingColumn = 'AttributeName' and @SortingOrder = 'desc' then a.[Name] end desc,
		case when @SortingColumn = 'Value' and @SortingOrder = 'asc' then pa.[Value] end,
		case when @SortingColumn = 'Value' and @SortingOrder = 'desc' then pa.[Value] end desc,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'asc' then pa.IsDeleted end,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'desc' then pa.IsDeleted end desc,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'asc' then pa.CreateDate end,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'desc' then pa.CreateDate end desc,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'asc' then pa.ModifyDate end,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'desc' then pa.ModifyDate end desc
	offset iif(@Page * @Size > 0, @Page * @Size, 0) rows
 	fetch next iif(@Size > 0, @Size, convert(int, 0x7fffffff)) rows only;