select top 1
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
	where a.Id = @id;