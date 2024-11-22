CREATE DATABASE BlogsDB;
GO

USE Blogs;
GO

CREATE TABLE Role (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL
);

-- Create User Table
CREATE TABLE [User] (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(100) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    RoleId INT NOT NULL,
    FOREIGN KEY (RoleId) REFERENCES Role(Id)
);

-- Create Blog Table
CREATE TABLE Blog (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    Rating DECIMAL(3, 2) NULL,
    PublishDate DATETIME NOT NULL DEFAULT GETDATE(),
    UserId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES [User](Id)
);

-- Create Tag Table
CREATE TABLE Tag (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
);

-- Create BlogTag Table (Many-to-Many Relationship)
CREATE TABLE BlogTag (
    Id INT PRIMARY KEY IDENTITY(1,1),
    BlogId INT NOT NULL,
    TagId INT NOT NULL,
    FOREIGN KEY (BlogId) REFERENCES Blog(Id),
    FOREIGN KEY (TagId) REFERENCES Tag(Id)
);


INSERT INTO Role (Name) VALUES ('Admin');
INSERT INTO Role (Name) VALUES ('Editor');
INSERT INTO Role (Name) VALUES ('Viewer');

--since user relies on blog as 1 to many we cascade so when user deleted all associated blogs with him/her are deleted.
ALTER TABLE Blog
ADD CONSTRAINT FK_Blog_UserId
FOREIGN KEY (UserId) REFERENCES [User](Id)
ON DELETE CASCADE;