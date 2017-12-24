create database aklion_crm
collate Cyrillic_General_CI_AS;
go

use aklion_crm;
go

/*** Магазины ***/
-- Магазин
create table dbo.Store
(
    Id         int          not null identity(1, 1) constraint PK_Store_Id primary key,
    [Name]     varchar(256) not null constraint UQ_Store_Name unique,
    ApiSecret  varchar(16)  null,
    IsLocked   bit          not null,
    IsDeleted  bit          not null,
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null
);
go

/*** Пользователи ***/
-- Любой пользователь, котрый может войти в систему: админ, владелец магазина, менеджер, оператор, курьер и т.д.
create table dbo.[User]
(
    Id               int           not null identity(1, 1) constraint PK_User_Id primary key,
    [Login]          varchar(256)  not null constraint UQ_User_Login unique,
    PasswordHash     varchar(512)  null,
    Email            varchar(256)  not null constraint UQ_User_Email unique,
    Phone            varchar(10)   not null constraint UQ_User_Phone unique,
    Surname          varchar(256)  not null,
    [Name]           varchar(256)  not null,
    Patronymic       varchar(256)  not null,
    Gender           tinyint       not null,
    BirthDate        date          null,
    IsEmailConfirmed bit           not null,
    IsPhoneConfirmed bit           not null,
    IsLocked         bit           not null,
    IsDeleted        bit           not null,
    AvatarUrl        varchar(2048) null,
    CreateDate       datetime2(7)  not null,
    ModifyDate       datetime2(7)  null
);
go
-- Атрибут пользователя
create table dbo.UserAttribute
(
    Id            int           not null identity(1, 1) constraint PK_UserAttribute_Id primary key,
    StoreId       int           not null constraint FK_UserAttribute_StoreId foreign key (StoreId) references dbo.Store (Id) index IX_UserAttribute_StoreId nonclustered (StoreId),
    [Name]        varchar(256)  not null,
    [Description] varchar(1024) not null,
    IsDeleted     bit           not null,
    CreateDate    datetime2(7)  not null,
    ModifyDate    datetime2(7)  null,
    constraint UQ_UserAttribute_StoreId_Name unique (StoreId, [Name])
);
go
-- Cвязь пользователя и атрибута пользователя
create table dbo.UserAttributeLink
(
    Id          int          not null identity(1, 1) constraint PK_UserAttributeLink_Id primary key,
    StoreId     int          not null constraint FK_UserAttributeLink_StoreId foreign key (StoreId) references dbo.Store (Id) index IX_UserAttributeLink_StoreId nonclustered (StoreId),
    UserId      int          not null constraint FK_UserAttributeLink_UserId foreign key (UserId) references dbo.[User] (Id) index IX_UserAttributeLink_UserId nonclustered (UserId),
    AttributeId int          not null constraint FK_UserAttributeLink_AttributeId foreign key (AttributeId) references dbo.UserAttribute (Id) index IX_UserAttributeLink_AttributeId nonclustered (AttributeId),
    [Value]     varchar(max) null,
    IsDeleted   bit          not null,
    CreateDate  datetime2(7) not null,
    ModifyDate  datetime2(7) null,
    constraint UQ_UserAttributeLink_StoreId_UserId_AttributeId unique (StoreId, UserId, AttributeId)
);
go
-- Права пользователей внутри магазина. Права берутся из Enum.
create table dbo.UserPermission
(
    Id         int          not null identity(1, 1) constraint PK_UserPermission_Id primary key,
    UserId     int          not null constraint FK_UserPermission_UserId foreign key (UserId) references dbo.[User] (Id) index IX_UserPermission_UserId nonclustered (UserId),
    StoreId    int          null constraint FK_UserPermission_StoreId foreign key (StoreId) references dbo.Store (Id) index IX_UserPermission_StoreId nonclustered (StoreId),
    Permission tinyint      not null,
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null,
    constraint UQ_UserPermission_UserId_StoreId_Permission unique (UserId, StoreId, Permission)
);
go
-- Токены при смене телефона, email, пароля и т.д.
create table dbo.UserToken
(
    Id             int          not null identity(1, 1) constraint PK_UserToken_Id primary key,
    UserId         int          not null constraint FK_UserToken_UserId foreign key (UserId) references dbo.[User] (Id) index IX_UserToken_UserId nonclustered (UserId),
    TokenType      tinyint      not null,
    Token          varchar(256) not null,
    ExpirationDate datetime2(7) not null,
    IsUsed         bit          not null,
    CreateDate     datetime2(7) not null,
    ModifyDate     datetime2(7) null
);
go

