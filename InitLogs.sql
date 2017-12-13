create database aklion_crm_logs
collate Cyrillic_General_CI_AS;
go

use aklion_crm_logs;
go

-- AdminUser	(UserId = 1, StoreId = null), 
-- ApiUser		(UserId = 2, StoreId = {id}),
-- ConsoleUser	(UserId = 3, StoreId = null),
-- Users		(UserId = {id}, StoreId = {id});

--Create ApiUser

create table dbo.[Log]
(
    Id          int           not null identity(1, 1) constraint PK_Log_Id primary key,
	UserId		int null,
	StoreId		int null,


    [Value]     varchar(4000) not null,
    [TimeStamp] datetime2(7)  not null
);
go