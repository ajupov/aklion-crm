if(exists(select 1 from dbo.UserPermission where UserId = @userId and StoreId = @storeId))
	select convert(bit, 1);
else
	select convert(bit, 0);