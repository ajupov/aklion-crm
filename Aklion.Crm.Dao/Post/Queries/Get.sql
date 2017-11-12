select top 1
	p.Id,
	p.StoreId,
	s.[Name]	as StoreName,
    p.[Name],
    p.IsDeleted,
    p.CreateDate,
    p.ModifyDate
	from dbo.Post as p
		inner join dbo.Store as s on
			p.StoreId = s.Id
	where p.Id = @id;