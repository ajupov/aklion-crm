select
	Id
	from dbo.[Order]
	where convert(varchar, Id) like convert(varchar, @pattern)  + '%'
		and StoreId = @storeId
		and IsDeleted = 0;