update dbo.ProductCategory
    set StoreId = @StoreId,
		ProductId = @ProductId,
		CategoryId = @CategoryId,
		IsDeleted = @IsDeleted,
		ModifyDate = getdate()
    where Id = @Id;