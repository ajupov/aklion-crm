select top 1
	pc.Id,
    pc.StoreId,
	s.[Name]	as StoreName,
    pc.ProductId,
	p.[Name]	as ProductName,
    pc.CategoryId,
	c.[Name]	as CategoryName,
	pc.IsDeleted,
    pc.CreateDate,
    pc.ModifyDate
	from dbo.ProductCategory as pc
		inner join dbo.Store as s on
			pc.StoreId = s.Id
		inner join dbo.Product as p on
			pc.ProductId = p.Id
		inner join dbo.Category as c on
			pc.CategoryId = c.Id
	where pc.Id = @id;