select top 1
	с.Id,
	с.StoreId,
	s.[Name]	as StoreName,
    с.[Name],
    с.IsDeleted,
    с.CreateDate,
    с.ModifyDate
	from dbo.Category as с
		inner join dbo.Store as s on
			с.StoreId = s.Id
	where с.Id = @id;