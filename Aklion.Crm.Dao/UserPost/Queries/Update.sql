update dbo.UserPost
    set UserId = @UserId,
		StoreId = @StoreId,
		PostId = @PostId,
		IsDeleted = @IsDeleted,
		ModifyDate = getdate()
    where Id = @Id;