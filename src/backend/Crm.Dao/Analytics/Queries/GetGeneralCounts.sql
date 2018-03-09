declare @usersCount    int = 0,
		@storesCount   int = 0,
		@productsCount int = 0,
		@clientsCount  int = 0,
		@ordersCount   int = 0;

select
    @usersCount = count(distinct up.UserId)
    from dbo.UserPermission as up
        inner join (
            select distinct
                StoreId
                from dbo.UserPermission
                where UserId = @userId
        ) as s on
            s.StoreId is null
				or up.StoreId = s.StoreId;

select
    @storesCount = count(distinct iif(up.Id is not null, coalesce(up.StoreId, 0), 0))
    from dbo.UserPermission as up
        inner join dbo.Store as s on
            up.StoreId = s.Id
    where s.IsDeleted = 0
		and up.UserId = @userId;

select
    @productsCount = count(p.Id)
    from dbo.Product as p
        inner join (
            select distinct
                StoreId
                from dbo.UserPermission
                where UserId = @userId
        ) as s on
            s.StoreId is null
                or p.StoreId = s.StoreId
    where p.IsDeleted = 0;

select
    @clientsCount = count(c.Id)
    from dbo.Client as c
        inner join (
            select distinct
                StoreId
                from dbo.UserPermission
                where UserId = @userId
        ) as s on
            s.StoreId is null
                or c.StoreId = s.StoreId
    where c.IsDeleted = 0;

select
    @ordersCount = count(o.Id)
    from dbo.[Order] as o
        inner join (
            select distinct
                StoreId
                from dbo.UserPermission
                where UserId = @userId
        ) as s on
            s.StoreId is null
                or o.StoreId = s.StoreId
    where o.IsDeleted = 0;

select
    @usersCount		as UsersCount,
    @storesCount	as StoresCount,
    @productsCount	as ProductsCount,
	@clientsCount	as ClientsCount,
    @ordersCount	as OrdersCount;