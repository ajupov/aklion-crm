insert dbo.ProductTag
(
	StoreId,
	ProductId,
	TagId,
	IsDeleted,
	CreateDate,
	ModifyDate
)
values
(
	@StoreId,
	@ProductId,
	@TagId,
	@IsDeleted,
    getdate(),
    null
);

select
	scope_identity();