# Modern Blog - ASP.NET Core 8.0 MVC

A modern, responsive blog management web application built with **ASP.NET Core 8.0 MVC**, **Entity Framework Core (SQL Server)**, and **ASP.NET Core Identity** for authentication and role-based authorization.

---

## 🚀 Features

### 📝 Blog & Post Management
- **Explore Posts**: Browse articles with category filtering and responsive card layouts.
- **Article Details**: Rich formatted post content view with author metadata and publish dates.
- **Interactive Comments**: Real-time AJAX comment posting directly on blog articles.
- **Post CRUD (Admin Only)**:
  - Create new posts with featured image upload (`.jpg`, `.jpeg`, `.png`).
  - Edit existing posts with image replacement and file cleanup.
  - Delete posts with automatic removal of associated images from disk.

### 🔐 Authentication & Security
- **ASP.NET Core Identity**: Secure user registration, login, and session cookie management.
- **Role-Based Access Control (RBAC)**: Distinct permissions for `Admin` and `User` roles.
- **Automatic Admin Seeding**: Default administrator account provisioned automatically on application startup.
- **Access Denied Protection**: Custom redirection and error handling for unauthorized route access.

### 📊 Admin Dashboard
- **User Management**: View all registered users and their assigned roles.
- **User Creation**: Admin capability to create accounts directly with specific role assignments.
- **Analytics Overview**: Quick metrics including total user counts.

### 🎨 Modern UI & Experience
- Clean, responsive dark-themed styling with **Bootswatch** and **Bootstrap Icons**.
- Smooth transitions, glassmorphic accents, and mobile-friendly responsive navigation.

---

## 🛠️ Tech Stack

- **Framework**: [.NET 8.0](https://dotnet.microsoft.com/) (ASP.NET Core MVC)
- **ORM**: Entity Framework Core 8.0 (Code-First)
- **Database**: Microsoft SQL Server
- **Identity**: ASP.NET Core Identity
- **Frontend**: Razor Views (CSHTML), Bootstrap 5 / Bootswatch, Bootstrap Icons, jQuery
- **File Storage**: Local file system (`wwwroot/images/`)

---

## 📂 Project Structure

```plaintext
Learning ASP.NET(MVC)/
├── Controllers/
│   ├── AuthController.cs         # Login, Register, Logout, AccessDenied
│   ├── DashboardController.cs    # Admin user management & role administration
│   └── PostController.cs         # Post CRUD, category filters, comments, file uploads
├── Data/
│   └── ApplicationDbContext.cs   # EF Core DbContext with seed data
├── Helper/
│   └── RemoveHtmlTag.cs          # Utility helper for string/HTML formatting
├── Migrations/                   # EF Core Code-First database migrations
├── Models/
│   ├── Category.cs               # Category model
│   ├── Comment.cs                # Comment model
│   ├── Post.cs                   # Post model
│   └── ViewModels/               # ViewModels for Auth, Post, and Dashboard
├── Views/
│   ├── Auth/                     # Login and Register Razor views
│   ├── Dashboard/                # Admin user dashboard & creation views
│   ├── Post/                     # Post Index, Details, Create, Edit, Delete views
│   └── Shared/                   # Layout, Navbar, and shared partials
├── wwwroot/
│   ├── css/                      # Custom stylesheets (site.css)
│   ├── images/                   # Uploaded post images
│   └── lib/                      # Client-side vendor libraries (Bootstrap, jQuery)
├── appsettings.json              # Connection strings and logging settings
└── Program.cs                    # Application startup, DI, middleware & seed logic
```

---

## 📋 Prerequisites

Before running the application, ensure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or LocalDB / SQL Server Express / Docker container)
- [.NET EF Core CLI Tool](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) (optional, for migrations):
  ```bash
  dotnet tool install --global dotnet-ef
  ```

---

## ⚙️ Getting Started

### 1. Clone or Open the Repository
Open a terminal in the project root directory:
```bash
cd "d:/MIS/Learning ASP.NET(MVC)/Learning ASP.NET(MVC)"
```

### 2. Configure Database Connection
Open `appsettings.json` and adjust the connection string to match your SQL Server instance:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,;Database=yourdb;User Id=sa;Password=your_password;TrustServerCertificate=True;"
  }
}
```
> **Note**: If using SQL Server LocalDB on Windows, you can use:
> ```json
> "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=yourdb;Trusted_Connection=True;MultipleActiveResultSets=true"
> ```

### 3. Apply Migrations & Update Database
Run the following command to create the database schema and populate seed categories and posts:

```bash
dotnet ef database update
```

### 4. Run the Application
Launch the web server:

```bash
dotnet run
```

Or run with hot-reload during development:
```bash
dotnet watch run
```

Navigate to the local URL displayed in the console (typically `https://localhost:7xxx` or `http://localhost:5xxx`).

---

## 🔑 Default Administrator Account

On initial launch, `Program.cs` automatically creates the `Admin` role and an administrator account if it does not already exist:

| Role | Email | Password |
|---|---|---|
| **Admin** | `admin@gmail.com` | `123456` |

> ⚠️ **Security Notice**: Remember to update the default administrator credentials before deploying to any production environment.

---

## 📖 Usage Guide

1. **Viewing Posts**: Anyone can browse blog articles from the home page and filter by category (Technology, Healthy, Lifestyle).
2. **Reading & Commenting**: Click **Read More** on any card to view the full content. Logged-in users can post instant comments.
3. **Admin Actions**:
   - Log in with `admin@gmail.com` / `123456`.
   - Access **Dashboard** from the top navigation to inspect registered users and create new accounts.
   - Use **Create Post**, **Edit**, or **Delete** buttons directly from the post listings and detail views.

---

## 📜 License

This project is developed for educational and portfolio purposes. Feel free to modify and expand it!
