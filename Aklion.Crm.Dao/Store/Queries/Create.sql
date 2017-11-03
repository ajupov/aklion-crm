insert dbo.Store
(
	CreateUserId,
    [Name],
    ApiSecret,
    IsLocked,
    IsDeleted,
    CreateDate,
    ModifyDate
)
values
(
	@CreateUserId,
    @Name,
    @ApiSecret,
    @IsLocked,
    @IsDeleted,
    getdate(),
    null
);

select
	scope_identity();