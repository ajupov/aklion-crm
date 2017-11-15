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
	pp.[Name]	as ParentName,
    p.IsDeleted,
    p.CreateDate,
    p.ModifyDate
	from dbo.Product as p
		inner join dbo.Store as s on
			p.StoreId = s.Id
		left outer join dbo.Product as pp on
			p.ParentId = pp.id
	where p.Id = @id;