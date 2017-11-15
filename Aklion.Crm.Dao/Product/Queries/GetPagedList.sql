select
	count(0)
	from dbo.Product as p
		inner join dbo.Store as s on
			p.StoreId = s.Id
		left outer join dbo.Product as pp on
			p.ParentId = pp.id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or p.Id = @Id)
			and (coalesce(@StoreId, 0) = 0 or p.StoreId = @StoreId)
			and (coalesce(@StoreName, '') = '' or s.[Name] like @StoreName + '%')
			and (coalesce(@Type, 0) = 0 or p.[Type] = @Type)
			and (coalesce(@Name, '') = '' or p.[Name] like @Name + '%')
			and (coalesce(@Description, '') = '' or p.[Description] like @Description + '%')
			and (coalesce(@Price, 0) = 0 or p.Price = @Price)
			and (coalesce(@Status, 0) = 0 or p.[Status] = @Status)
			and (coalesce(@VendorCode, '') = '' or p.VendorCode like @VendorCode + '%')
			and (coalesce(@ParentId, 0) = 0 or p.ParentId = @ParentId)
			and (coalesce(@ParentName, '') = '' or pp.[Name] like @ParentName + '%')
			and (@IsDeleted is null or p.IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, p.CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, p.ModifyDate) = convert(date, @ModifyDate)));

select
	p.Id,
    p.StoreId,
	s.[Name]	as StoreName,
    p.[Type],
    p.[Name],
    p.[Description],
    p.Price,
    p.[Status],
    p.VendorCode,
    p.ParentId,
	pp.[Name]	as ParentName,
    p.IsDeleted,
    p.CreateDate,
    p.ModifyDate
	from dbo.Product as p
		inner join dbo.Store as s on
			p.StoreId = s.Id
		left outer join dbo.Product as pp on
			p.ParentId = pp.id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or p.Id = @Id)
			and (coalesce(@StoreId, 0) = 0 or p.StoreId = @StoreId)
			and (coalesce(@StoreName, '') = '' or s.[Name] like @StoreName + '%')
			and (coalesce(@Type, 0) = 0 or p.[Type] = @Type)
			and (coalesce(@Name, '') = '' or p.[Name] like @Name + '%')
			and (coalesce(@Description, '') = '' or p.[Description] like @Description + '%')
			and (coalesce(@Price, 0) = 0 or p.Price = @Price)
			and (coalesce(@Status, 0) = 0 or p.[Status] = @Status)
			and (coalesce(@VendorCode, '') = '' or p.VendorCode like @VendorCode + '%')
			and (coalesce(@ParentId, 0) = 0 or p.ParentId = @ParentId)
			and (coalesce(@ParentName, '') = '' or pp.[Name] like @ParentName + '%')
			and (@IsDeleted is null or p.IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, p.CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, p.ModifyDate) = convert(date, @ModifyDate)))
	order by
		case when @SortingColumn = 'Id' and @SortingOrder = 'asc' then p.Id end,
		case when @SortingColumn = 'Id' and @SortingOrder = 'desc' then p.Id end desc,
		case when @SortingColumn = 'StoreId' and @SortingOrder = 'asc' then p.StoreId end,
		case when @SortingColumn = 'StoreId' and @SortingOrder = 'desc' then p.StoreId end desc,
		case when @SortingColumn = 'StoreName' and @SortingOrder = 'asc' then s.[Name] end,
		case when @SortingColumn = 'StoreName' and @SortingOrder = 'desc' then s.[Name] end desc,
		case when @SortingColumn = 'Type' and @SortingOrder = 'asc' then p.[Type] end,
		case when @SortingColumn = 'Type' and @SortingOrder = 'desc' then p.[Type] end desc,
		case when @SortingColumn = 'Name' and @SortingOrder = 'asc' then p.[Name] end,
		case when @SortingColumn = 'Name' and @SortingOrder = 'desc' then p.[Name] end desc,
		case when @SortingColumn = 'Description' and @SortingOrder = 'asc' then p.[Description] end,
		case when @SortingColumn = 'Description' and @SortingOrder = 'desc' then p.[Description] end desc,
		case when @SortingColumn = 'Price' and @SortingOrder = 'asc' then p.Price end,
		case when @SortingColumn = 'Price' and @SortingOrder = 'desc' then p.Price end desc,
		case when @SortingColumn = 'Status' and @SortingOrder = 'asc' then p.[Status] end,
		case when @SortingColumn = 'Status' and @SortingOrder = 'desc' then p.[Status] end desc,
		case when @SortingColumn = 'VendorCode' and @SortingOrder = 'asc' then p.VendorCode end,
		case when @SortingColumn = 'VendorCode' and @SortingOrder = 'desc' then p.VendorCode end desc,
		case when @SortingColumn = 'ParentId' and @SortingOrder = 'asc' then p.ParentId end,
		case when @SortingColumn = 'ParentId' and @SortingOrder = 'desc' then p.ParentId end desc,
		case when @SortingColumn = 'ParentName' and @SortingOrder = 'asc' then pp.[Name] end,
		case when @SortingColumn = 'ParentName' and @SortingOrder = 'desc' then pp.[Name] end desc,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'asc' then p.IsDeleted end,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'desc' then p.IsDeleted end desc,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'asc' then p.CreateDate end,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'desc' then p.CreateDate end desc,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'asc' then p.ModifyDate end,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'desc' then p.ModifyDate end desc
	offset iif(@Page * @Size > 0, @Page * @Size, 0) rows
 	fetch next iif(@Size > 0, @Size, convert(int, 0x7fffffff)) rows only;