select top 1
	Id,
	CreateUserId,
    [Name],
    ApiSecret,
    IsLocked,
    IsDeleted,
    CreateDate,
    ModifyDate
	from dbo.Store
	where Id = @id;