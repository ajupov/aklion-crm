select top 1
	c.Id,
	c.StoreId,
	s.[Name]	as StoreName,
    c.[Name],
	c.ParentId,
	pc.[Name]	as ParentName,
    c.IsDeleted,
    c.CreateDate,
    c.ModifyDate
	from dbo.Category as c
		inner join dbo.Store as s on
			c.StoreId = s.Id
		left outer join dbo.Category as pc on
			c.ParentId = pc.Id
	where c.Id = @id;