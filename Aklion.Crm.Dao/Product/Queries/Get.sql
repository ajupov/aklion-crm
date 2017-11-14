select top 1
	p.Id,
    p.StoreId,
	s.[Name]	as StoreName,
    p.[Type],
    p.[Name],
    p.[Description],
    p.Price,
    p.[Status],
    p.VendorCode,
    p.ParentId,
    p.IsDeleted,
    p.CreateDate,
    p.ModifyDate
	from dbo.Product as p
		inner join dbo.Store as s on
			p.StoreId = s.Id
	where p.Id = @id;