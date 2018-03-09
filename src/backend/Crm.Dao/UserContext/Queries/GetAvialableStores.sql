select distinct
	s.Id,
	s.Name
	from dbo.UserPermission as up
		inner join dbo.Store as s on
			up.StoreId = s.Id
	where s.IsDeleted = 0
		and up.UserId = @userId;