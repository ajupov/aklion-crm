declare @userId int = (
	select top 1
		Id
		from dbo.[User]
		where [Login] = @login);

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

declare @storeId int = (
	select top 1
		iif(
			exists (
				select
					1
					from dbo.UserPermission
					where UserId = @userId 
						and StoreId = @selectedStoreId 
						and coalesce(Permission, 0) != 0),
			@selectedStoreId,
			0));

select top 1
	Id,
    [Name],
    IsLocked,
    IsDeleted
	from dbo.Store
	where Id = @storeId;

select
    Permission
	from dbo.UserPermission
	where UserId = @userId 
		and StoreId = @storeId
		and coalesce(Permission, 0) != 0;

select
	s.Id,
	s.[Name]
	from dbo.Store as s
		inner join dbo.UserPermission as up on
			s.Id = up.Permission
	where up.UserId = @UserId
		and coalesce(up.Permission, 0) != 0;