update dbo.UserPermission
    set UserId = @UserId,
		StoreId = @StoreId,
		Permission = @Permission,
		ModifyDate = getdate()
    where Id = @Id;