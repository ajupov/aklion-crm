select
	count(0)
	from dbo.UserPost as up
		inner join dbo.[User] as u on
			up.UserId = u.Id
		inner join dbo.Store as s on
			up.StoreId = s.Id
		inner join dbo.Post as p on
			up.PostId = p.Id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or s.Id = @Id)
			and (coalesce(@UserId, 0) = 0 or up.UserId = @StoreId)
			and (coalesce(@UserLogin, '') = '' or u.[Login] like @UserLogin + '%')
			and (coalesce(@StoreId, 0) = 0 or up.StoreId = @StoreId)
			and (coalesce(@StoreName, '') = '' or s.[Name] like @StoreName + '%')		
			and (coalesce(@PostId, 0) = 0 or up.StoreId = @PostId)
			and (coalesce(@PostName, '') = '' or p.[Name] like @PostName + '%')
			and (@IsDeleted is null or up.IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, up.CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, up.ModifyDate) = convert(date, @ModifyDate)));

select
	up.Id,
	up.UserId,
	u.[Login]	as UserLogin,
	up.StoreId,
	s.[Name]	as StoreName,
	up.PostId,
    p.[Name]	as PostName,
    up.IsDeleted,
    p.CreateDate,
    p.ModifyDate
	from dbo.UserPost as up
		inner join dbo.[User] as u on
			up.UserId = u.Id
		inner join dbo.Store as s on
			up.StoreId = s.Id
		inner join dbo.Post as p on
			up.PostId = p.Id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or s.Id = @Id)
			and (coalesce(@UserId, 0) = 0 or up.UserId = @StoreId)
			and (coalesce(@UserLogin, '') = '' or u.[Login] like @UserLogin + '%')
			and (coalesce(@StoreId, 0) = 0 or up.StoreId = @StoreId)
			and (coalesce(@StoreName, '') = '' or s.[Name] like @StoreName + '%')		
			and (coalesce(@PostId, 0) = 0 or up.StoreId = @PostId)
			and (coalesce(@PostName, '') = '' or p.[Name] like @PostName + '%')
			and (@IsDeleted is null or up.IsDeleted = @IsDeleted)
			and (@CreateDate is null or convert(date, up.CreateDate) = convert(date, @CreateDate))
			and (@ModifyDate is null or convert(date, up.ModifyDate) = convert(date, @ModifyDate)))
	order by
		case when @SortingColumn = 'Id' and @SortingOrder = 'asc' then up.Id end,
		case when @SortingColumn = 'Id' and @SortingOrder = 'desc' then up.Id end desc,
		case when @SortingColumn = 'UserId' and @SortingOrder = 'asc' then up.UserId end,
		case when @SortingColumn = 'UserId' and @SortingOrder = 'desc' then up.UserId end desc,
		case when @SortingColumn = 'UserLogin' and @SortingOrder = 'asc' then u.[Login] end,
		case when @SortingColumn = 'UserLogin' and @SortingOrder = 'desc' then u.[Login] end desc,
		case when @SortingColumn = 'StoreId' and @SortingOrder = 'asc' then up.StoreId end,
		case when @SortingColumn = 'StoreId' and @SortingOrder = 'desc' then up.StoreId end desc,
		case when @SortingColumn = 'StoreName' and @SortingOrder = 'asc' then s.[Name] end,
		case when @SortingColumn = 'StoreName' and @SortingOrder = 'desc' then s.[Name] end desc,
		case when @SortingColumn = 'PostId' and @SortingOrder = 'asc' then up.PostId end,
		case when @SortingColumn = 'PostId' and @SortingOrder = 'desc' then up.PostId end desc,
		case when @SortingColumn = 'PostName' and @SortingOrder = 'asc' then p.[Name] end,
		case when @SortingColumn = 'PostName' and @SortingOrder = 'desc' then p.[Name] end desc,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'asc' then up.IsDeleted end,
		case when @SortingColumn = 'IsDeleted' and @SortingOrder = 'desc' then up.IsDeleted end desc,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'asc' then up.CreateDate end,
		case when @SortingColumn = 'CreateDate' and @SortingOrder = 'desc' then up.CreateDate end desc,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'asc' then up.ModifyDate end,
		case when @SortingColumn = 'ModifyDate' and @SortingOrder = 'desc' then up.ModifyDate end desc
	offset iif(@Page * @Size > 0, @Page * @Size, 0) rows
 	fetch next iif(@Size > 0, @Size, convert(int, 0x7fffffff)) rows only;