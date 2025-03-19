﻿CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY, 
    [Name] NVARCHAR(100) NOT NULL,  
    Surname NVARCHAR(100) NOT NULL,  
    Email NVARCHAR(255) UNIQUE NOT NULL,  
    Phone NVARCHAR(20),  
    PasswordHash NVARCHAR(255) NOT NULL,  
    UserImagePath NVARCHAR(500) NULL,
    UserRole INT NOT NULL,  
    CreatedBy INT, 
    UpdatedBy INT,  
    DeletedBy INT,  
    CreatedDate DATETIME,  
    UpdatedDate DATETIME,  
    DeletedDate DATETIME,  
    IsDeleted BIT DEFAULT 0,  
);

CREATE TABLE [dbo].[Cars] (
    [Id]             INT PRIMARY KEY            IDENTITY (1, 1) NOT NULL,
    [Brand]          NVARCHAR (255) NOT NULL,
    [BrandImagePath] NVARCHAR (300) NULL,
    [Model]          NVARCHAR (255) NOT NULL,
    [Year]           INT            NOT NULL,
    [Price]          INT            NOT NULL,
    [Fuel]           INT            NOT NULL,
    [Transmission]   INT            NOT NULL,
    [Miles]          FLOAT (53)     NOT NULL,
    [Body]           INT            NOT NULL,
    [BodyTypeImage]  NVARCHAR (300) NULL,
    [Color]          NVARCHAR (255) NOT NULL,
    [VIN]            NVARCHAR (255) NOT NULL,
    [Text]           NVARCHAR (MAX) NOT NULL,
    [IsDeleted]      BIT            DEFAULT ((0)) NOT NULL,
    [CreatedDate]    DATETIME       NOT NULL,
    [CreatedBy]      INT            NULL,
    [UpdatedDate]    DATETIME       NULL,
    [UpdatedBy]      INT            NULL,
    [DeletedDate]    DATETIME       NULL,
    [DeletedBy]      INT            NULL,
    [UserId]         INT            NULL,
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]),
);


CREATE TABLE UserFavorites (
    UserId INT,  
    CarId INT,  
    PRIMARY KEY (UserId, CarId),
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (CarId) REFERENCES Cars(Id)
);

--CREATE TABLE UserCars (
--    UserId INT NOT NULL,  
--    CarId INT NOT NULL,   
--    PRIMARY KEY (UserId, CarId),  
--    FOREIGN KEY (UserId) REFERENCES Users(Id),
--    FOREIGN KEY (CarId) REFERENCES Cars(Id)
--);


CREATE TABLE CarImages (
    Id INT IDENTITY(1,1) PRIMARY KEY, 
    CarId INT NOT NULL,
    ImagePath NVARCHAR(500) NOT NULL,
    FOREIGN KEY (CarId) REFERENCES Cars(Id)
);