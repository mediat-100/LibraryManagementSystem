# 📚 Library Management System

A RESTful API for managing library books with authentication and authorization built using .NET 8.

## ✨ Features

- **User Authentication** - Secure signup and login with JWT tokens
- **Token Refresh** - Refresh access tokens without re-authentication
- **Book Management** - Full CRUD operations for library books
- **Pagination & Filtering** - Efficient data retrieval with query parameters
- **API Documentation** - Interactive Swagger UI for testing endpoints

## 🛠️ Tech Stack

- **.NET 8** - Modern web framework
- **Entity Framework Core** - ORM for database operations
- **Microsoft SQL Server** - Relational database
- **JWT Authentication** - Secure token-based auth

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB, Express, or Full)

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd library-management-system
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Run the application**
   ```bash
   dotnet run
   ```

4. **Access Swagger UI**
   
   Navigate to `https://localhost:5XXX/swagger` (check console for actual port)

## 📡 API Endpoints

### Authentication

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| `POST` | `/api/auth/signup` | Register a new user | ❌ |
| `POST` | `/api/auth/login` | Authenticate user and receive tokens | ❌ |
| `POST` | `/api/auth/refreshtoken` | Refresh expired access token | ❌ |

### Books

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| `POST` | `/api/books` | Create a new book | ✅ |
| `GET` | `/api/books?page=1&pageSize=10` | Retrieve books (with pagination & filters) | ✅ |
| `GET` | `/api/books/{id}` | Get a specific book by ID | ✅ |
| `PUT` | `/api/books/{id}` | Update an existing book | ✅ |
| `DELETE` | `/api/books/{id}` | Delete a book | ✅ |

### Query Parameters (GET /api/books)

- `page` - Page number (default: 1)
- `pageSize` - Items per page (default: 10)
- Additional filter parameters as defined in your API

## 🔐 Authentication

This API uses JWT Bearer tokens for authentication.

1. **Sign up** or **login** to receive an access token
2. Include the token in subsequent requests:
   ```
   Authorization: Bearer <your-access-token>
   ```
3. When the token expires, use the `/api/auth/refreshtoken` endpoint to get a new one

### Using Swagger UI

1. Click the **Authorize** button (🔒) at the top of Swagger UI
2. Enter: `<your-access-token>`
3. Click **Authorize** to apply to all requests

## 🧪 Testing

Use the Swagger UI to test all endpoints interactively, or import the API into tools like:
- [Postman](https://www.postman.com/)

## 📝 License

This project is licensed under the MIT License.


## 👤 Author

Mediat Tomiwa Yusuff

---