/*** Продукты ***/
-- Статус продукта
create table dbo.ProductStatus
(
    Id         int          not null identity(1, 1) constraint PK_ProductStatus_Id primary key,
    StoreId    int          not null constraint FK_ProductStatus_StoreId foreign key (StoreId) references dbo.Store (Id) index IX_ProductStatus_StoreId nonclustered (StoreId),
    [Name]     varchar(256) not null,
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null,
);
go

-- Продукт (товар или услуга)
create table dbo.Product
(
    Id         int            not null identity(1, 1) constraint PK_Product_Id primary key,
    StoreId    int            not null constraint FK_Product_StoreId foreign key (StoreId) references dbo.Store (Id) index IX_Product_StoreId nonclustered (StoreId),
    ParentId   int            null,
    [Name]     varchar(256)   not null,
    Price      decimal(18, 2) not null,
    StatusId   int            not null constraint FK_Product_StatusId foreign key (StatusId) references dbo.ProductStatus (Id) index IX_Product_StatusId nonclustered (StatusId),
    VendorCode varchar(16)    null,
    IsDeleted  bit            not null,
    CreateDate datetime2(7)   not null,
    ModifyDate datetime2(7)   null,
    constraint UQ_Product_StoreId_Name_VendorCode unique (StoreId, [Name])
);
go
-- Атрибут продукта
create table dbo.ProductAttribute
(
    Id            int           not null identity(1, 1) constraint PK_ProductAttribute_Id primary key,
    StoreId       int           not null constraint FK_ProductAttribute_StoreId foreign key (StoreId) references dbo.Store (Id) index IX_ProductAttribute_StoreId nonclustered (StoreId),
    [Name]        varchar(256)  not null,
    [Description] varchar(1024) not null,
    IsDeleted     bit           not null,
    CreateDate    datetime2(7)  not null,
    ModifyDate    datetime2(7)  null,
    constraint UQ_ProductAttribute_StoreId_Name unique (StoreId, [Name])
);
go
-- Cвязь продукта и атрибута продукта
create table dbo.ProductAttributeLink
(
    Id          int          not null identity(1, 1) constraint PK_ProductAttributeLink_Id primary key,
    StoreId     int          not null constraint FK_ProductAttributeLink_StoreId foreign key (StoreId) references dbo.Store (Id) index IX_ProductAttributeLink_StoreId nonclustered (StoreId),
    ProductId   int          not null constraint FK_ProductAttributeLink_ProductId foreign key (ProductId) references dbo.Product (Id) index IX_ProductAttributeLink_ProductId nonclustered (ProductId),
    AttributeId int          not null constraint FK_ProductAttributeLink_AttributeId foreign key (AttributeId) references dbo.ProductAttribute (Id) index IX_ProductAttributeLink_AttributeId nonclustered (AttributeId),
    [Value]     varchar(max) null,
    IsDeleted   bit          not null,
    CreateDate  datetime2(7) not null,
    ModifyDate  datetime2(7) null,
    constraint UQ_ProductAttributeLink_StoreId_ProductId_AttributeId unique (StoreId, ProductId, AttributeId)
);
go

