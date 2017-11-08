select
	Id,
    [Login]	as [Value]
	from dbo.[User]
	where IsLocked = 0
		and IsDeleted = 0
		and [Login] like @loginPattern + '%';