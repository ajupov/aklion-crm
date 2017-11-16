update dbo.Attribute
    set StoreId = @StoreId,
		[Name] = @Name,
		IsDeleted = @IsDeleted,
		ModifyDate = getdate()
    where Id = @Id;