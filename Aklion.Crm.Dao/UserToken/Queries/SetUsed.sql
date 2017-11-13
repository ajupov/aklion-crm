update dbo.UserToken
    set IsUsed = @IsUsed,
		ModifyDate = getdate()
    where Id = @id;