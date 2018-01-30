declare @correctStoreId int = (
	select top 1
		iif(
			exists (
				select
					1
					from dbo.UserPermission
					where UserId = @userId 
						and StoreId = @storeId 
						and coalesce(Permission, 0) != 0),
			@storeId,
			(
				select top 1
					up.StoreId
					from dbo.UserPermission as up
						inner join dbo.Store as s on
							up.StoreId = s.Id
					where up.UserId = @userId
					order by s.IsDeleted, s.IsLocked, s.CreateDate
			)));

select top 1
	Id,
    [Login],
    IsEmailConfirmed,
    IsPhoneConfirmed,
    IsLocked,
    IsDeleted,
    AvatarUrl
	from dbo.[User]
	where Id = @userId;

select top 1
	Id,
    Name,
    IsLocked,
    IsDeleted
	from dbo.Store
	where Id = @correctStoreId;

select
    Permission
	from dbo.UserPermission
	where UserId = @userId 
		and (StoreId is null or StoreId = @correctStoreId)
		and coalesce(Permission, 0) != 0;