# Dashboard: Organizational Management Hub

[//]: # (Introduction)
**Dashboard** is a robust MVC web application architected for optimal management of an organization's core functions, providing a central hub for overseeing departments, employees, user roles, and access controls, fortified by a secure authentication system.

[//]: # (Key Features & Architecture)
## Key Features & Architecture
- **CRUD Operations:** Intuitive interfaces for creating, reading, updating, and deleting information.
- **3-Tier Architecture:** Ensures system maintainability and scalability.
- **Unit of Work & Repository Patterns:** Maintain consistency and integrity across transactions.
- **Advanced Security:** Utilizes Microsoft Identity for authentication and authorization.
- **Data Validation:** Enforces data integrity with client and server-side checks.
- **AutoMapper:** Simplifies object-to-object mapping, reducing code redundancy.
- **File Management:** Offers file upload and management capabilities.

[//]: # (Technologies Used)
## Technologies
### Frontend
- HTML, CSS, Bootstrap, jQuery
### Backend
- ASP.NET MVC Core, ASP.NET Identity, LINQ, EF Core

[//]: # (Getting Started with Installation)
## Getting Started

To get a local copy up and running, follow these simple steps.

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download) installed on your machine
- An SQL Server instance accessible for the application to connect to

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/mostafabadrshaalan/Dashboard.git

Setup
1-Database Migration:
To set up the database with the necessary tables, run the following command:
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```
2-User Registration:
When registering a new user account, ensure that the password meets the following criteria:

At least one digit (0-9)
At least one uppercase character (A-Z)
At least one lowercase character (a-z)
At least one non-alphanumeric character (!@#$%^&* etc.)
