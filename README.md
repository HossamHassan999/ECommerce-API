# 🛒 ECommerce-API

A clean, scalable E-Commerce Web API built with **ASP.NET Core**, featuring user authentication, product & category management, cart functionality, order processing, and HTML invoice emails.

---

## 🚀 Features

- 🔐 User Registration & Authentication (JWT)
- 📦 Product & Category Management
- 🛍️ Cart & Order Workflow
- 📧 HTML Invoice Emails after order creation
- 🛡️ Role-based Access (Admin / User)
- 🗃️ Entity Framework Core (SQL Server)

---

## ⚙️ Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- SQL Server (LocalDB or full)
- Gmail (App Password for SMTP) or any SMTP provider

### Setup Steps

1. **Clone the repository**

```bash
git clone https://github.com/HossamHassan999/ECommerce-API.git
cd ECommerce-API


2.Restore packages
dotnet restore

3.Configure secrets (instead of appsettings.json)
dotnet user-secrets init
dotnet user-secrets set "SmtpSettings:Host" "smtp.gmail.com"
dotnet user-secrets set "SmtpSettings:Port" "587"
dotnet user-secrets set "SmtpSettings:EnableSsl" "true"
dotnet user-secrets set "SmtpSettings:Username" "your_email@gmail.com"
dotnet user-secrets set "SmtpSettings:Password" "your_app_password"
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=.;Database=ECommerceDB;Trusted_Connection=True;TrustServerCertificate=True;"


4.Apply migrations & create database
dotnet run


🧱 Project Structure

/Controllers     → API endpoints (Products, Orders, Auth, etc.)
/Services        → Business logic (OrderService, EmailService, etc.)
/Data            → Entities & DbContext
/DTOs            → Data Transfer Objects (DTOs)
/Interfaces      → Interfaces for dependency injection
/Mappings        → AutoMapper configuration
/Middlewares     → Custom middleware (e.g., error handling, JWT validation)


📬 Invoice Email on Order
When a user places an order, they receive a professional HTML invoice email including:

Order ID & Date

Product list with quantity, unit price, and subtotal

Grand total

🔐 Authentication
JWT-based access

Role claims (User / Admin)

Passwords are hashed (e.g., BCrypt)


📡 API Endpoints Overview
Method	Endpoint	Description
POST	/api/orders/create	Create an order from user's cart
PUT	/api/orders/{id}/status	Update order status (Admin only)
GET	/api/orders/user/{userId}	Get all orders for a user
POST	/api/products	Add a new product (Admin only)
GET	/api/products	Get all products
POST	/api/auth/register	Register a new user
POST	/api/auth/login	Login and receive JWT



🧪 Testing (coming soon)
✅ Unit testing with xUnit

🧪 Mocking with Moq

🔌 Integration tests (planned)

📈 Roadmap
 PDF invoice attachment support

 Product reviews & ratings

 Stripe or PayPal payment integration

 Admin dashboard & analytics

 Swagger/OpenAPI documentation

🤝 Contributing
Contributions are welcome!
To contribute:

Fork the repo

Create a new branch (feature/your-feature)

Commit and push your changes

Open a Pull Request

📄 License
This project is licensed under the MIT License.

👨‍💻 Developed by
Hossam Hassan
