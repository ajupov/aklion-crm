﻿select
	count(0)
	from dbo.Category as c
		inner join dbo.Store as s on
			c.StoreId = s.Id
		left outer join dbo.Category as pc on
			c.ParentId = pc.Id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or c.Id = @Id)
			and (coalesce(@StoreId, 0) = 0 or c.StoreId = @StoreId)
			and (coalesce(@StoreName, '') = '' or s.[Name] like @StoreName + '%')
			and (coalesce(@Name, '') = '' or c.[Name] like @Name + '%')
			and (@IsDeleted is null or c.IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, c.CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, c.ModifyDate) = convert(date, @ModifyDate)));

select
	c.Id,
	c.StoreId,
	s.[Name]	as StoreName,
    c.[Name],
	c.ParentId,
	pc.[Name]	as ParentName,
    c.IsDeleted,
    c.CreateDate,
    c.ModifyDate
	from dbo.Category as c
		inner join dbo.Store as s on
			c.StoreId = s.Id
		left outer join dbo.Category as pc on
			c.ParentId = pc.Id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or c.Id = @Id)
			and (coalesce(@StoreId, 0) = 0 or c.StoreId = @StoreId)
			and (coalesce(@StoreName, '') = '' or s.[Name] like @StoreName + '%')
			and (coalesce(@Name, '') = '' or c.[Name] like @Name + '%')
			and (coalesce(@ParentId, 0) = 0 or c.ParentId = @ParentId)
			and (coalesce(@ParentName, '') = '' or pc.[Name] like @ParentName + '%')
			and (@IsDeleted is null or c.IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, c.CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, c.ModifyDate) = convert(date, @ModifyDate)))
	order by
		case when @SortingColumn = 'Id' and @SortingOrder = 'asc' then c.Id end,
		case when @SortingColumn = 'Id' and @SortingOrder = 'desc' then c.Id end desc,
		case when @SortingColumn = 'StoreId' and @SortingOrder = 'asc' then c.StoreId end,
		case when @SortingColumn = 'StoreId' and @SortingOrder = 'desc' then c.StoreId end desc,
		case when @SortingColumn = 'StoreName' and @SortingOrder = 'asc' then s.[Name] end,
		case when @SortingColumn = 'StoreName' and @SortingOrder = 'desc' then s.[Name] end desc,
		case when @SortingColumn = 'Name' and @SortingOrder = 'asc' then c.[Name] end,
		case when @SortingColumn = 'Name' and @SortingOrder = 'desc' then c.[Name] end desc,
		case when @SortingColumn = 'ParentId' and @SortingOrder = 'asc' then c.ParentId end,
		case when @SortingColumn = 'ParentId' and @SortingOrder = 'desc' then c.ParentId end desc,
		case when @SortingColumn = 'ParentName' and @SortingOrder = 'asc' then pc.[Name] end,
		case when @SortingColumn = 'ParentName' and @SortingOrder = 'desc' then pc.[Name] end desc,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'asc' then c.IsDeleted end,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'desc' then c.IsDeleted end desc,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'asc' then c.CreateDate end,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'desc' then c.CreateDate end desc,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'asc' then c.ModifyDate end,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'desc' then c.ModifyDate end desc
	offset iif(@Page * @Size > 0, @Page * @Size, 0) rows
 	fetch next iif(@Size > 0, @Size, convert(int, 0x7fffffff)) rows only;