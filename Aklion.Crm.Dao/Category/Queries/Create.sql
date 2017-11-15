insert dbo.Category
(
	StoreId,
	[Name],
	ParentId,
	IsDeleted,
	CreateDate,
	ModifyDate
)
values
(
	@StoreId,
	@Name,
	@ParentId,
	@IsDeleted,
    getdate(),
    null
);

select
	scope_identity();