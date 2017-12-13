select top 1
	pal.Id,
    pal.StoreId,
	s.[Name]	as StoreName,
    pal.ProductId,
	p.[Name]	as ProductName,
    pal.AttributeId,
	pa.[Name]	as AttributeName,
	pal.[Value],
	pal.IsDeleted,
    pal.CreateDate,
    pal.ModifyDate
	from dbo.ProductAttributeLink as pal
		inner join dbo.Store as s on
			pal.StoreId = s.Id
		inner join dbo.Product as p on
			pal.ProductId = p.Id
		inner join dbo.ProductAttribute as pa on
			pal.AttributeId = a.Id
	where pal.Id = @id;