using AgroindustryManagement.Models;
using AgroindustryManagement.Services.Database;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DatabaseServiceTests
{
    public class DataBaseServiceTests
    {
        private readonly AGDatabaseService _databaseService;
        private readonly AGDatabaseContext _mockDbContext;
        public DataBaseServiceTests()
        {
            var options = new DbContextOptionsBuilder<AGDatabaseContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB per test
            .Options;
            _mockDbContext = new AGDatabaseContext(options);
            _databaseService = new AGDatabaseService(_mockDbContext);
        }
        [Fact]
        public void Test_GetAllFields_ReturnsFields()
        {
            // Arrange
            var mockFields = new List<Field>
            {
                new Field { Id = 1, Area = 50 },
                new Field { Id = 2, Area = 100 }
            };
            _mockDbContext.Fields.AddRange(mockFields);
            _mockDbContext.SaveChanges();
            // Act
            var result = _databaseService.GetAllFields();
            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("50", result.First().Area.ToString());
        }
        [Fact]
        public void Test_GetFieldById_ReturnsCorrectField()
        {
            // Arrange
            var mockField = new Field { Id = 1, Area = 75 };
            _mockDbContext.Fields.Add(mockField);
            _mockDbContext.SaveChanges();
            // Act
            var result = _databaseService.GetFieldById(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(75, result.Area);
        }
        [Fact]
        public void Test_AddField_AddsFieldSuccessfully()
        {
            // Arrange
            var newField = new Field { Id = 1, Area = 200 };
            // Act
            _databaseService.AddField(newField);
            var result = _databaseService.GetFieldById(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.Area);
        }
        [Fact]
        public void Test_DeleteField_RemovesFieldSuccessfully()
        {
            // Arrange
            var mockField = new Field { Id = 1, Area = 150 };
            _mockDbContext.Fields.Add(mockField);
            _mockDbContext.SaveChanges();
            // Act
            _databaseService.DeleteField(1);
            // Assert

            Assert.Throws<KeyNotFoundException>(() => _databaseService.GetFieldById(1));
        }
        [Fact]
        public void Test_UpdateField_UpdatesFieldSuccessfully()
        {
            // Arrange
            var mockField = new Field { Id = 1, Area = 120 };
            _mockDbContext.Fields.Add(mockField);
            _mockDbContext.SaveChanges();
            // Act
            mockField.Area = 180;
            _databaseService.UpdateField(mockField);
            var result = _databaseService.GetFieldById(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(180, result.Area);
        }
        [Fact]
        public void Test_GetWorkerById_ReturnsCorrectWorker()
        {
            // Arrange
            var mockWorker = new Worker("first", "surname", new List<WorkerTask>());
            mockWorker.Id = 1;
            // Act
            _databaseService.AddWorker(mockWorker);
            var result = _databaseService.GetWorkerById(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("first", result.FirstName);
        }
        [Fact]
        public void Test_AddWorker_AddsWorkerSuccessfully()
        {
            // Arrange
            var newWorker = new Worker("John", "Doe", new List<WorkerTask>());
            // Act
            _databaseService.AddWorker(newWorker);
            var result = _databaseService.GetWorkerById(newWorker.Id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
        }
        [Fact]
        public void Test_DeleteWorker_RemovesWorkerSuccessfully()
        {
            // Arrange
            var mockWorker = new Worker("Jane", "Smith", new List<WorkerTask>());
            mockWorker.Id = 1;
            _databaseService.AddWorker(mockWorker);
            // Act
            _databaseService.DeleteWorker(1);
            // Assert
            Assert.Throws<KeyNotFoundException>(() => _databaseService.GetWorkerById(1));
        }
        [Fact]
        public void Test_UpdateWorker_UpdatesWorkerSuccessfully()
        {
            // Arrange
            var mockWorker = new Worker("Alice", "Johnson", new List<WorkerTask>());
            mockWorker.Id = 1;
            _databaseService.AddWorker(mockWorker);
            // Act
            mockWorker.LastName = "Williams";
            _databaseService.UpdateWorker(mockWorker);
            var result = _databaseService.GetWorkerById(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("Williams", result.LastName);
        }
        [Fact]
        public void Test_GetAllWorkers_ReturnsWorkers()
        {
            // Arrange
            var mockWorkers = new List<Worker>
            {
                new Worker("First1", "Last1", new List<WorkerTask>()),
                new Worker("First2", "Last2", new List<WorkerTask>())
            };
            foreach (var worker in mockWorkers)
            {
                _databaseService.AddWorker(worker);
            }
            // Act
            var result = _databaseService.GetAllWorkers();
            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("First1", result.First().FirstName);
        }
        [Fact]
        public void Test_UpdateWorker_NonExistentWorker_ThrowsException()
        {
            // Arrange
            var nonExistentWorker = new Worker("Non", "Existent", new List<WorkerTask>());
            nonExistentWorker.Id = 999; // Assuming this ID does not exist
            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => _databaseService.UpdateWorker(nonExistentWorker));
        }
        [Fact]
        public void Test_AddNewMachine_AddsMachineSuccessfully()
        {
            // Arrange
            var newMachine = new Machine { Id = 1, Type = MachineType.Harvester };
            // Act
            _databaseService.AddMachine(newMachine);
            var result = _databaseService.GetMachineById(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("Harvester", result.Type.ToString());
        }
        [Fact]
        public void Test_GetMachineById_ReturnsCorrectMachine()
        {
            // Arrange
            var mockMachine = new Machine { Id = 1, Type = MachineType.Tractor };
            _databaseService.AddMachine(mockMachine);
            // Act
            var result = _databaseService.GetMachineById(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("Tractor", result.Type.ToString());
        }
        [Fact]
        public void Test_DeleteMachine_RemovesMachineSuccessfully()
        {
            // Arrange
            var mockMachine = new Machine { Id = 1, Type = MachineType.Tractor };
            _databaseService.AddMachine(mockMachine);
            // Act
            _databaseService.DeleteMachine(1);
            // Assert
            Assert.Throws<KeyNotFoundException>(() => _databaseService.GetMachineById(1));
        }
        [Fact]
        public void Test_UpdateMachine_UpdatesMachineSuccessfully()
        {
            // Arrange
            var mockMachine = new Machine { Id = 1, Type = MachineType.Seeder };
            _databaseService.AddMachine(mockMachine);
            // Act
            mockMachine.Type = MachineType.Harvester;
            _databaseService.UpdateMachine(mockMachine);
            var result = _databaseService.GetMachineById(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("Harvester", result.Type.ToString());
        }
        [Fact]
        public void Test_GetAllMachines_ReturnsMachines()
        {
            // Arrange
            var mockMachines = new List<Machine>
            {
                new Machine { Id = 1, Type = MachineType.Tractor },
                new Machine { Id = 2, Type = MachineType.Seeder }
            };
            foreach (var machine in mockMachines)
            {
                _databaseService.AddMachine(machine);
            }
            // Act
            var result = _databaseService.GetAllMachines();
            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("Tractor", result.First().Type.ToString());
        }
        [Fact]
        public void Test_AddNewInventoryItem_AddsItemSuccessfully()
        {
            // Arrange
            var newItem = new InventoryItem("Fertilizer", "litres");
            // Act
            _databaseService.AddInventoryItem(newItem);
            var result = _databaseService.GetInventoryItemById(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("Fertilizer", result.Name);
        }
        [Fact]
        public void Test_GetInventoryItemById_ReturnsCorrectItem()
        {
            // Arrange
            var mockItem = new InventoryItem("Seeds", "kg");
            _databaseService.AddInventoryItem(mockItem);
            // Act
            var result = _databaseService.GetInventoryItemById(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("Seeds", result.Name);
        }
        [Fact]
        public void Test_DeleteInventoryItem_RemovesItemSuccessfully()
        {
            // Arrange
            var mockItem = new InventoryItem("Pesticide", "litres");
            _databaseService.AddInventoryItem(mockItem);
            // Act
            _databaseService.DeleteInventoryItem(1);
            // Assert
            Assert.Throws<KeyNotFoundException>(() => _databaseService.GetInventoryItemById(1));
        }
        [Fact]
        public void Test_UpdateInventoryItem_UpdatesItemSuccessfully()
        {
            // Arrange
            var mockItem = new InventoryItem("Herbicide", "litres");
            _databaseService.AddInventoryItem(mockItem);
            // Act
            mockItem.Name = "UpdatedHerbicide";
            _databaseService.UpdateInventoryItem(mockItem);
            var result = _databaseService.GetInventoryItemById(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("UpdatedHerbicide", result.Name);
        }
        [Fact]
        public void Test_GetAllInventoryItems_ReturnsItems()
        {
            // Arrange
            var mockItems = new List<InventoryItem>
            {
                new InventoryItem("Item1", "kg"),
                new InventoryItem("Item2", "litres")
            };
            foreach (var item in mockItems)
            {
                _databaseService.AddInventoryItem(item);
            }
            // Act
            var result = _databaseService.GetAllInventoryItems();
            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("Item1", result.First().Name);
        }
        [Fact]
        public void Test_AddNewWorkerTask_AddsTaskSuccessfully()
        {
            // Arrange
            var newTask = new WorkerTask { Id = 1, Description = "Plowing" };
            // Act
            _databaseService.AddWorkerTask(newTask);
            var result = _databaseService.GetWorkerTaskById(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("Plowing", result.Description);
        }
        [Fact]
        public void Test_GetWorkerTaskById_ReturnsCorrectTask()
        {
            // Arrange
            var mockTask = new WorkerTask { Id = 1, Description = "Sowing" };
            _databaseService.AddWorkerTask(mockTask);
            // Act
            var result = _databaseService.GetWorkerTaskById(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("Sowing", result.Description);
        }
        [Fact]
        public void Test_DeleteWorkerTask_RemovesTaskSuccessfully()
        {
            // Arrange
            var mockTask = new WorkerTask { Id = 1, Description = "Irrigation" };
            _databaseService.AddWorkerTask(mockTask);
            // Act
            _databaseService.DeleteWorkerTask(1);
            // Assert
            Assert.Throws<KeyNotFoundException>(() => _databaseService.GetWorkerTaskById(1));
        }
        [Fact]
        public void Test_UpdateWorkerTask_UpdatesTaskSuccessfully()
        {
            // Arrange
            var mockTask = new WorkerTask { Id = 1, Description = "Harvesting" };
            _databaseService.AddWorkerTask(mockTask);
            // Act
            mockTask.Description = "UpdatedHarvesting";
            _databaseService.UpdateWorkerTask(mockTask);
            var result = _databaseService.GetWorkerTaskById(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("UpdatedHarvesting", result.Description);
        }
        [Fact]
        public void Test_GetAllWorkerTasks_ReturnsTasks()
        {
            // Arrange
            var mockTasks = new List<WorkerTask>
            {
                new WorkerTask { Id = 1, Description = "Task1" },
                new WorkerTask { Id = 2, Description = "Task2" }
            };
            foreach (var task in mockTasks)
            {
                _databaseService.AddWorkerTask(task);
            }
            // Act
            var result = _databaseService.GetAllWorkerTasks();
            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("Task1", result.First().Description);
        }
        [Fact]
        public void Test_UpdateWorkerTask_NonExistentTask_ThrowsException()
        {
            // Arrange
            var nonExistentTask = new WorkerTask { Id = 999, Description = "NonExistent" }; // Assuming this ID does not exist
            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => _databaseService.UpdateWorkerTask(nonExistentTask));
        }
        [Fact]
        public void Test_AddField_WithWorkersAndMachines()
        {
            // Arrange
            var workers = new List<Worker>
            {
                new Worker("Worker1", "Last1", new List<WorkerTask>()),
                new Worker("Worker2", "Last2", new List<WorkerTask>())
            };
            var machines = new List<Machine>
            {
                new Machine { Type = MachineType.Tractor },
                new Machine { Type = MachineType.Seeder }
            };
            var newField = new Field(workers, machines, new List<WorkerTask>())
            {
                Area = 300
            };
            // Act
            _databaseService.AddField(newField);
            var result = _databaseService.GetFieldById(newField.Id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Workers.Count);
            Assert.Equal(2, result.Machines.Count);
        }
        [Fact]
        public void Test_AddWorker_WithTasks()
        {
            // Arrange
            var tasks = new List<WorkerTask>
            {
                new WorkerTask { Description = "Task1" },
                new WorkerTask { Description = "Task2" }
            };
            var newWorker = new Worker("Tasked", "Worker", tasks);
            // Act
            _databaseService.AddWorker(newWorker);
            var result = _databaseService.GetWorkerById(newWorker.Id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Tasks.Count);
        }
        [Fact]
        public void Test_GetResourceByCultureType_ReturnsCorrectResource()
        {
            // Arrange
            var mockResource = new Resource
            {
                CultureType = CultureType.Corn,
                SeedPerHectare = 25.0
            };
            _mockDbContext.Resources.Add(mockResource);
            _mockDbContext.SaveChanges();
            var cultureType = CultureType.Corn;
            // Act
            var result = _databaseService.GetResourceByCultureType(cultureType);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("Corn", result.CultureType.ToString());
        }
        [Fact]
        public void Test_GetCriticalInventoryItems_ReturnsItemsBelowThreshold()
        {
            // Arrange
            var mockItems = new List<InventoryItem>
            {
                new InventoryItem("Item1", "kg") { Quantity = 2 },
                new InventoryItem("Item2", "litres") { Quantity = 15 },
                new InventoryItem("Item3", "kg") { Quantity = 0 }
            };
            _mockDbContext.InventoryItems.AddRange(mockItems);
            _mockDbContext.SaveChanges();
            // Act
            var result = _databaseService.GetCriticalInventoryItems();
            // Assert
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public void Test_GetTasksByWorkerId_ReturnsCorrectTasks()
        {
            // Arrange
            var worker = new Worker("Tasked", "Worker", new List<WorkerTask>());
            _databaseService.AddWorker(worker);
            var mockTasks = new List<WorkerTask>
            {
                new WorkerTask { Description = "Task1", Worker = worker },
                new WorkerTask { Description = "Task2", Worker = worker }
            };
            foreach (var task in mockTasks)
            {
                _databaseService.AddWorkerTask(task);
            }
            // Act
            var result = _databaseService.GetTasksByWorkerId(worker.Id);
            // Assert
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public void Test_GetAvailableMachines_ReturnsMachinesNotAssignedToFields()
        {
            // Arrange
            var assignedMachine = new Machine { Type = MachineType.Tractor, Field = new Field(),IsAvailable=false };
            var unassignedMachine1 = new Machine { Type = MachineType.Seeder, IsAvailable = true };
            var unassignedMachine2 = new Machine { Type = MachineType.Harvester, IsAvailable = true };
            _mockDbContext.Machines.AddRange(assignedMachine, unassignedMachine1, unassignedMachine2);
            _mockDbContext.SaveChanges();
            // Act
            var result = _databaseService.GetAvailableMachines();
            // Assert
            Assert.Equal(2, result.Count());
        }
    }
}
