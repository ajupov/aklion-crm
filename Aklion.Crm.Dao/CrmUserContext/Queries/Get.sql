declare @UserId				int,
		@UserLogin			varchar(256),
		@IsEmailConfirmed	bit,
		@IsPhoneConfirmed	bit,
		@IsLocked			bit,
		@IsDeleted			bit,
		@AvatarUrl			varchar(2048),
		@StoreId			int,
		@StoreName			varchar(256),
		@StoreIsLocked		bit,
		@StoreIsDeleted		bit;

select top 1
	@UserId = Id,
    @UserLogin = [Login],
    @IsEmailConfirmed = IsEmailConfirmed,
    @IsPhoneConfirmed = IsPhoneConfirmed,
    @IsLocked = IsLocked,
    @IsDeleted = IsDeleted,
    @AvatarUrl = AvatarUrl
	from dbo.[User]
	where [Login] = @login;

select top 1
	@StoreId = iif(
		exists (
			select
				1
				from dbo.UserPermission
				where UserId = @UserId 
					and StoreId = @selectedStoreId 
					and coalesce(Permission, 0) != 0),
		@selectedStoreId,
		0);

select top 1
	@StoreId = Id,
    @StoreName = [Name],
    @StoreIsLocked = IsLocked,
    @StoreIsDeleted = IsDeleted
	from dbo.Store
	where Id = @StoreId;

select top 1
	@UserId				as UserId,
	@UserLogin			as UserLogin,	
	@IsEmailConfirmed	as IsEmailConfirmed,
	@IsPhoneConfirmed	as IsPhoneConfirmed,
	@IsLocked			as IsLocked,
	@IsDeleted			as IsDeleted,
	@AvatarUrl			as AvatarUrl,
	@StoreId			as StoreId,
	@StoreName			as StoreName,
	@StoreIsLocked		as StoreIsLocked,
	@StoreIsDeleted		as StoreIsDeleted;

select
    Permission
	from dbo.UserPermission
	where UserId = @UserId 
		and StoreId = @StoreId
		and coalesce(Permission, 0) != 0;

select
	s.Id,
	s.[Name]
	from dbo.Store as s
		inner join dbo.UserPermission as up on
			s.Id = up.Permission
	where up.UserId = @UserId
		and coalesce(up.Permission, 0) != 0;