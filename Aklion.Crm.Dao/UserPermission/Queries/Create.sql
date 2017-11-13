insert dbo.UserPermission
(
	UserId,
	StoreId,
	Permission,
	CreateDate,
	ModifyDate
)
values
(
	@UserId,
	@StoreId,
	@Permission,
    getdate(),
    null
);

select
	scope_identity();