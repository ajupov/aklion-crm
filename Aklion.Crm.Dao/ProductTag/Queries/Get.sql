select top 1
	pt.Id,
    pt.StoreId,
	s.[Name]	as StoreName,
    pt.ProductId,
	p.[Name]	as ProductName,
    pt.TagId,
	t.[Name]	as TagName,
	pt.IsDeleted,
    pt.CreateDate,
    pt.ModifyDate
	from dbo.ProductTag as pt
		inner join dbo.Store as s on
			pt.StoreId = s.Id
		inner join dbo.Product as p on
			pt.ProductId = p.Id
		inner join dbo.Tag as t on
			pt.TagId = t.Id
	where pt.Id = @id;