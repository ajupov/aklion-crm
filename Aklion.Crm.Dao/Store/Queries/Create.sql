insert dbo.Store
(
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