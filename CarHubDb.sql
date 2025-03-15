CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY, 
    Name NVARCHAR(100) NOT NULL,  
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

CREATE TABLE Cars (
    Id INT PRIMARY KEY IDENTITY,
    Brand NVARCHAR(255) NOT NULL,
    Model NVARCHAR(255) NOT NULL,
    Year INT NOT NULL,
    Price INT NOT NULL,
    Fuel INT NOT NULL,  
    Transmission INT NOT NULL,  
    Miles FLOAT NOT NULL,
    Body INT NOT NULL,  
    Color NVARCHAR(255) NOT NULL,
    VIN NVARCHAR(255) NOT NULL,
    Text NVARCHAR(MAX) NOT NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,
    CreatedDate DATETIME NOT NULL,
    CreatedBy INT NULL,
    UpdatedDate DATETIME NULL,
    UpdatedBy INT NULL,
    DeletedDate DATETIME NULL,
    DeletedBy INT NULL
);


CREATE TABLE UserFavorites (
    UserId INT,  
    CarId INT,  
    PRIMARY KEY (UserId, CarId),
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (CarId) REFERENCES Cars(Id)
);

CREATE TABLE UserCars (
    UserId INT NOT NULL,  
    CarId INT NOT NULL,   
    PRIMARY KEY (UserId, CarId),  
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (CarId) REFERENCES Cars(Id)
);


