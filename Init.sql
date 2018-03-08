create database crm_main
collate Cyrillic_General_CI_AS;
go

use crm_main;
go

/*** Магазины ***/
-- Магазин
create table dbo.Store
(
    Id         int          not null identity(1, 1) constraint PK_Store_Id primary key,
    [Name]     varchar(256) not null,
    ApiSecret  varchar(16)  null,
    IsLocked   bit          not null,
    IsDeleted  bit          not null,
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null,
	index IX_Store_Name_ApiSecret_IsLocked_IsDeleted nonclustered ([Name], ApiSecret, IsLocked, IsDeleted)
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
    ModifyDate       datetime2(7)  null,
	index IX_User_Login_Email_Phone_IsLocked_IsDeleted nonclustered ([Login], Email, Phone, IsLocked, IsDeleted)
);
go
-- Атрибут пользователя
create table dbo.UserAttribute
(
    Id         int           not null identity(1, 1) constraint PK_UserAttribute_Id primary key,
    StoreId    int           not null constraint FK_UserAttribute_StoreId foreign key (StoreId) references dbo.Store (Id),
    [Key]      varchar(256)  not null,
    [Name]     varchar(1024) not null,
    IsDeleted  bit           not null,
    CreateDate datetime2(7)  not null,
    ModifyDate datetime2(7)  null,
    constraint UQ_UserAttribute_StoreId_Key unique (StoreId, [Key]),
	index IX_UserAttribute_StoreId_Key_Name_IsDeleted nonclustered (StoreId, [Key], [Name], IsDeleted)
);
go
-- Cвязь пользователя и атрибута пользователя
create table dbo.UserAttributeLink
(
    Id          int          not null identity(1, 1) constraint PK_UserAttributeLink_Id primary key,
    StoreId     int          not null constraint FK_UserAttributeLink_StoreId foreign key (StoreId) references dbo.Store (Id),
    UserId      int          not null constraint FK_UserAttributeLink_UserId foreign key (UserId) references dbo.[User] (Id),
    AttributeId int          not null constraint FK_UserAttributeLink_AttributeId foreign key (AttributeId) references dbo.UserAttribute (Id),
    [Value]     varchar(max) null,
    IsDeleted   bit          not null,
    CreateDate  datetime2(7) not null,
    ModifyDate  datetime2(7) null,
    constraint UQ_UserAttributeLink_StoreId_UserId_AttributeId unique (StoreId, UserId, AttributeId),
	index IX_UserAttributeLink_StoreId_UserId_AttributeId_IsDeleted nonclustered (StoreId, UserId, AttributeId, IsDeleted)
);
go
-- Права пользователей внутри магазина. Права берутся из Enum.
create table dbo.UserPermission
(
    Id         int          not null identity(1, 1) constraint PK_UserPermission_Id primary key,
    StoreId    int          null constraint FK_UserPermission_StoreId foreign key (StoreId) references dbo.Store (Id),
    UserId     int          not null constraint FK_UserPermission_UserId foreign key (UserId) references dbo.[User] (Id),
    Permission tinyint      not null,
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null,
    constraint UQ_UserPermission_StoreId_UserId_Permission unique (StoreId, UserId, Permission),
	index IX_UserPermission_StoreId_UserId_Permission nonclustered (StoreId, UserId, Permission)
);
go
-- Токены при смене телефона, email, пароля и т.д.
create table dbo.UserToken
(
    Id             int          not null identity(1, 1) constraint PK_UserToken_Id primary key,
    UserId         int          not null constraint FK_UserToken_UserId foreign key (UserId) references dbo.[User] (Id),
    TokenType      tinyint      not null,
    Token          varchar(256) not null,
    ExpirationDate datetime2(7) not null,
    IsUsed         bit          not null,
    CreateDate     datetime2(7) not null,
    ModifyDate     datetime2(7) null,
	index IX_UserToken_UserId_TokenType_ExpirationDate_IsUsed nonclustered (UserId, TokenType, ExpirationDate, IsUsed)
);
go

