select
    Permission
	from dbo.UserPermission
	where UserId = @userId
		and StoreId = @storeId