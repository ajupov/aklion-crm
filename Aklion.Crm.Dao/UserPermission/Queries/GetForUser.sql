select
    Id,
    UserId,
    StoreId,
    Permission,
    CreateDate,
    ModifyDate
	from dbo.UserPermission
	where UserId = @userId
		and StoreId = @storeId
		and Permission = @permission