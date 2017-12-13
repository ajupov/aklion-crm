update dbo.ProductAttributeLink
    set StoreId = @StoreId,
		ProductId = @ProductId,
		AttributeId = @AttributeId,
		[Value] = @Value,
		IsDeleted = @IsDeleted,
		ModifyDate = getdate()
    where Id = @Id;