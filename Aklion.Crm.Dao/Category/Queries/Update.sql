update dbo.Category
    set StoreId = @StoreId,
		[Name] = @Name,
		ParentId = @ParentId,
		IsDeleted = @IsDeleted,
		ModifyDate = getdate()
    where Id = @Id;