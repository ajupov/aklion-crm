select top 1
	Id,
    UserId,
    TokenType,
    Token,
    ExpirationDate,
    IsUsed,
    CreateDate,
    ModifyDate
	from dbo.UserToken
	where UserId = @UserId
		and TokenType = @TokenType
		and Token = @Token
	order by CreateDate desc;