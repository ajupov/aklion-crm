update dbo.Store
    set CreateUserId = @CreateUserId,
		[Name] = @Name,
		ApiSecret = @ApiSecret,
		IsLocked = @IsLocked,
		IsDeleted = @IsDeleted,
		ModifyDate = getdate()
    where Id = @Id;