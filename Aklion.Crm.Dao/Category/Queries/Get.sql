select top 1
	с.Id,
	с.StoreId,
	s.[Name]	as StoreName,
    с.[Name],
	c.ParentId,
	pc.[Name]	as ParentName,
    с.IsDeleted,
    с.CreateDate,
    с.ModifyDate
	from dbo.Category as с
		inner join dbo.Store as s on
			с.StoreId = s.Id
		left outer join dbo.Category as pc on
			c.ParentId = pc.Id
	where с.Id = @id;