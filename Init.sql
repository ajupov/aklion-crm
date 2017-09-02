create database aklion_crm
collate Cyrillic_General_CI_AS;
go

use aklion_crm;
go

-- Любой пользователь, котрый может войти в систему: админ, владелец фирмы, менеджер, оператор, курьер и т.д.
create table dbo.[User]
(
    Id               int           not null identity(1, 1) constraint PK_User_Id primary key,
    [Login]          varchar(256)  not null constraint UQ_User_Login unique,
    PasswordHash     varchar(512)  not null,
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

create table dbo.Store
(
    Id         int          not null identity(1, 1) constraint PK_Store_Id primary key,
    [Name]     varchar(256) not null constraint UQ_Store_Name unique,
    ApiKey     varchar(32)  null constraint UQ_Store_ApiKey unique,
    ApiSecret  varchar(128) null,
    IsLocked   bit          not null,
    IsDeleted  bit          not null,
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null
);
go

-- Должность
create table dbo.Post
(
    Id         int          not null identity(1, 1) constraint PK_Post_Id primary key,
    StoreId    int          not null constraint FK_Post_Store_Id foreign key (StoreId) references dbo.Store (Id),
    [Name]     varchar(256) not null,
    IsDeleted  bit          not null,
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null,
    constraint UQ_Post_StoreId_Name unique (StoreId, [Name])
);
go

-- Должность пользователя внутри магазина, например админ, владелец фирмы, менеджер, оператор, курьер и т.д.
create table dbo.UserPost
(
    Id         int          not null identity(1, 1) constraint PK_UserPost_Id primary key,
    UserId     int          not null constraint FK_UserPost_User_Id foreign key (UserId) references dbo.[User] (Id),
    StoreId    int          not null constraint FK_UserPost_Store_Id foreign key (StoreId) references dbo.Store (Id),
    PostId     int          not null constraint FK_UserPost_Post_Id foreign key (PostId) references dbo.Post (Id),
    IsDeleted  bit          not null,
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null,
    constraint UQ_UserPost_UserId_StoreId_PostId unique (UserId, StoreId, PostId)
);
go

-- Права пользователей внутри магазина. Права берутся из Enum.
create table dbo.UserRule
(
    Id         int          not null identity(1, 1) constraint PK_UserRule_Id primary key,
    UserId     int          not null constraint FK_UserRule_User_Id foreign key (UserId) references dbo.[User] (Id),
    StoreId    int          not null constraint FK_UserRule_Store_Id foreign key (StoreId) references dbo.Store (Id),
    [Rule]     tinyint      not null,
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null,
    constraint UQ_UserRule_UserId_StoreId_Rule unique (UserId, StoreId, [Rule])
);
go

-- Токены при смене телефона, email, пароля и т.д.
create table dbo.UserToken
(
    Id             int          not null identity(1, 1) constraint PK_UserToken_Id primary key,
    UserId         int          not null constraint FK_UserToken_User_Id foreign key (UserId) references dbo.[User] (Id),
    TokenType      tinyint      null,
    Token          varchar(256) not null,
    ExpirationDate datetime2(7) not null,
    IsUsed         bit          not null,
    CreateDate     datetime2(7) not null,
    ModifyDate     datetime2(7) null
);
go

-- Категории продуктов
create table dbo.Category
(
    Id         int          not null identity(1, 1) constraint PK_Category_Id primary key,
    StoreId    int          not null constraint FK_Category_Store_Id foreign key (StoreId) references dbo.Store (Id),
    [Name]     varchar(256) not null,
    IsDeleted  bit          not null,
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null,
    constraint UQ_Category_StoreId_Name unique (StoreId, [Name])
);
go

-- Продукт (товар или услуга)
create table dbo.Product
(
    Id            int            not null identity(1, 1) constraint PK_Product_Id primary key,
    StoreId       int            not null constraint FK_Product_Store_Id foreign key (StoreId) references dbo.Store (Id),
    [Type]        tinyint        not null, -- Enum (Товар, услуга)
    [Name]        varchar(256)   not null,
    [Description] varchar(4000)  null,
    Price         decimal(18, 2) null,
    [Status]      tinyint        not null, -- Enum (Отсутствует, имеется в наличии, имеется на складе)
    VendorCode    varchar(16)    null,     -- Артикул
    ParrentId     int            null,     -- Родитель, для вариативных
    IsDeleted     bit            not null,
    CreateDate    datetime2(7)   not null,
    ModifyDate    datetime2(7)   null,
    constraint UQ_Product_StoreId_Name_VendorCode unique (StoreId, [Name], VendorCode)
);
go

-- Атрибут
create table dbo.Attribute
(
    Id         int          not null identity(1, 1) constraint PK_Attribute_Id primary key,
    StoreId    int          not null constraint FK_Attribute_Store_Id foreign key (StoreId) references dbo.Store (Id),
    [Name]     varchar(256) not null,
    IsDeleted  bit          not null,
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null,
    constraint UQ_Attribute_StoreId_Name unique (StoreId, [Name])
);
go

-- Атрибут продукта
create table dbo.ProductAttribute
(
    Id          int           not null identity(1, 1) constraint PK_ProductAttribute_Id primary key,
    StoreId     int           not null constraint FK_ProductAttribute_Store_Id foreign key (StoreId) references dbo.Store (Id),
    ProductId   int           not null constraint FK_ProductAttribute_Product_Id foreign key (ProductId) references dbo.Product (Id),
    AttributeId int           not null constraint FK_ProductAttribute_Attribute_Id foreign key (AttributeId) references dbo.Attribute (Id),
    [Value]     varchar(4000) not null,
    CreateDate  datetime2(7)  not null,
    ModifyDate  datetime2(7)  null,
    constraint UQ_ProductAttribute_StoreId_ProductId_AttributeId_Value unique (StoreId, ProductId, AttributeId, [Value])
);
go

-- Тэг
create table dbo.Tag
(
    Id         int          not null identity(1, 1) constraint PK_Tag_Id primary key,
    StoreId    int          not null constraint FK_Tag_Store_Id foreign key (StoreId) references dbo.Store (Id),
    [Name]     varchar(256) not null,
    IsDeleted  bit          not null,
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null,
    constraint UQ_Tag_StoreId_Name unique (StoreId, [Name])
);
go

-- Тэг продукта
create table dbo.ProductTag
(
    Id         int          not null identity(1, 1) constraint PK_ProductTag_Id primary key,
    StoreId    int          not null constraint FK_ProductTag_Store_Id foreign key (StoreId) references dbo.Store (Id),
    ProductId  int          not null constraint FK_ProductTag_Product_Id foreign key (ProductId) references dbo.Product (Id),
    TagId      int          not null constraint FK_ProductTag_Tag_Id foreign key (TagId) references dbo.Tag (Id),
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null,
    constraint UQ_ProductTag_StoreId_ProductId_TagId unique (StoreId, ProductId, TagId)
);
go

-- Клиент
create table dbo.Client
(
    Id         int          not null identity(1, 1) constraint PK_Client_Id primary key,
    StoreId    int          not null constraint FK_Client_Store_Id foreign key (StoreId) references dbo.Store (Id),
    Email      varchar(256) null,
    Surname    varchar(256) null,
    [Name]     varchar(256) not null,
    Patronymic varchar(256) null,
    Gender     tinyint      null,
    BirthDate  date         null,
    IsDeleted  bit          not null,
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null,
    constraint UQ_Client_StoreId_Email unique (StoreId, Email)
);
go

-- Номера телефонов клента
create table dbo.ClientPhone
(
    Id         int          not null identity(1, 1) constraint PK_ClientPhone_Id primary key,
    StoreId    int          not null constraint FK_ClientPhone_Store_Id foreign key (StoreId) references dbo.Store (Id),
    ClientId   int          not null constraint FK_ClientPhone_Client_Id foreign key (ClientId) references dbo.Client (Id),
    [Value]    varchar(10)  not null,
    IsDeleted  bit          not null,
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null,
    constraint UQ_ClientPhone_StoreId_ClientId_Value unique (StoreId, ClientId, [Value])
);
go

-- Адреса клента
create table dbo.ClientAddress
(
    Id         int          not null identity(1, 1) constraint PK_ClientAddress_Id primary key,
    StoreId    int          not null constraint FK_ClientAddress_Store_Id foreign key (StoreId) references dbo.Store (Id),
    ClientId   int          not null constraint FK_ClientAddress_Client_Id foreign key (ClientId) references dbo.Client (Id),
    Country    varchar(256) null,
    [State]    varchar(256) null,
    City       varchar(256) not null,
    Street     varchar(256) null,
    House      varchar(8)   null,
    Entrance   varchar(8)   null,
    [Floor]    varchar(8)   null,
    Apartment  varchar(8)   null,
	PostCode   varchar(6)   null,
    IsDeleted  bit          not null,
    CreateDate datetime2(7) not null,
    ModifyDate datetime2(7) null,
    constraint UQ_ClientAddress_StoreId_ClientId_Country_State_City_Street_House_Entrance_Floor_Apartment unique
		(StoreId, ClientId, Country, [State], City, Street, House, Entrance, [Floor], Apartment)
);
go

-- Заказ
create table dbo.[Order]
(
    Id           int            not null identity(1, 1) constraint PK_Order_Id primary key,
    StoreId      int            not null constraint FK_Order_Store_Id foreign key (StoreId) references dbo.Store (Id),
    ClientId     int            not null constraint FK_Order_Client_Id foreign key (ClientId) references dbo.Client (Id),
    CreateUserId int            null constraint FK_Order_User_Id foreign key (CreateUserId) references dbo.[User] (Id),
    [Type]       tinyint        not null, -- Enum (Из CRM, из сайта)
    TotalSum     decimal(18, 2) null,
    DiscountSum  decimal(18, 2) null,
    [Status]     tinyint        not null, -- Enum (В очереди, в обработке, выполнен, возвращен)
    ClientNote   varchar(4000)  null,
    UserNote     varchar(4000)  null,
    IsDeleted    bit            not null,
    CreateDate   datetime2(7)   not null,
    ModifyDate   datetime2(7)   null,
);
go

-- Позиция заказа
create table dbo.OrderItem
(
    Id         int            not null identity(1, 1) constraint PK_OrderItem_Id primary key,
    StoreId    int            not null constraint FK_OrderItem_Store_Id foreign key (StoreId) references dbo.Store (Id),
    OrderId    int            not null constraint FK_OrderItem_Order_Id foreign key (OrderId) references dbo.[Order] (Id),
    ProductId  int            not null constraint FK_OrderItem_Product_Id foreign key (ProductId) references dbo.Product (Id),
    [Sum]      decimal(18, 2) null,
    [Count]    int            not null,
    IsDeleted  bit            not null,
    CreateDate datetime2(7)   not null,
    ModifyDate datetime2(7)   null
);
go