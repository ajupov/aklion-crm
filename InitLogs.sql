create database aklion_crm_logs
collate Cyrillic_General_CI_AS;
go

use aklion_crm_logs;
go

create table dbo.[Log]
(
    Id          int           not null identity(1, 1) constraint PK_Log_Id primary key,
    [Value]     varchar(4000) not null,
    [TimeStamp] datetime2(7)  not null
);
go