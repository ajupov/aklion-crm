select top 1
	a.Id,
	a.StoreId,
	s.[Name]	as StoreName,
    a.[Name],
    a.IsDeleted,
    a.CreateDate,
    a.ModifyDate
	from dbo.Attribute as a
		inner join dbo.Store as s on
			a.StoreId = s.Id
	where a.Id = @id;