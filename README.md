# DotNetTrainingBatch5

## C# .NET

### Languages and Frameworks

- **C# Language**
- **.NET Framework** 
  - Versions: 1, 2, 3, 3.5, 4, 4.5, 4.6, 4.7, 4.8 (Windows)
- **.NET Core**
  - Versions: 1, 2, 2.2, 3, 3.1 (VS2019, VS2022 - Windows, Linux, macOS)
- **.NET**
  - Versions: 5 (VS2019), 6 (VS2022), 7, 8 (Windows, Linux, macOS)

### Development Tools

- **Visual Studio Code**
- **Visual Studio 2022**

### Application Types

- **Console App**
- **Windows Forms**
- **ASP.NET Core Web API**
- **ASP.NET Core Web MVC**
- **Blazor WebAssembly**
- **Blazor Server**

## Project Structure

### UI + Business Logic + Data Access => Database

## Example Projects

### Kpay

#### Features:

- **Mobile No** => Transfer
- **Mobile No Check**
- **SLH** => **Collin**
  - **10000 => 0**
  - **-5000 => +5000**
  - **Bank +5000**

### SQL Scripts

```sql
SELECT [BlogId], [BlogTitle], [BlogAuthor], [BlogContent]
FROM [dbo].[Tbl_Blog]

GO

SELECT * FROM Tbl_Blog WHERE DeleteFlag = 0

UPDATE Tbl_Blog SET BlogTitle = 'heehee2' WHERE BlogId = 1
UPDATE Tbl_Blog SET DeleteFlag = 1 WHERE BlogId = 2

DELETE FROM Tbl_Blog WHERE BlogId = 1

SELECT * FROM tbl_blog WITH (NOLOCK)
```

### Oracle Commands

```sql

-- Commit data
INSERT INTO ...
COMMIT

-- Update data
UPDATE tbl_blog
COMMIT
```

## Entity Framework Core (EF Core)

- **Database First** (Manual, Auto)
- **Code First**

```sh
dotnet ef dbcontext scaffold "Server=.;Database=DotNetTrainingBatch5;User Id=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext -f
```

## API Development

- **HttpMethod**
- **HttpStatusCode**
- **Request / Response**

## Training Schedule (5 weeks)

1. **Visual Studio 2022 Installation**
2. **Microsoft SQL Server 2022**
3. **SSMS (SQL Server Management System)**

### Topics Covered:

- **C# Basic**
- **SQL Basic**
- **Console App** (Create Project)
- **DTO (Data Transfer Object)**
- **Nuget Package**
- **ADO.NET**
- **Dapper**
  - ORM
  - Data Model
- **EFCore**
  - AppDbContext
  - Database First
  - AsNoTracking
- **REST API (ASP.NET Core Web API)**
  - Swagger
  - Postman
  - Http Method
  - Http Status Code

## Backend API Development

- **Data Model (Data Access, Database) 10 columns**
- **View Model (Frontend Return Data) 2 columns**

## Further Topics

- **MSSQL Basic**
- **C# Basic**
- **Console App**
- **ADO.NET**
- **Dapper**
- **EFCore**
- **EFCore Database First**
- **Northwind Database**
- **ASP.NET Core Web API**
- **Minimal API** / [ADO.NET / Dapper => Custom Service]

### Supported .NET Versions

- **.NET 6**
- **.NET 7**
- **.NET Core 3.1**

## File Operations

### File.json

- Read File.json => Convert to Object [] => Insert => Json => Write

## Kpay Application

### Features

- **Mobile No** => Transfer
- **Id, FullName, MobileNo, Balance, Pin (000000)**
- **Bank** => Deposit / Withdraw

#### API Endpoints

1. **Deposit**
   - **Deposit API** => MobileNo, Balance (+) => 1000 + (-1000)

2. **Withdraw**
   - **Withdraw API** => MobileNo, Balance (-) => 1000 - (-1000)
   - Minimum balance 10,000 MMK

3. **Transfer**
   - **Transfer API**
     - FromMobileNo, ToMobileNo, Amount, Pin, Notes
     - FromMobile check
     - ToMobileNo check
     - FromMobileNo != ToMobileNo
     - Pin == 
     - Balance
     - FromMobileNo Balance (-)
     - ToMobileNo Balance (+)
     - Message (Complete)
     - Transaction History

4. **Balance**

### User Management

- **Create Wallet User**
- **Login**
- **Change Pin**
- **Phone No Change**
- **Forget Password**
- **Reset Password**
- **First Time Login**
