# 🌾 AgroindustryManagement

## Overview
The Agroindustry Management Application is a comprehensive tool designed to streamline and optimize agricultural operations. It provides a user-friendly interface for managing fields, workers, tasks, resources, and machinery. The application is built with scalability and maintainability in mind, leveraging modern technologies and best practices in software development.

---

## Features
### Core Functionality
- **Field Management**:
  - Add, edit, delete, and view fields.
  - Calculate fertilizer and seed requirements based on field area and crop type.
  - Estimate yield and fuel consumption for field operations.
  - Determine the required machinery count and work duration for field tasks.

- **Worker Management**:
  - Add, edit, delete, and view workers.
  - Assign tasks to workers and track their progress.

- **Task Management**:
  - Create, edit, delete, and view tasks.
  - Assign tasks to specific workers and fields.
  - Track task progress and completion.

- **Resource Management**:
  - Manage resources such as seeds, fertilizers, and machinery.
  - Display detailed information about resources and their requirements.

- **Inventory Management**:
  - Track inventory items and their quantities.
  - Identify critical inventory items below the threshold.

- **Machinery Management**:
  - Add, edit, delete, and view machinery.
  - Track machinery availability and assignments to fields.

### Additional Features
- **Dynamic Data Collection**:
  - Universal data collection and editing methods for all models.

- **Validation**:
  - Input validation for all user-provided data.
  - Ensures data integrity and prevents invalid entries.

- **Calculation Services**:
  - Fertilizer and seed amount calculations.
  - Yield estimation based on crop type and field area.
  - Fuel consumption estimation for machinery.

- **Menu Navigation**:
  - Intuitive menu system for navigating through different functionalities.
  - Modular menu handlers for scalability.

---

## Technologies Used
- **Programming Language**: C#
- **Framework**: .NET Core
- **Database**: Entity Framework Core (EF Core) with an in-memory database for testing.
- **Architecture**: Layered architecture with separation of concerns.
- **Testing**: xUnit for unit testing.

---

## Project Structure
AgroindustryManagement/
├── Models/                      # Data models
│   ├── Field.cs                 # Field model
│   ├── Worker.cs                # Worker model
│   ├── WorkerTask.cs            # Worker task model
│   ├── Resource.cs              # Resource model
│   ├── Machine.cs               # Machine model
│   ├── InventoryItem.cs         # Inventory item model
│   └── Enums/                   # Enums for various types
│       ├── MachineType.cs       # Machine type enum
│       └── TaskType.cs          # Task type enum
│
├── Data/                        # Database context and services
│   ├── AgroDbContext.cs         # EF Core DbContext
│   ├── DatabaseInitializer.cs   # Seed data initializer
│   └── AGDatabaseService.cs     # Database service for CRUD operations
│
├── Services/                    # Business logic and calculations
│   ├── CalculationService.cs    # Calculation logic for various operations
│   ├── DataCollector.cs         # Universal data collection logic
│   ├── ViewService.cs           # Handles user interface rendering
│   └── Menu/                    # Menu state handlers
│       ├── AGFieldMenuStateHandler.cs   # Field menu handler
│       ├── AGWorkerMenuStateHandler.cs  # Worker menu handler
│       └── AGTaskMenuStateHandler.cs    # Task menu handler
│
├── Tests/                       # Unit tests
│   ├── DatabaseServiceTests/    # Tests for database services
│   ├── CalculationServiceTests/ # Tests for calculation services
│   └── ViewServiceTests/        # Tests for view rendering
│
├── Program.cs                   # Application entry point
└── README.md                    # Project documentation

---

## How to Run
1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/agroindustry-management.git

   Testing
The project includes unit tests for core functionalities.
To run the tests:
dotnet test