/*** Продукты ***/
-- Статус продукта
create table dbo.ProductStatus
(
    Id         int          not null identity(1, 1) constraint PK_ProductStatus_Id primary key,
    StoreId    int          not null constraint FK_ProductStatus_StoreId foreign key (StoreId) references dbo.Store (Id),
    [Name]     varchar(256) not null,
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null,
	constraint UQ_ProductStatus_StoreId_Name unique (StoreId, [Name]),
	index IX_ProductStatus_StoreId_Name nonclustered (StoreId, [Name])
);
go
-- Продукт (товар или услуга)
create table dbo.Product
(
    Id         int            not null identity(1, 1) constraint PK_Product_Id primary key,
    StoreId    int            not null constraint FK_Product_StoreId foreign key (StoreId) references dbo.Store (Id),
    ParentId   int            null,
    [Name]     varchar(256)   not null,
    Price      decimal(18, 2) not null,
    StatusId   int            not null constraint FK_Product_StatusId foreign key (StatusId) references dbo.ProductStatus (Id),
    VendorCode varchar(16)    null,
    IsDeleted  bit            not null,
    CreateDate datetime2(7)   not null,
    ModifyDate datetime2(7)   null,
    constraint UQ_Product_StoreId_Name unique (StoreId, [Name]),
	index IX_Product_StoreId_Name_StatusId_IsDeleted nonclustered (StoreId, [Name], StatusId, IsDeleted)
);
go
-- Атрибут продукта
create table dbo.ProductAttribute
(
    Id         int           not null identity(1, 1) constraint PK_ProductAttribute_Id primary key,
    StoreId    int           not null constraint FK_ProductAttribute_StoreId foreign key (StoreId) references dbo.Store (Id),
    [Key]      varchar(256)  not null,
    [Name]     varchar(1024) not null,
    IsDeleted  bit           not null,
    CreateDate datetime2(7)  not null,
    ModifyDate datetime2(7)  null,
    constraint UQ_ProductAttribute_StoreId_Key unique (StoreId, [Key]),
	index IX_ProductAttribute_StoreId_Key_Name_IsDeleted nonclustered (StoreId, [Key], [Name], IsDeleted)
);
go
-- Cвязь продукта и атрибута продукта
create table dbo.ProductAttributeLink
(
    Id          int          not null identity(1, 1) constraint PK_ProductAttributeLink_Id primary key,
    StoreId     int          not null constraint FK_ProductAttributeLink_StoreId foreign key (StoreId) references dbo.Store (Id),
    ProductId   int          not null constraint FK_ProductAttributeLink_ProductId foreign key (ProductId) references dbo.Product (Id),
    AttributeId int          not null constraint FK_ProductAttributeLink_AttributeId foreign key (AttributeId) references dbo.ProductAttribute (Id),
    [Value]     varchar(max) null,
    IsDeleted   bit          not null,
    CreateDate  datetime2(7) not null,
    ModifyDate  datetime2(7) null,
    constraint UQ_ProductAttributeLink_StoreId_ProductId_AttributeId unique (StoreId, ProductId, AttributeId),
	index IX_ProductAttributeLink_StoreId_ProductId_AttributeId_IsDeleted nonclustered (StoreId, ProductId, AttributeId, IsDeleted)
);
go

-- Ключ изображения продукта
create table dbo.ProductImageKey
(
    Id         int           not null identity(1, 1) constraint PK_ProductImageKey_Id primary key,
    StoreId    int           not null constraint FK_ProductImageKey_StoreId foreign key (StoreId) references dbo.Store (Id),
    [Key]      varchar(256)  not null,
    [Name]     varchar(1024) not null,
    IsDeleted  bit           not null,
    CreateDate datetime2(7)  not null,
    ModifyDate datetime2(7)  null,
    constraint UQ_ProductImageKey_StoreId_Key unique (StoreId, [Key]),
	index IX_ProductImageKey_StoreId_Key_Name_IsDeleted nonclustered (StoreId, [Key], [Name], IsDeleted)
);
go

