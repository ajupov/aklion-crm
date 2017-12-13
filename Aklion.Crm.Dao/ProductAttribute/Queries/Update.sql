update dbo.Product
    set StoreId = @StoreId,
		[Type] = @Type,
		[Name] = @Name,
		[Description] = @Description,
		Price = @Price,
		[Status] = @Status,
		VendorCode = @VendorCode,
		ParentId = @ParentId,
		IsDeleted = @IsDeleted,
		ModifyDate = getdate()
    where Id = @Id;