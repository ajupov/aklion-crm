if(exists(select 1 from dbo.[User] where [Phone] = @phone))
	select convert(bit, 1);
else
	select convert(bit, 0);