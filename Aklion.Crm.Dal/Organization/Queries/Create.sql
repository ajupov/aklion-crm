insert dbo.Organization
(
    [Name],
    IsDeleted,
    CreateDate,
    ModifyDate
)
values
(
    @Name,
    @IsDeleted,
    sysdatetime(),
	null
);

select
	scope_identity();