select top 1
	s.Id,
	s.CreateUserId,
	u.[Login]		as CreateUserLogin,
    s.[Name],
    s.ApiSecret,
    s.IsLocked,
    s.IsDeleted,
    s.CreateDate,
    s.ModifyDate
	from dbo.Store as s
		inner join dbo.[User] as u on
			s.CreateUserId = u.Id
	where s.Id = @id;