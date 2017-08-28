select top 1
	Id,
    [Name],
    IsDeleted,
    CreateDate,
    ModifyDate
	from dbo.Organization
	where Id = @id;