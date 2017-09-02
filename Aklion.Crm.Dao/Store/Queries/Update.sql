update dbo.Store
    set [Name] = @Name,
		ApiKey = @ApiKey,
		ApiSecret = @ApiSecret,
		IsLocked = @IsLocked,
		IsDeleted = @IsDeleted,
		ModifyDate = sysdatetime()
    where Id = @Id;