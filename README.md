# Inventory Manager

A comprehensive inventory management system designed to help businesses efficiently track, manage, and optimize their inventory operations. This application provides intuitive tools for managing products, monitoring stock levels, and generating insightful reports.

## 🌐 Live Application

**🚀 [View Live Application](https://inventory-manager-pluska-1755210558.azurewebsites.net)**

## 🚀 Features

### Core Functionality

- **Product Management**: Add, edit, delete, and categorize products with detailed information
- **Supplier Management**: Manage supplier relationships and contact information
- **Stock Movements**: Track inventory changes with detailed audit trails
- **Reports & Analytics**: Generate insights on low stock, top products, and inventory value
- **Search & Filter**: Advanced search capabilities across all inventory items
- **User Authentication**: Secure login system with role-based access control

### Technical Features

- **Blazor Server**: Modern web application built with .NET 9 and Blazor Server
- **Azure SQL Database**: Scalable cloud database for production use
- **Entity Framework Core**: Robust ORM with optimized DbContext management
- **Responsive Design**: Mobile-friendly UI with Bootstrap 5
- **Azure App Service**: Hosted on Microsoft Azure for reliability and scalability

## 🛠️ Technology Stack

- **Backend**: .NET 9, Blazor Server, Entity Framework Core
- **Database**: Azure SQL Database
- **Frontend**: HTML5, CSS3, JavaScript, Bootstrap 5
- **Hosting**: Azure App Service
- **Authentication**: ASP.NET Core Identity

## 🚀 Getting Started

### Prerequisites

- .NET 9 SDK
- Azure SQL Database (for production)
- SQL Server LocalDB (for development)

### Local Development

1. Clone the repository
2. Update `appsettings.json` with your database connection string
3. Run `dotnet restore` and `dotnet build`
4. Execute `dotnet run` to start the application

### Production Deployment

The application is automatically deployed to Azure App Service and can be accessed at:
**https://inventory-manager-pluska-1755210558.azurewebsites.net**

## 👥 Team Members

**Andres Pluska** - Full Stack Developer & Project Lead

## 📝 Recent Updates

- ✅ Fixed DbContext concurrency issues in Blazor Server components
- ✅ Migrated to IDbContextFactory pattern for better performance
- ✅ Resolved 500 errors in production environment
- ✅ Successfully deployed to Azure with working database connectivity
- ✅ Updated all components: Products, StockMovements, Suppliers, and Reports

## 🔗 Links

- **Live Application**: [https://inventory-manager-pluska-1755210558.azurewebsites.net](https://inventory-manager-pluska-1755210558.azurewebsites.net)
- **GitHub Repository**: [https://github.com/pluska/inventory-manager-net](https://github.com/pluska/inventory-manager-net)
