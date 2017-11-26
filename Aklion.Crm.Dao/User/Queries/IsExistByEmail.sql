if(exists(select 1 from dbo.[User] where [Email] = @email))
	select convert(bit, 1);
else
	select convert(bit, 0);