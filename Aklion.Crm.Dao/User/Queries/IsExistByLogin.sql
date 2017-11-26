if(exists(select 1 from dbo.[User] where [Login] = @login))
	select convert(bit, 1);
else
	select convert(bit, 0);