insert dbo.Store
(
	CreateUserId,
    [Name],
    ApiKey,
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
    @ApiKey,
    @ApiSecret,
    @IsLocked,
    @IsDeleted,
    sysdatetime(),
    null
);

select
	scope_identity();