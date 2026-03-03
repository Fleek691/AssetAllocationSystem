# Asset Allocation System - 3-Layered Architecture

## Project Structure

The solution has been successfully restructured into a proper 3-layered architecture following best practices:

### 📁 Solution Structure

```
AssetAllocationSystem/
├── AssetAllocationSystem.DAL/          (Data Access Layer)
├── AssetAllocationSystem.BLL/          (Business Logic Layer)
└── AssetAllocationSystem/              (Presentation Layer)
```

---

## 1️⃣ AssetAllocationSystem.DAL (Data Access Layer)

**Purpose:** Handles all database operations, entity models, and data persistence.

### Structure:
```
AssetAllocationSystem.DAL/
├── Entities/
│   ├── Asset.cs
│   ├── AssetAssignment.cs
│   └── User.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Repositories/
│   ├── Repository.cs (Generic Repository)
│   ├── AssetRepository.cs
│   ├── UserRepository.cs
│   └── AssetAssignmentRepository.cs
├── Interfaces/
│   ├── IRepository.cs
│   ├── IAssetRepository.cs
│   ├── IUserRepository.cs
│   └── IAssetAssignmentRepository.cs
└── Migrations/
    ├── 20260225085417_InitialCreate.cs
    ├── 20260225085417_InitialCreate.Designer.cs
    └── ApplicationDbContextModelSnapshot.cs
```

### Dependencies:
- Microsoft.EntityFrameworkCore.SqlServer (10.0.3)
- Microsoft.EntityFrameworkCore.Tools (10.0.3)

### Namespace:
- **Root:** `AssetAllocationSystem.DAL`
- **Entities:** `AssetAllocationSystem.DAL.Entities`
- **Data:** `AssetAllocationSystem.DAL.Data`
- **Repositories:** `AssetAllocationSystem.DAL.Repositories`
- **Interfaces:** `AssetAllocationSystem.DAL.Interfaces`
- **Migrations:** `AssetAllocationSystem.DAL.Migrations`

---

## 2️⃣ AssetAllocationSystem.BLL (Business Logic Layer)

**Purpose:** Contains business logic, service implementations, DTOs, and helper classes.

### Structure:
```
AssetAllocationSystem.BLL/
├── Services/
│   ├── AssetService.cs
│   ├── AssignmentService.cs
│   └── AuthService.cs
├── Interfaces/
│   ├── IAssetService.cs
│   ├── IAssignmentService.cs
│   └── IAuthService.cs
├── DTOs/
│   ├── AssetResponseDto.cs
│   ├── CreateAssetDto.cs
│   ├── UpdateAssetDto.cs
│   ├── AssignAssetDto.cs
│   ├── ReturnAssetDto.cs
│   ├── AssetAssignmentResponseDto.cs
│   ├── RegisterDto.cs
│   ├── LoginDto.cs
│   └── AuthResponseDto.cs
└── Helpers/
    └── JwtHelper.cs
```

### Dependencies:
- AssetAllocationSystem.DAL (Project Reference)
- Microsoft.Extensions.Configuration.Abstractions (10.0.3)
- System.IdentityModel.Tokens.Jwt (8.16.0)

### Namespace:
- **Root:** `AssetAllocationSystem.BLL`
- **Services:** `AssetAllocationSystem.BLL.Services`
- **Interfaces:** `AssetAllocationSystem.BLL.Interfaces`
- **DTOs:** `AssetAllocationSystem.BLL.DTOs`
- **Helpers:** `AssetAllocationSystem.BLL.Helpers`

---

## 3️⃣ AssetAllocationSystem (Presentation Layer)

**Purpose:** API Controllers, middleware, and application configuration.

### Structure:
```
AssetAllocationSystem/
├── Controllers/
│   ├── AssetController.cs
│   ├── AssignmentController.cs
│   └── AuthController.cs
├── Middleware/
│   └── ExceptionMiddleware.cs
└── Program.cs
```

