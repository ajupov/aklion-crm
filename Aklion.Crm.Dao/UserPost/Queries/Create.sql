insert dbo.UserPost
(
	UserId,
	StoreId,
	PostId,
	IsDeleted,
	CreateDate,
	ModifyDate
)
values
(
	@UserId,
	@StoreId,
	@PostId,
	@IsDeleted,
    getdate(),
    null
);

select
	scope_identity();