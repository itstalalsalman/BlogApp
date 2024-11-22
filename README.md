# MyBlogSpace

**MyBlogSpace** is a web application built using ASP.NET Core MVC, designed to manage Blogs, Categories, Users, Roles, and Tags. This project showcases the use of database-first approach, CRUD operations, and the MVC architecture in the .NET ecosystem, with a focus on implementing services, models, and views for an enhanced user experience.

---

## Project Overview

The application allows users to:

- Create, read, update, and delete blogs.
- Manage users, roles, categories, and tags.
- Interact with a fully implemented database using SQL Server and Entity Framework Core.

---

## Features

### 1. **Database Design**
- **Database-First Approach:** 
  - The project uses a database-first approach with **SQL Server** (MSSQL) to design the database and generate entities using **Entity Framework Core** (EF Core).
  - Entities were generated in the **Models** folder after establishing a connection with the MSSQL database using VS Code.

### 2. **One-to-Many Relationships**
- **User and Blog Entities:** 
  - A one-to-many relationship exists between **User** and **Blog** entities. Each user can have multiple blogs, while each blog belongs to a single user.
  
### 3. **CRUD Operations**
- Full **Create, Read, Update, and Delete (CRUD)** operations are implemented for the **User** and **Blog** entities.
  
### 4. **MVC Architecture**
- The application follows the **Model-View-Controller (MVC)** pattern, where:
  - **Controllers** handle the user input and interact with the models.
  - **Views** are created from the scaffolding of the models and later styled for better user interaction.
  
### 5. **Entity Framework Core & Services**
- EF Core is used to generate models and perform CRUD operations with SQL Server.
- The business logic and data access layer are separated into service classes that interact with the database via the DbContext.

### 6. **Scaffolding & Views**
- Views are scaffolded automatically from the models and controllers.
- After scaffolding, views are customized and styled according to project requirements.

---

## Database Setup and Queries

### 1. **Database Creation**
To set up the database, follow these steps:
```
CREATE DATABASE Blogs;
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
```

1. Create a new MSSQL database.
2. Configure the connection string in the `appsettings.json` file:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=your_server;Database=BlogsDB;Trusted_Connection=True;"
     }
   }
