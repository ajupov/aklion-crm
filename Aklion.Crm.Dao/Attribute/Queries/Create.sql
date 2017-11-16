insert dbo.Attribute
(
	StoreId,
	[Name],
	IsDeleted,
	CreateDate,
	ModifyDate
)
values
(
	@StoreId,
	@Name,
	@IsDeleted,
    getdate(),
    null
);

select
	scope_identity();