### Dependencies:
- AssetAllocationSystem.BLL (Project Reference)
- AssetAllocationSystem.DAL (Project Reference)
- Microsoft.AspNetCore.Authentication.JwtBearer (10.0.3)
- Microsoft.EntityFrameworkCore.SqlServer (10.0.3)
- Microsoft.EntityFrameworkCore.Tools (10.0.3)
- Swashbuckle.AspNetCore (7.2.0)

### Namespace:
- **Controllers:** `AssetAllocationSystem.Controllers`
- **Middleware:** `AssetAllocationSystem.Middleware`

---

## 🔗 Layer Dependencies

```
Presentation Layer (AssetAllocationSystem)
    ↓ references
Business Logic Layer (AssetAllocationSystem.BLL)
    ↓ references
Data Access Layer (AssetAllocationSystem.DAL)
```

**Key Principles:**
- ✅ Presentation layer references BLL and DAL
- ✅ BLL references DAL only
- ✅ DAL has no dependencies on other layers
- ✅ Each layer has its own namespace
- ✅ Proper separation of concerns

---

## 🛠️ How to Run

1. **Add projects to solution (if not already added):**
   ```bash
   dotnet sln add AssetAllocationSystem.DAL/AssetAllocationSystem.DAL.csproj
   dotnet sln add AssetAllocationSystem.BLL/AssetAllocationSystem.BLL.csproj
   dotnet sln add AssetAllocationSystem/AssetAllocationSystem.csproj
   ```

2. **Restore packages:**
   ```bash
   dotnet restore
   ```

3. **Build the solution:**
   ```bash
   dotnet build
   ```

4. **Update database (if needed):**
   ```bash
   dotnet ef database update --project AssetAllocationSystem.DAL --startup-project AssetAllocationSystem
   ```

5. **Run the application:**
   ```bash
   dotnet run --project AssetAllocationSystem
   ```

---

## ✅ Migration Notes

### What Changed:
1. **Created two new class library projects:**
   - `AssetAllocationSystem.DAL` - for data access
   - `AssetAllocationSystem.BLL` - for business logic

2. **Moved files:**
   - All entity models → `AssetAllocationSystem.DAL/Entities`
   - DbContext → `AssetAllocationSystem.DAL/Data`
   - Repositories → `AssetAllocationSystem.DAL/Repositories`
   - Migrations → `AssetAllocationSystem.DAL/Migrations` ✅ **NEW LOCATION**
   - Services → `AssetAllocationSystem.BLL/Services`
   - DTOs → `AssetAllocationSystem.BLL/DTOs`
   - JwtHelper → `AssetAllocationSystem.BLL/Helpers`

3. **Updated namespaces:**
   - Changed from `AssetAllocationSystem.Infrastructure.Data` → `AssetAllocationSystem.DAL.Entities`, `AssetAllocationSystem.DAL.Data`
   - Changed from `AssetAllocationSystem.Business.*` → `AssetAllocationSystem.BLL.*`
   - Changed from `AssetAllocationSystem.DTOs` → `AssetAllocationSystem.BLL.DTOs`
   - Migration namespace: `AssetAllocationSystem.Migrations` → `AssetAllocationSystem.DAL.Migrations` ✅

4. **Kept in Presentation Layer:**
   - Controllers (API endpoints)
   - Middleware (ExceptionMiddleware)
   - Program.cs (Startup configuration)
   - Swagger/API configuration

---

## 🎯 Benefits of This Architecture

1. **Separation of Concerns** - Each layer has a distinct responsibility
2. **Maintainability** - Easier to locate and modify code
3. **Testability** - Each layer can be tested independently
4. **Scalability** - Layers can be scaled or replaced independently
5. **Reusability** - BLL and DAL can be reused in other projects
6. **Clean Dependencies** - Clear, unidirectional dependency flow

---

## 📦 Target Framework
All projects target **.NET 10.0**

---

**✅ Build Status:** Successful
**✅ All migrations moved to DAL layer**
**✅ Project structure follows 3-layered architecture best practices**
