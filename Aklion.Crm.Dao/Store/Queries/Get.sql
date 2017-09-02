select top 1
	Id,
    [Name],
    ApiKey,
    ApiSecret,
    IsLocked,
    IsDeleted,
    CreateDate,
    ModifyDate
	from dbo.Store
	where Id = @id;