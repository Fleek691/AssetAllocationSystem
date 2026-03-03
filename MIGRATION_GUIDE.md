# Migration Commands Guide

## Entity Framework Core Migrations

Since the migrations have been moved to the **AssetAllocationSystem.DAL** project, you need to specify the project paths when running EF Core commands.

### Important Notes:
- **Migrations are now in:** `AssetAllocationSystem.DAL/Migrations`
- **DbContext is in:** `AssetAllocationSystem.DAL` project
- **Startup project is:** `AssetAllocationSystem` (the web API project)

---

## Common Migration Commands

### 1. Add a New Migration
```bash
dotnet ef migrations add MigrationName --project AssetAllocationSystem.DAL --startup-project AssetAllocationSystem
```

### 2. Update Database
```bash
dotnet ef database update --project AssetAllocationSystem.DAL --startup-project AssetAllocationSystem
```

### 3. Remove Last Migration
```bash
dotnet ef migrations remove --project AssetAllocationSystem.DAL --startup-project AssetAllocationSystem
```

### 4. List Migrations
```bash
dotnet ef migrations list --project AssetAllocationSystem.DAL --startup-project AssetAllocationSystem
```

### 5. Generate SQL Script
```bash
dotnet ef migrations script --project AssetAllocationSystem.DAL --startup-project AssetAllocationSystem
```

### 6. Update to Specific Migration
```bash
dotnet ef database update MigrationName --project AssetAllocationSystem.DAL --startup-project AssetAllocationSystem
```

---

## Using Package Manager Console (Visual Studio)

If you're using Visual Studio Package Manager Console:

### 1. Add Migration
```powershell
Add-Migration MigrationName -Project AssetAllocationSystem.DAL -StartupProject AssetAllocationSystem
```

### 2. Update Database
```powershell
Update-Database -Project AssetAllocationSystem.DAL -StartupProject AssetAllocationSystem
```

### 3. Remove Migration
```powershell
Remove-Migration -Project AssetAllocationSystem.DAL -StartupProject AssetAllocationSystem
```

---

## Default Project Settings (Visual Studio)

To avoid typing the project parameters every time:

1. Open **Package Manager Console**
2. Set **Default project** to `AssetAllocationSystem.DAL`
3. Ensure **AssetAllocationSystem** is set as the startup project (right-click → Set as Startup Project)

Then you can use shorter commands:
```powershell
Add-Migration MigrationName
Update-Database
Remove-Migration
```

---

## Connection String

Make sure your `appsettings.json` in the **AssetAllocationSystem** project has the correct connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=AssetAllocationDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

---

## Troubleshooting

### Error: "Unable to create an object of type 'ApplicationDbContext'"

**Solution:** Make sure you're specifying the `--startup-project` parameter, as the DbContext needs the connection string from the web project's `appsettings.json`.

### Error: "The term 'dotnet-ef' is not recognized"

**Solution:** Install the EF Core CLI tools globally:
```bash
dotnet tool install --global dotnet-ef
```

Or update if already installed:
```bash
dotnet tool update --global dotnet-ef
```

---

## Migration Namespace

All migrations are now in the namespace:
```csharp
namespace AssetAllocationSystem.DAL.Migrations
```

This ensures migrations are properly organized within the Data Access Layer.
