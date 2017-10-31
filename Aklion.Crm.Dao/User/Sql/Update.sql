update dbo.[User]
	set [Login] = @Login,
		PasswordHash = @PasswordHash,
		Email = @Email,
		Phone = @Phone,
		Surname = @Surname,
		[Name] = @Name,
		Patronymic = @Patronymic,
		Gender = @Gender,
		BirthDate = @BirthDate,
		IsEmailConfirmed = @IsEmailConfirmed,
		IsPhoneConfirmed = @IsPhoneConfirmed,
		IsLocked = @IsLocked,
		IsDeleted = @IsDeleted,
		AvatarUrl = @AvatarUrl,
		ModifyDate = getdate()
	where Id = @id;