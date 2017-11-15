insert dbo.ProductCategory
(
	StoreId,
	ProductId,
	CategoryId,
	IsDeleted,
	CreateDate,
	ModifyDate
)
values
(
	@StoreId,
	@ProductId,
	@CategoryId,
	@IsDeleted,
    getdate(),
    null
);

select
	scope_identity();