/*** Клиенты ***/
-- Клиент
create table dbo.Client
(
    Id         int          not null identity(1, 1) constraint PK_Client_Id primary key,
    StoreId    int          not null constraint FK_Client_StoreId foreign key (StoreId) references dbo.Store (Id) index IX_Client_StoreId nonclustered (StoreId),
    [Name]     varchar(256) not null,
    IsDeleted  bit          not null,
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null
);
go
-- Атрибут клиента
create table dbo.ClientAttribute
(
    Id            int           not null identity(1, 1) constraint PK_ClientAttribute_Id primary key,
    StoreId       int           not null constraint FK_ClientAttribute_StoreId foreign key (StoreId) references dbo.Store (Id) index IX_ClientAttribute_StoreId nonclustered (StoreId),
    [Name]        varchar(256)  not null,
    [Description] varchar(1024) not null,
    IsDeleted     bit           not null,
    CreateDate    datetime2(7)  not null,
    ModifyDate    datetime2(7)  null,
    constraint UQ_ClientAttribute_StoreId_Name unique (StoreId, [Name])
);
go
-- Cвязь клиента и атрибута клиента
create table dbo.ClientAttributeLink
(
    Id          int          not null identity(1, 1) constraint PK_ClientAttributeLink_Id primary key,
    StoreId     int          not null constraint FK_ClientAttributeLink_StoreId foreign key (StoreId) references dbo.Store (Id) index IX_ClientAttributeLink_StoreId nonclustered (StoreId),
    ClientId    int          not null constraint FK_ClientAttributeLink_ProductId foreign key (ClientId) references dbo.Client (Id) index IX_ClientAttributeLink_ClientId nonclustered (ClientId),
    AttributeId int          not null constraint FK_ClientAttributeLink_AttributeId foreign key (AttributeId) references dbo.ClientAttribute (Id) index IX_ClientAttributeLink_AttributeId nonclustered (AttributeId),
    [Value]     varchar(max) null,
    IsDeleted   bit          not null,
    CreateDate  datetime2(7) not null,
    ModifyDate  datetime2(7) null,
    constraint UQ_ClientAttributeLink_StoreId_ClientId_AttributeId unique (StoreId, ClientId, AttributeId)
);
go

/*** Заказы ***/
-- Источник заказа
create table dbo.OrderSource
(
    Id         int          not null identity(1, 1) constraint PK_OrderSource_Id primary key,
    StoreId    int          not null constraint FK_OrderSource_StoreId foreign key (StoreId) references dbo.Store (Id) index IX_OrderSource_StoreId nonclustered (StoreId),
    [Name]     varchar(256) not null,
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null,
);
-- Статус заказа
create table dbo.OrderStatus
(
    Id         int          not null identity(1, 1) constraint PK_OrderStatus_Id primary key,
    StoreId    int          not null constraint FK_OrderStatus_StoreId foreign key (StoreId) references dbo.Store (Id) index IX_OrderStatus_StoreId nonclustered (StoreId),
    [Name]     varchar(256) not null,
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null,
);
-- Заказ
create table dbo.[Order]
(
    Id          int            not null identity(1, 1) constraint PK_Order_Id primary key,
    StoreId     int            not null constraint FK_Order_StoreId foreign key (StoreId) references dbo.Store (Id) index IX_Order_StoreId nonclustered (StoreId),
    ClientId    int            not null constraint FK_Order_ClientId foreign key (ClientId) references dbo.Client (Id) index IX_Order_ClientId nonclustered (ClientId),
    SourceId    int            not null constraint FK_Order_SourceId foreign key (SourceId) references dbo.OrderSource (Id) index IX_Order_SourceId nonclustered (SourceId),
    StatusId    int            not null constraint FK_Order_StatusId foreign key (StatusId) references dbo.OrderStatus (Id) index IX_Order_StatusId nonclustered (StatusId),
    TotalSum    decimal(18, 2) null,
    DiscountSum decimal(18, 2) null,
    IsDeleted   bit            not null,
    CreateDate  datetime2(7)   not null,
    ModifyDate  datetime2(7)   null,
);
go
-- Атрибут заказа
create table dbo.OrderAttribute
(
    Id            int           not null identity(1, 1) constraint PK_OrderAttribute_Id primary key,
    StoreId       int           not null constraint FK_OrderAttribute_StoreId foreign key (StoreId) references dbo.Store (Id) index IX_OrderAttribute_StoreId nonclustered (StoreId),
    [Name]        varchar(256)  not null,
    [Description] varchar(1024) not null,
    IsDeleted     bit           not null,
    CreateDate    datetime2(7)  not null,
    ModifyDate    datetime2(7)  null,
    constraint UQ_OrderAttribute_StoreId_Name unique (StoreId, [Name])
);
go
-- Cвязь заказа и атрибута заказа
create table dbo.OrderAttributeLink
(
    Id          int          not null identity(1, 1) constraint PK_OrderAttributeLink_Id primary key,
    StoreId     int          not null constraint FK_OrderAttributeLink_StoreId foreign key (StoreId) references dbo.Store (Id) index IX_OrderAttributeLink_StoreId nonclustered (StoreId),
    OrderId     int          not null constraint FK_OrderAttributeLink_OrderId foreign key (OrderId) references dbo.[Order] (Id) index IX_OrderAttributeLink_ClientId nonclustered (OrderId),
    AttributeId int          not null constraint FK_OrderAttributeLink_AttributeId foreign key (AttributeId) references dbo.OrderAttribute (Id) index IX_OrderAttributeLink_AttributeId nonclustered (AttributeId),
    [Value]     varchar(max) null,
    IsDeleted   bit          not null,
    CreateDate  datetime2(7) not null,
    ModifyDate  datetime2(7) null,
    constraint UQ_OrderAttributeLink_StoreId_OrderId_AttributeId unique (StoreId, OrderId, AttributeId)
);
go
-- Позиция заказа
create table dbo.OrderItem
(
    Id         int            not null identity(1, 1) constraint PK_OrderItem_Id primary key,
    StoreId    int            not null constraint FK_OrderItem_StoreId foreign key (StoreId) references dbo.Store (Id) index IX_OrderItem_StoreId nonclustered (StoreId),
    OrderId    int            not null constraint FK_OrderItem_OrderId foreign key (OrderId) references dbo.[Order] (Id) index IX_OrderItem_OrderId nonclustered (OrderId),
    ProductId  int            not null constraint FK_OrderItem_ProductId foreign key (ProductId) references dbo.Product (Id) index IX_OrderItem_ProductId nonclustered (ProductId),
    Price      decimal(18, 2) not null,
    [Count]    int            not null,
    IsDeleted  bit            not null,
    CreateDate datetime2(7)   not null,
    ModifyDate datetime2(7)   null
);
go

