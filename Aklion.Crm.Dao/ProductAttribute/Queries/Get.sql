select top 1
	pa.Id,
    pa.StoreId,
	s.[Name]	as StoreName,
    pa.ProductId,
	p.[Name]	as ProductName,
    pa.AttributeId,
	a.[Name]	as AttributeName,
	pa.[Value],
	pa.IsDeleted,
    pa.CreateDate,
    pa.ModifyDate
	from dbo.ProductAttribute as pa
		inner join dbo.Store as s on
			pa.StoreId = s.Id
		inner join dbo.Product as p on
			pa.ProductId = p.Id
		inner join dbo.Attribute as a on
			pa.AttributeId = a.Id
	where pa.Id = @id;