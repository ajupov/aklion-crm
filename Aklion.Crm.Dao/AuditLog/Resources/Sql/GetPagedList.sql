select
	count(0)
	from dbo.AuditLog as al
		left join dbo.[User] as u on
			al.UserId = u.Id
		left join dbo.Store as s on
			al.StoreId = s.Id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or al.Id = @Id)
			and (coalesce(@UserId, 0) = 0 or al.UserId = @UserId)
			and (coalesce(@UserLogin, '') = '' or u.Login like @UserLogin + '%')
			and (coalesce(@StoreId, 0) = 0 or al.StoreId = @StoreId)
			and (coalesce(@StoreName, '') = '' or s.[Name] like @StoreName + '%')
			and (coalesce(@Name, '') = '' or a.[Name] like @Name + '%')
			and (coalesce(@ActionType, 0) = 0 or al.ActionType = @ActionType)
			and (coalesce(@OldValue, '') = '' or al.OldValue like @OldValue + '%')
			and (coalesce(@NewValue, '') = '' or al.NewValue like @NewValue + '%')
			and (@TimeStamp is null or convert(date, al.TimeStamp) = convert(date, @TimeStamp)));

select
	al.Id,
    al.UserId,
	u.Login		as UserLogin,
    al.StoreId,
	s.Name		as StoreName,
    al.ActionType,
    al.OldValue,
    al.NewValue,
    al.TimeStamp
	from dbo.AuditLog as al
		left join dbo.[User] as u on
			al.UserId = u.Id
		left join dbo.Store as s on
			al.StoreId = s.Id
	where @IsSearch = 0 or
		((coalesce(@Id, 0) = 0 or al.Id = @Id)
			and (coalesce(@UserId, 0) = 0 or al.UserId = @UserId)
			and (coalesce(@UserLogin, '') = '' or u.Login like @UserLogin + '%')
			and (coalesce(@StoreId, 0) = 0 or al.StoreId = @StoreId)
			and (coalesce(@StoreName, '') = '' or s.[Name] like @StoreName + '%')
			and (coalesce(@Name, '') = '' or a.[Name] like @Name + '%')
			and (coalesce(@ActionType, 0) = 0 or al.ActionType = @ActionType)
			and (coalesce(@OldValue, '') = '' or al.OldValue like @OldValue + '%')
			and (coalesce(@NewValue, '') = '' or al.NewValue like @NewValue + '%')
			and (@TimeStamp is null or convert(date, al.TimeStamp) = convert(date, @TimeStamp)))
	order by
		case when @SortingColumn = 'Id' and @SortingOrder = 'asc' then al.Id end,
		case when @SortingColumn = 'Id' and @SortingOrder = 'desc' then al.Id end desc,
		case when @SortingColumn = 'UserId' and @SortingOrder = 'asc' then al.UserId end,
		case when @SortingColumn = 'UserId' and @SortingOrder = 'desc' then al.UserId end desc,
		case when @SortingColumn = 'UserLogin' and @SortingOrder = 'asc' then u.Login end,
		case when @SortingColumn = 'UserLogin' and @SortingOrder = 'desc' then u.Login end desc,
		case when @SortingColumn = 'StoreId' and @SortingOrder = 'asc' then al.StoreId end,
		case when @SortingColumn = 'StoreId' and @SortingOrder = 'desc' then al.StoreId end desc,
		case when @SortingColumn = 'StoreName' and @SortingOrder = 'asc' then s.[Name] end,
		case when @SortingColumn = 'StoreName' and @SortingOrder = 'desc' then s.[Name] end desc,
		case when @SortingColumn = 'ActionType' and @SortingOrder = 'asc' then al.ActionType end,
		case when @SortingColumn = 'ActionType' and @SortingOrder = 'desc' then al.ActionType end desc,
		case when @SortingColumn = 'OldValue' and @SortingOrder = 'asc' then al.OldValue end,
		case when @SortingColumn = 'OldValue' and @SortingOrder = 'desc' then al.OldValue end desc,
		case when @SortingColumn = 'NewValue' and @SortingOrder = 'asc' then al.NewValue end,
		case when @SortingColumn = 'NewValue' and @SortingOrder = 'desc' then al.NewValue end desc,
		case when @SortingColumn = 'TimeStamp' and @SortingOrder = 'asc' then al.TimeStamp end,
		case when @SortingColumn = 'TimeStamp' and @SortingOrder = 'desc' then al.TimeStamp end desc
	offset iif(@Page * @Size > 0, @Page * @Size, 0) rows
 	fetch next iif(@Size > 0, @Size, convert(int, 0x7fffffff)) rows only;