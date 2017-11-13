select top 1
	up.Id,
	up.UserId,
	u.[Login]	as UserLogin,
	up.StoreId,
	s.[Name]	as StoreName,
	up.Permission,
    up.CreateDate,
    up.ModifyDate
	from dbo.UserPermission as up
		inner join dbo.[User] as u on
			up.UserId = u.Id
		inner join dbo.Store as s on
			up.StoreId = s.Id
	where up.Id = @id;