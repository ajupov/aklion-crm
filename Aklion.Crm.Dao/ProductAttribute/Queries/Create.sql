insert dbo.ProductAttribute
(
	StoreId,
	ProductId,
	AttributeId,
	[Value],
	IsDeleted,
	CreateDate,
	ModifyDate
)
values
(
	@StoreId,
	@ProductId,
	@AttributeId,
	@Value,
	@IsDeleted,
    getdate(),
    null
);

select
	scope_identity();