-- Cвязь продукта и ключа изображения продукта
create table dbo.ProductImageKeyLink
(
    Id          int            not null identity(1, 1) constraint PK_ProductImageKeyLink_Id primary key,
    StoreId     int            not null constraint FK_ProductImageKeyLink_StoreId foreign key (StoreId) references dbo.Store (Id),
    ProductId   int            not null constraint FK_ProductImageKeyLink_ProductId foreign key (ProductId) references dbo.Product (Id),
    KeyId		int            not null constraint FK_ProductImageKeyLink_KeyId foreign key (KeyId) references dbo.ProductImageKey (Id),
    [Value]     varbinary(max) null,
    IsDeleted   bit            not null,
    CreateDate  datetime2(7)   not null,
    ModifyDate  datetime2(7)   null,
    constraint UQ_ProductImageKeyLink_StoreId_ProductId_AttributeId unique (StoreId, ProductId, KeyId),
	index IX_ProductImageKeyLink_StoreId_ProductId_KeyId_IsDeleted nonclustered (StoreId, ProductId, KeyId, IsDeleted)
);
go

/*** Клиенты ***/
-- Клиент
create table dbo.Client
(
    Id         int          not null identity(1, 1) constraint PK_Client_Id primary key,
    StoreId    int          not null constraint FK_Client_StoreId foreign key (StoreId) references dbo.Store (Id),
    [Name]     varchar(256) not null,
    IsDeleted  bit          not null,
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null,
	index IX_Client_StoreId_Name_IsDeleted nonclustered (StoreId, [Name], IsDeleted)
);
go
-- Атрибут клиента
create table dbo.ClientAttribute
(
    Id         int           not null identity(1, 1) constraint PK_ClientAttribute_Id primary key,
    StoreId    int           not null constraint FK_ClientAttribute_StoreId foreign key (StoreId) references dbo.Store (Id),
    [Key]      varchar(256)  not null,
    [Name]     varchar(1024) not null,
    IsDeleted  bit           not null,
    CreateDate datetime2(7)  not null,
    ModifyDate datetime2(7)  null,
    constraint UQ_ClientAttribute_StoreId_Key unique (StoreId, [Key]),
	index IX_ClientAttribute_StoreId_Key_Name_IsDeleted nonclustered (StoreId, [Key], [Name], IsDeleted)
);
go
-- Cвязь клиента и атрибута клиента
create table dbo.ClientAttributeLink
(
    Id          int          not null identity(1, 1) constraint PK_ClientAttributeLink_Id primary key,
    StoreId     int          not null constraint FK_ClientAttributeLink_StoreId foreign key (StoreId) references dbo.Store (Id),
    ClientId    int          not null constraint FK_ClientAttributeLink_ProductId foreign key (ClientId) references dbo.Client (Id),
    AttributeId int          not null constraint FK_ClientAttributeLink_AttributeId foreign key (AttributeId) references dbo.ClientAttribute (Id),
    [Value]     varchar(max) null,
    IsDeleted   bit          not null,
    CreateDate  datetime2(7) not null,
    ModifyDate  datetime2(7) null,
    constraint UQ_ClientAttributeLink_StoreId_ClientId_AttributeId unique (StoreId, ClientId, AttributeId),
	index IX_ClientAttributeLink_StoreId_ClientId_AttributeId_IsDeleted nonclustered (StoreId, ClientId, AttributeId, IsDeleted)
);
go

