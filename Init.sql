create database aklion_crm
collate Cyrillic_General_CI_AS;
go;

use aklion_crm;
go

create table dbo.Organization
(
    Id         int          not null identity(1, 1) constraint PK_Organization_Id primary key,
    [Name]     varchar(256) not null,
    IsDeleted  bit          not null,
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null
);
go