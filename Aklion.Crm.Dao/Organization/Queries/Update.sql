update dbo.Organization
	set [Name] = @Name,
		IsDeleted = @IsDeleted,
        ModifyDate = sysdatetime()
	where Id = @Id;