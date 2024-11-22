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

1. Create a new MSSQL database.
2. Configure the connection string in the `appsettings.json` file:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=your_server;Database=BlogsDB;Trusted_Connection=True;"
     }
   }