-- Аудит
create table dbo.[AuditLog]
(
    Id          int          not null identity(1, 1) constraint PK_Log_Id primary key,
    UserId      int          null,
    StoreId     int          null,
    ActionType  tinyint      not null,
    [OldValue]  varchar(max) not null,
    [NewValue]  varchar(max) not null,
    [TimeStamp] datetime2(7) not null
);
go

--password: admin
set identity_insert dbo.[User] on;
insert dbo.[User] (Id, [Login], PasswordHash, Email, Phone, Surname, [Name], Patronymic, Gender, BirthDate, IsEmailConfirmed,  IsPhoneConfirmed, IsLocked, IsDeleted, AvatarUrl, CreateDate, ModifyDate)
	values (1, 'admin', 'ACCSme+dFh2xam1fnEW5HTSefoK4bS6hOPHIbz6J7gwojfR82BA6MHJmThclzPN7VgTcHpdax+rTCkbuoAdVjwlvD/XSNcuzbOKgY4V3u5h72OFauWyi/7dfSST/5odIbMz0qoBkYU6+FdzY7g8p//i5uNNCVfBJvTHPvlQ/qZloDECeC6NKtHF3T4zSWocO6Gj286sO6jmcouzVL17OtxdFGbJZbJb6snVFOxNsboNzxQovlw8xFEjCpifN5BoWAqulMF2uxAjg/il3ZYl6peWymauKTqHXcY/FszLAjwL2VWwwSLHPwvzYZUriSZb7vj/oIFlLLXRLu6g2JLI8oQ==', 'au073@mail.ru', '9378164793', 'Аюпов', 'Усман', 'Кябирович', 1, '1994-10-17', 1, 1, 0, 0, null, getdate(), null),
		   (2, 'api', null, 'api', 'api', 'api', 'api', 'api', 0, null, 1, 1, 0, 0, null, getdate(), null),
		   (3, 'console', null, 'console', 'console', 'console', 'console', 'console', 0, null, 1, 1, 0, 0, null, getdate(), null);
set identity_insert dbo.[User] off;
go