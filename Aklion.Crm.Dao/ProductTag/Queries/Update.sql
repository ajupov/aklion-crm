update dbo.ProductTag
    set StoreId = @StoreId,
		ProductId = @ProductId,
		TagId = @TagId,
		IsDeleted = @IsDeleted,
		ModifyDate = getdate()
    where Id = @Id;