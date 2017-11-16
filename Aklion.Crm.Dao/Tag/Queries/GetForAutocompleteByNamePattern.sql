select
	Id,
    [Name]	as [Value]
	from dbo.Tag
	where IsDeleted = 0
		and (coalesce(@storeId, 0) = 0 or StoreId = @storeId)
		and [Name] like @pattern + '%';