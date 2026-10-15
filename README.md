MarketLink --- Agricultural Marketplace Management System
MarketLink is an ASP.NET Core MVC web application for managing an
agricultural marketplace. It provides a centralized administrative
interface for overseeing farmers, customers, markets, orders, product
listings, reviews, and marketplace announcements.
Features
- Dashboard: View marketplace totals for farmers, customers,
  markets, and orders, along with recent orders and announcements.
- Farmer management: Search and filter farmer records, view
  details, and approve, suspend, or reinstate accounts.
- Customer management: Search customers, filter by account status,
  review customer details, and activate or deactivate accounts.
- Market management: Create, update, and remove markets; configure
  opening days and hours; and store location and map information.
- Order and sales reporting: View order and revenue summaries,
  market performance, and active farmer information; record
  report-generation requests.
- Product and review moderation: Review marketplace listings and
  customer reviews, and remove content when necessary.
- Category management: Add, rename, and delete product categories,
  with checks to prevent deleting categories that are still in use.
- Announcements: Publish announcements for a selected audience and
  manage existing announcements.
- Authentication: Register, sign in, and sign out using ASP.NET
  Core Identity.
- Authorization: Protect management controllers behind
  authenticated access.
- Relational data model: Use Entity Framework Core and SQL Server
  to manage marketplace records and relationships.
Note: The repository contains both administrative MVC modules and
marketplace entity models. The availability of a model does not
necessarily mean that a complete customer-facing workflow is
implemented.

Technology Stack
- Language: C#
- Framework: ASP.NET Core MVC (.NET 8)
- ORM: Entity Framework Core
- Database: Microsoft SQL Server
- Authentication: ASP.NET Core Identity
- Frontend: Razor Views, HTML, CSS, JavaScript, and Bootstrap
  (where used in the project)
Getting Started
Prerequisites
- .NET 8 SDK
- Microsoft SQL Server (local or remote instance)
- Visual Studio with the ASP.NET and web development workload, or
  Visual Studio Code with the .NET SDK
1. Clone the repository
git clone <YOUR_REPOSITORY_URL>
cd MarketLinkedWebsite
Replace <YOUR_REPOSITORY_URL> with the HTTPS or SSH URL of your GitHub
repository.
2. Configure the database connection
Open MarketLinkedWebsite/appsettings.json and update the
DefaultConnection connection string for your SQL Server instance.
Example for a local SQL Server installation:
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=MarketLinkDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
Use the connection string format appropriate for your SQL Server
installation. Do not commit passwords, production credentials, or other
secrets to GitHub. For production, use environment variables or .NET
User Secrets.
3. Restore dependencies
From the directory containing MarketLinkedWebsite.csproj, run:
dotnet restore
4. Apply database migrations
If Entity Framework Core migrations are included in the repository,
apply them with:
dotnet ef database update
If the EF CLI is not installed, install it with:
dotnet tool install --global dotnet-ef
If migrations are not present, create and review an initial migration
before updating the database:
dotnet ef migrations add InitialCreate
dotnet ef database update
Run migration commands from the project directory and ensure the
required EF Core 8 design and SQL Server provider packages are
installed.
5. Run the application
dotnet run
Open the local URL printed in the terminal. You can also open the
solution in Visual Studio and run the web project.
Project Structure
MarketLinkedWebsite/
└── MarketLinkedWebsite/
    ├── Controllers/       # MVC controllers and application workflows
    ├── Data/              # Entity Framework Core DbContext
    ├── Models/            # View and application models
    ├── UserModel/         # Marketplace entities and enums
    ├── Views/              # Razor views
    ├── wwwroot/            # Static assets
    ├── appsettings.json    # Application configuration
    ├── Program.cs          # Service registration and HTTP pipeline
    └── MarketLinkedWebsite.csproj
Security Notes
- Configure a secure database connection before deployment.
- Keep credentials and secrets outside source control.
- Review role assignments and authorization policies before exposing
  the application publicly.
- Use HTTPS in deployed environments.
- Review authentication settings and validate all application
  workflows before production use.
Future Enhancements
- Complete customer-facing product browsing and checkout flows.
- Integrate a secure payment provider if online payments are required.
- Add automated tests for controllers, validation, and database
  operations.
- Improve reporting with downloadable reports and additional
  analytics.
- Add deployment instructions and screenshots of the running
  application.
License
No license is specified in this repository. Add a LICENSE file before
permitting others to reuse, modify, or distribute the project.