/*** Заказы ***/
-- Источник заказа
create table dbo.OrderSource
(
    Id         int          not null identity(1, 1) constraint PK_OrderSource_Id primary key,
    StoreId    int          not null constraint FK_OrderSource_StoreId foreign key (StoreId) references dbo.Store (Id),
    [Name]     varchar(256) not null,
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null,
	constraint UQ_OrderSource_StoreId_Name unique (StoreId, [Name]),
	index IX_OrderSource_StoreId_Name nonclustered (StoreId, [Name])

);
-- Статус заказа
create table dbo.OrderStatus
(
    Id         int          not null identity(1, 1) constraint PK_OrderStatus_Id primary key,
    StoreId    int          not null constraint FK_OrderStatus_StoreId foreign key (StoreId) references dbo.Store (Id),
    [Name]     varchar(256) not null,
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null,
	constraint UQ_OrderStatus_StoreId_Name unique (StoreId, [Name]),
	index IX_OrderStatus_StoreId_Name nonclustered (StoreId, [Name])
);
-- Заказ
create table dbo.[Order]
(
    Id          int            not null identity(1, 1) constraint PK_Order_Id primary key,
    StoreId     int            not null constraint FK_Order_StoreId foreign key (StoreId) references dbo.Store (Id),
    ClientId    int            not null constraint FK_Order_ClientId foreign key (ClientId) references dbo.Client (Id),
    SourceId    int            not null constraint FK_Order_SourceId foreign key (SourceId) references dbo.OrderSource (Id),
    StatusId    int            not null constraint FK_Order_StatusId foreign key (StatusId) references dbo.OrderStatus (Id),
    TotalSum    decimal(18, 2) null,
    DiscountSum decimal(18, 2) null,
    IsDeleted   bit            not null,
    CreateDate  datetime2(7)   not null,
    ModifyDate  datetime2(7)   null,
	index IX_Order_StoreId_ClientId_SourceId_StatusId_IsDeleted nonclustered (StoreId, ClientId, SourceId, StatusId, IsDeleted)
);
go
-- Атрибут заказа
create table dbo.OrderAttribute
(
    Id         int           not null identity(1, 1) constraint PK_OrderAttribute_Id primary key,
    StoreId    int           not null constraint FK_OrderAttribute_StoreId foreign key (StoreId) references dbo.Store (Id),
    [Key]      varchar(256)  not null,
    [Name]     varchar(1024) not null,
    IsDeleted  bit           not null,
    CreateDate datetime2(7)  not null,
    ModifyDate datetime2(7)  null,
    constraint UQ_OrderAttribute_StoreId_Key unique (StoreId, [Key]),
	index IX_OrderAttribute_StoreId_Key_Name_IsDeleted nonclustered (StoreId, [Key], [Name], IsDeleted)
);
go
-- Cвязь заказа и атрибута заказа
create table dbo.OrderAttributeLink
(
    Id          int          not null identity(1, 1) constraint PK_OrderAttributeLink_Id primary key,
    StoreId     int          not null constraint FK_OrderAttributeLink_StoreId foreign key (StoreId) references dbo.Store (Id),
    OrderId     int          not null constraint FK_OrderAttributeLink_OrderId foreign key (OrderId) references dbo.[Order] (Id),
    AttributeId int          not null constraint FK_OrderAttributeLink_AttributeId foreign key (AttributeId) references dbo.OrderAttribute (Id),
    [Value]     varchar(max) null,
    IsDeleted   bit          not null,
    CreateDate  datetime2(7) not null,
    ModifyDate  datetime2(7) null,
    constraint UQ_OrderAttributeLink_StoreId_OrderId_AttributeId unique (StoreId, OrderId, AttributeId),
	index IX_OrderAttributeLink_StoreId_OrderId_AttributeId_IsDeleted nonclustered (StoreId, OrderId, AttributeId, IsDeleted)
);
go
-- Позиция заказа
create table dbo.OrderItem
(
    Id         int            not null identity(1, 1) constraint PK_OrderItem_Id primary key,
    StoreId    int            not null constraint FK_OrderItem_StoreId foreign key (StoreId) references dbo.Store (Id),
    OrderId    int            not null constraint FK_OrderItem_OrderId foreign key (OrderId) references dbo.[Order] (Id),
    ProductId  int            not null constraint FK_OrderItem_ProductId foreign key (ProductId) references dbo.Product (Id),
    Price      decimal(18, 2) not null,
    [Count]    int            not null,
    IsDeleted  bit            not null,
    CreateDate datetime2(7)   not null,
    ModifyDate datetime2(7)   null,
	constraint UQ_OrderItem_StoreId_OrderId_ProductId unique (StoreId, OrderId, ProductId),
	index IX_OrderItem_StoreId_OrderId_ProductId_IsDeleted nonclustered (StoreId, OrderId, ProductId, IsDeleted)
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

insert dbo.UserPermission (StoreId, UserId, Permission, CreateDate, ModifyDate)
	values (null, 1, 1, getdate(), null);
go