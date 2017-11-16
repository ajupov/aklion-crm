update dbo.ProductAttribute
    set StoreId = @StoreId,
		ProductId = @ProductId,
		AttributeId = @AttributeId,
		[Value] = @Value,
		IsDeleted = @IsDeleted,
		ModifyDate = getdate()
    where Id = @Id;