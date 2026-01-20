# Library Management Systme

A Simple Library Management System REST API Application

## Developed With

- .NET 8
- Entity Framework
- Microsoft SqlServer

## Routes

|       Routes                         |          Description                    | 
| ------------------------------------ | ----------------------------------------|
| [POST] /api/auth/signup              | Sign up a new user                      |
| [POST] /api/auth/login               | Login a user                            |
| [POST] /api/auth/refreshtoken        | Refresh user access token               |
| [POST] /api/books                    | Add a book                              |
| [PUT] /api/books/{id}                | Update a book                           |
| [GET] /api/books?page=1&pageSize=10  | Get all books  (includes filter params) |
| [DELETE] /api/books/{id}             | Delete a book by id                     |

## Getting Started

# 1. Clone The Repository 
git clone <repository-url>

# 2. Restore Nuget Packages
dotnet restore

# 3. Run The Application
dotnet run

# 4. Swagger UI
https://localhost:5XXX/swagger (your local host url)

# 5. Authorization
Use the Signup or login endpoints to recieve access token in order to access all books endpoint

