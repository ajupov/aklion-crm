update dbo.Category
    set StoreId = @StoreId,
		[Name] = @Name,
		IsLocked = @IsLocked,
		IsDeleted = @IsDeleted,
		ModifyDate = getdate()
    where Id = @Id;