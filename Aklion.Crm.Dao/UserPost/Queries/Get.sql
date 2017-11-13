select top 1
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
	where up.Id = @id;