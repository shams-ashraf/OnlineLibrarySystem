Online Library System
A desktop application built with C# and Windows Forms for managing an online library system. The application integrates with SQL Server to provide functionalities such as user authentication, book management, publisher and author tracking, and report generation. It supports both admin and regular user roles with CRUD (Create, Read, Update, Delete) operations.

Features

User Authentication: Sign-up and login functionality with admin and regular user roles.
Book Management: Add, update, delete, and search for books by ISBN, name, publisher, or author.
Publisher and Author Management: Manage publisher and author details associated with books.
Report Generation: Create, update, and delete reports (admin-only feature).
Account Management: Update or delete user accounts.
Search Functionality: Search for books and reports by various criteria.
Responsive UI: User-friendly interface built with Windows Forms.

Technologies Used

C#: Programming language for the application logic.
Windows Forms: For building the desktop GUI.
SQL Server: Database for storing books, users, authors, publishers, and reports.
ADO.NET: For database connectivity and operations.

Prerequisites
To run this project, you need the following installed:

Visual Studio (2019 or later) with .NET Framework (4.7.2 or higher).
SQL Server (Express or higher) for the database.
SQL Server Management Studio (SSMS) (optional, for database management).

Setup Instructions

Clone the Repository:git clone https://github.com/your-username/OnlineLibrarySystem.git


Database Setup:
Create a database named Online Library in SQL Server.
Execute the following SQL script to create the necessary tables:CREATE TABLE Users (
    UserID NVARCHAR(50) PRIMARY KEY,
    fName NVARCHAR(50),
    lName NVARCHAR(50),
    Email NVARCHAR(100),
    Password NVARCHAR(50),
    Admin BIT
);

CREATE TABLE Publishers (
    PublisherID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100),
    BookID NVARCHAR(50),
    construction_date DATE
);

CREATE TABLE Authors (
    AuthorID INT PRIMARY KEY IDENTITY(1,1),
   26         Name NVARCHAR(100),
    BookID NVARCHAR(50),
    Birthday DATE
);

CREATE TABLE Books (
    ISBN NVARCHAR(50) PRIMARY KEY,
    BookName NVARCHAR(100),
    PublicationYear INT,
    PublisherID INT,
    Category NVARCHAR(50),
    FOREIGN KEY (PublisherID) REFERENCES Publishers(PublisherID)
);

CREATE TABLE BookAuthors (
    ISBN NVARCHAR(50),
    AuthorID INT,
    PRIMARY KEY (ISBN, AuthorID),
    FOREIGN KEY (ISBN) REFERENCES Books(ISBN),
    FOREIGN KEY (AuthorID) REFERENCES Authors(AuthorID)
);

CREATE TABLE Report (
    ReportID INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(100),
    Publication_date DATE,
    Description NVARCHAR(500),
    UserID NVARCHAR(50),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);


Update the connection string in DBAccess.cs if your SQL Server instance differs:private static string strConnString = "Data Source=(local);Initial Catalog=Online Library;Integrated Security=True";


Open the Project:
Open the .sln file in Visual Studio.
Build and run the project.


Login Credentials:
Use an existing UserID and Password from the Users table or create a new account via the Sign-Up page.



Database Schema
The database consists of the following tables:

Users: Stores user information (UserID, fName, lName, Email, Password, Admin).
Books: Stores book details (ISBN, BookName, PublicationYear, PublisherID, Category).
Publishers: Stores publisher information (PublisherID, Name, BookID, construction_date).
Authors: Stores author information (AuthorID, Name, BookID, Birthday).
BookAuthors: Junction table linking Books and Authors (ISBN, AuthorID).
Report: Stores report details (ReportID, Title, Publication_date, Description, UserID).

Project Structure

DBAccess.cs: Handles database connectivity and operations (connection, queries, data retrieval).
Login.cs: Manages user login and authentication.
SignUp.cs: Handles user registration (not provided but referenced in code).
EditAccount.cs: Allows users to update or delete their account details.
homeadmin.cs: Admin dashboard for managing books, authors, and publishers.
Report.cs: Interface for creating, updating, and deleting reports.
Program.cs: Entry point for the application.

How to Use

Login:
Enter a valid UserID and Password to access the system.
Admins have additional privileges (e.g., add, update, delete books/reports).


Admin Dashboard (homeadmin):
Add new books, publishers, and authors.
Update or delete existing records.
Search for books by ISBN, name, publisher, or author.


Reports (Report):
Admins can create, update, or delete reports.
Search reports by title.


Account Management (EditAccount):
Update user details (name, email, password).
Delete user account.
