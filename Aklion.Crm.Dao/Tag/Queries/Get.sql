select top 1
	t.Id,
	t.StoreId,
	s.[Name]	as StoreName,
    t.[Name],
    t.IsDeleted,
    t.CreateDate,
    t.ModifyDate
	from dbo.Tag as t
		inner join dbo.Store as s on
			t.StoreId = s.Id
	where t.Id = @id;