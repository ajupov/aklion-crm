insert dbo.UserToken
(
	UserId,
	TokenType,
	Token,
	ExpirationDate,
	IsUsed,
	CreateDate,
	ModifyDate
)
values
(
	@UserId,
	@TokenType,
	@Token,
	@ExpirationDate,
	@IsUsed,
    getdate(),
    null
);

select
	scope_identity();