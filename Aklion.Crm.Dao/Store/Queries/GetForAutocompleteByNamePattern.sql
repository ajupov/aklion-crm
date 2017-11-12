select
	Id,
    [Name]	as [Value]
	from dbo.Store
	where IsLocked = 0
		and IsDeleted = 0
		and [Name] like @pattern + '%';