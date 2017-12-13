select
	count(0)
	from dbo.ProductAttributeLink as pal
		inner join dbo.Store as s on
			pal.StoreId = s.Id
		inner join dbo.Product as p on
			pal.ProductId = p.Id
		inner join dbo.ProductAttribute as pa on
			pal.AttributeId = a.Id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or pal.Id = @Id)
			and (coalesce(@StoreId, 0) = 0 or pal.StoreId = @StoreId)
			and (coalesce(@StoreName, '') = '' or s.[Name] like @StoreName + '%')
			and (coalesce(@ProductId, 0) = 0 or pal.ProductId = @ProductId)
			and (coalesce(@ProductName, '') = '' or p.[Name] like @ProductName + '%')
			and (coalesce(@AttributeId, 0) = 0 or pal.AttributeId = @AttributeId)
			and (coalesce(@AttributeName, '') = '' or pa.[Name] like @AttributeName + '%')
			and (coalesce(@Value, '') = '' or pal.[Value] like @Value + '%')
			and (@IsDeleted is null or pal.IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, pal.CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, pal.ModifyDate) = convert(date, @ModifyDate)));

select	
	pal.Id,
    pal.StoreId,
	s.[Name]	as StoreName,
    pal.ProductId,
	p.[Name]	as ProductName,
    pal.AttributeId,
	pa.[Name]	as AttributeName,
	pal.[Value],
	pal.IsDeleted,
    pal.CreateDate,
    pal.ModifyDate
	from dbo.ProductAttributeLink as pal
		inner join dbo.Store as s on
			pal.StoreId = s.Id
		inner join dbo.Product as p on
			pal.ProductId = p.Id
		inner join dbo.ProductAttribute as pa on
			pal.AttributeId = a.Id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or pal.Id = @Id)
			and (coalesce(@StoreId, 0) = 0 or pal.StoreId = @StoreId)
			and (coalesce(@StoreName, '') = '' or s.[Name] like @StoreName + '%')
			and (coalesce(@ProductId, 0) = 0 or pal.ProductId = @ProductId)
			and (coalesce(@ProductName, '') = '' or p.[Name] like @ProductName + '%')
			and (coalesce(@AttributeId, 0) = 0 or pal.AttributeId = @AttributeId)
			and (coalesce(@AttributeName, '') = '' or pa.[Name] like @AttributeName + '%')
			and (coalesce(@Value, '') = '' or pal.[Value] like @Value + '%')
			and (@IsDeleted is null or pal.IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, pal.CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, pal.ModifyDate) = convert(date, @ModifyDate)))
	order by
		case when @SortingColumn = 'Id' and @SortingOrder = 'asc' then pal.Id end,
		case when @SortingColumn = 'Id' and @SortingOrder = 'desc' then pal.Id end desc,
		case when @SortingColumn = 'StoreId' and @SortingOrder = 'asc' then pal.StoreId end,
		case when @SortingColumn = 'StoreId' and @SortingOrder = 'desc' then pal.StoreId end desc,
		case when @SortingColumn = 'StoreName' and @SortingOrder = 'asc' then s.[Name] end,
		case when @SortingColumn = 'StoreName' and @SortingOrder = 'desc' then s.[Name] end desc,
		case when @SortingColumn = 'ProductId' and @SortingOrder = 'asc' then pal.ProductId end,
		case when @SortingColumn = 'ProductId' and @SortingOrder = 'desc' then pal.ProductId end desc,
		case when @SortingColumn = 'ProductName' and @SortingOrder = 'asc' then pa.[Name] end,
		case when @SortingColumn = 'ProductName' and @SortingOrder = 'desc' then pa.[Name] end desc,
		case when @SortingColumn = 'AttributeId' and @SortingOrder = 'asc' then pal.AttributeId end,
		case when @SortingColumn = 'AttributeId' and @SortingOrder = 'desc' then pal.AttributeId end desc,
		case when @SortingColumn = 'AttributeName' and @SortingOrder = 'asc' then a.[Name] end,
		case when @SortingColumn = 'AttributeName' and @SortingOrder = 'desc' then a.[Name] end desc,
		case when @SortingColumn = 'Value' and @SortingOrder = 'asc' then pal.[Value] end,
		case when @SortingColumn = 'Value' and @SortingOrder = 'desc' then pal.[Value] end desc,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'asc' then pal.IsDeleted end,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'desc' then pal.IsDeleted end desc,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'asc' then pal.CreateDate end,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'desc' then pal.CreateDate end desc,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'asc' then pal.ModifyDate end,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'desc' then pal.ModifyDate end desc
	offset iif(@Page * @Size > 0, @Page * @Size, 0) rows
 	fetch next iif(@Size > 0, @Size, convert(int, 0x7fffffff)) rows only;