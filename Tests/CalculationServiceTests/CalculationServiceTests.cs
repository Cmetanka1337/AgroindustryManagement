using AgroindustryManagement.Models;
using AgroindustryManagement.Services.Calculations;
using AgroindustryManagement.Services.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.CalculationServiceTests
{
    public class CalculationServiceTests
    {
        private readonly AGCalculationService _calculationService;
        private readonly AGDatabaseContext _mockDBContext;
        private readonly AGDatabaseService _databaseService;
        public CalculationServiceTests()
        {
            var options = new DbContextOptionsBuilder<AGDatabaseContext>()
                        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                        .Options;
            _mockDBContext = new AGDatabaseContext(options);
            _databaseService=new AGDatabaseService(_mockDBContext);
            _calculationService=new AGCalculationService(_databaseService);
        }
        [Fact]
        public void Test_CalculateSeedAmount_ReturnCorrectSeedAmount()
        {
            //ARRANGE
            var mockResource = new Resource
            {
                CultureType = CultureType.Wheat,
                SeedPerHectare = 200
            };
            _mockDBContext.Resources.Add(mockResource);
            _mockDBContext.SaveChanges();
            //ACT
            var result = _calculationService.CalculateSeedAmount("Wheat", 10);
            //ASSERT
            Assert.Equal(2000, result);
        }
        [Fact]
        public void Test_CalculateSeedAmount_EmptyCropType_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            _calculationService.CalculateSeedAmount("", 10));

        }
        [Fact]
        public void Test_CalculateSeedAmount_NegativeArea_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                        _calculationService.CalculateSeedAmount("Wheat", -5));
            Assert.Throws<ArgumentException>(() =>
                        _calculationService.CalculateSeedAmount("Wheat", 0));
        }
        [Fact]
        public void Test_CalculateSeedAmount_CultureTypeNotFound_ThrowsKeyNotFoundException()
        {
            var mockResource = new Resource
            {
                CultureType = CultureType.Wheat
            };
            _mockDBContext.Resources.Add(mockResource);
            _mockDBContext.SaveChanges();
            Assert.Throws<KeyNotFoundException>(() =>
                        _calculationService.CalculateSeedAmount("Rice", 10));
        }
        [Fact]
        public void Test_CalculateFertilizerAmount_ReturnCorrectFertilizerAmount()
        {
            var mockResource = new Resource
            {
                CultureType= CultureType.Wheat,
                FertilizerPerHectare=100
            };
            _mockDBContext.Resources.Add(mockResource);
            _mockDBContext.SaveChanges();
            var result = _calculationService.CalculateFertilizerAmount("Wheat", 20);
            Assert.Equal(2000, result);
        }
        [Fact]
        public void Test_CalculateFertilizerAmount_EmptyCropType_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            _calculationService.CalculateFertilizerAmount("", 10));
        }
        [Fact]
        public void Test_CalculateFertilizerAmount_NegativeArea_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                        _calculationService.CalculateFertilizerAmount("Wheat", -5));
            Assert.Throws<ArgumentException>(() =>
                        _calculationService.CalculateFertilizerAmount("Wheat", 0));
        }
        [Fact]
        public void Test_CalculateFertilizerAmount_CultureTypeNotFound_ThrowsKeyNotFoundException()
        {
            var mockResource = new Resource
            {
                CultureType= CultureType.Wheat
            };
            _mockDBContext.Resources.Add(mockResource);
            _mockDBContext.SaveChanges();
            Assert.Throws<KeyNotFoundException>(() =>
            _calculationService.CalculateFertilizerAmount("Rice", 10));
        }
        [Fact]
        public void Test_EstimateYield_ReturnCorrectYield()
        {
            var resource = new Resource
            {
                CultureType=CultureType.Wheat,
                Yield=20
            };
            _mockDBContext.Resources.Add(resource);
            _mockDBContext.SaveChanges();
            var result = _calculationService.EstimateYield("Wheat", 10);
            Assert.Equal(200, result);
        }
        [Fact]
        public void Test_EstimateYield_EmptyCropType_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            _calculationService.EstimateYield("", 10));
        }
        [Fact]
        public void Test_EstimateYield_NegativeArea_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            _calculationService.EstimateYield("Wheat", -5));

            Assert.Throws<ArgumentException>(() =>
            _calculationService.EstimateYield("Wheat", 0));
        }
        [Fact]
        public void Test_EstimateYield_CultureTypeNotFound_ThrowsKeyNotFoundException()
        {

            var mockResource = new Resource
            {
                CultureType= CultureType.Wheat
            };
            _mockDBContext.Resources.Add(mockResource);
            _mockDBContext.SaveChanges();
            Assert.Throws<KeyNotFoundException>(() =>
            _calculationService.EstimateYield("Rice", 10));
        }
        [Fact]
        public void Test_CalculateRequiredMachineryCount_ReturnCorrectNumberOfRequiredMachines()
        {
            var machine = new Machine { Id = 1 };
            var machine1 = new Machine { Id = 2 };
            var mockResource = new Resource
            {
                CultureType = CultureType.Wheat
            };
            mockResource.RequiredMachines.Add(machine);
            mockResource.RequiredMachines.Add(machine1);
            _mockDBContext.Resources.Add(mockResource);
            _mockDBContext.SaveChanges();
            var result = _calculationService.CalculateRequiredMachineryCount("Wheat", 10);
            Assert.Equal(2, result);
        }
        [Fact]
        public void Test_CalculateRequiredMachineryCount_EmptyCropType_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            _calculationService.CalculateRequiredMachineryCount("", 10));
        }
        [Fact]
        public void Test_CalculateRequiredMachineryCount_NegativeArea_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
           _calculationService.CalculateRequiredMachineryCount("Wheat", -5));

            Assert.Throws<ArgumentException>(() =>
            _calculationService.CalculateRequiredMachineryCount("Wheat", 0));
        }
        [Fact]
        public void Test_CalculateRequiredMachineryCount_CultureTypeNotFound_ThrowsKeyNotFoundException()
        {
            var mockResource = new Resource
            {
                CultureType= CultureType.Wheat
            };
            _mockDBContext.Resources.Add(mockResource);
            _mockDBContext.SaveChanges();
            Assert.Throws<KeyNotFoundException>(() =>
            _calculationService.CalculateRequiredMachineryCount("Rice", 10));
        }
        [Fact]
        public void Test_EstimateFuelConsumption_ReturnCorrectFuelConsumption()
        {
            var mockMachine = new Machine
            {
                Type=MachineType.Tractor,
                FuelConsumption=20
            };
            _databaseService.AddMachine(mockMachine);
            _mockDBContext.SaveChanges();
            var result = _calculationService.EstimateFuelConsumption("Tractor", 20);
            Assert.Equal(400, result);
        }
        [Fact]
        public void Test_EstimateFuelConsumption_EmptyMachineType_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            _calculationService.EstimateFuelConsumption("", 10));
        }
        [Fact]
        public void Test_EstimateFuelConsumption_NegativeArea_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
           _calculationService.EstimateFuelConsumption("Wheat", -5));

            Assert.Throws<ArgumentException>(() =>
            _calculationService.EstimateFuelConsumption("Wheat", 0));
        }
        [Fact]
        public void Test_EstimateFuelConsumption_MachineTypeNotFound_ThrowsKeyNotFoundException()
        {
            var mockMachine = new Machine
            {
                Type = MachineType.Tractor
            };
            _databaseService.AddMachine(mockMachine);

            Assert.Throws<KeyNotFoundException>(() =>
            _calculationService.EstimateFuelConsumption("Plow", 10));
        }
        [Fact]
        public void Test_CalculateRequiredWorkers_ReturnCorrectNumberOfRequiredWorkers()
        {
            var mockResource = new Resource
            {
                CultureType=CultureType.Wheat,
                WorkerPerHectare=3
            };
            _mockDBContext.Resources.Add(mockResource);
            _mockDBContext.SaveChanges();
            var result = _calculationService.CalculateRequiredWorkers("Wheat", 10);
            Assert.Equal(30, result);
        }
        [Fact]
        public void Test_CalculateRequiredWorkers_EmptyCropType_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            _calculationService.CalculateRequiredWorkers("", 10));
        }
        [Fact]
        public void Test_CalculateRequiredWorkers_NegativeArea_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
           _calculationService.CalculateRequiredWorkers("Wheat", -5));

            Assert.Throws<ArgumentException>(() =>
            _calculationService.CalculateRequiredWorkers("Wheat", 0));
        }
        [Fact]
        public void Test_CalculateRequiredWorkers_CropTypeNotFound_ThrowsKeyNotFoundException()
        {
            var mockResource = new Resource
            {
                CultureType = CultureType.Wheat
            };
            _mockDBContext.Resources.Add(mockResource);
            _mockDBContext.SaveChanges();
            Assert.Throws<KeyNotFoundException>(() =>
            _calculationService.CalculateRequiredWorkers("Rice", 10));
        }
        [Fact]
        public void Test_EstimateWorkDuration_ReturnCorrectWorkDuration()
        {
            var mockResource = new Resource
            {
                CultureType = CultureType.Wheat,
                WorkerWorkDuralityPerHectare=70
            };
            var mockMachine = new Machine
            {
                Type=MachineType.Tractor,
                WorkDuralityPerHectare=20
            };
            _mockDBContext.Resources.Add(mockResource);
            _databaseService.AddMachine(mockMachine);
            _mockDBContext.SaveChanges();
            var result = _calculationService.EstimateWorkDuration(10, 10, "Tractor", "Wheat");
            Assert.Equal(200, result);
        }
        [Fact]
        public void Test_EstimateWorkDuration_EmptyCropTypeOrMachineType_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            _calculationService.EstimateWorkDuration(10,10,"", "Tractor"));
            Assert.Throws<ArgumentException>(() =>
          _calculationService.EstimateWorkDuration(10, 10, "Wheat", ""));
        }
        [Fact]
        public void Test_EstimateWorkDuration_NegativeAreaOrWorkerCounts_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
           _calculationService.EstimateWorkDuration(-5, -5, "Wheat", "Tractor"));

            Assert.Throws<ArgumentException>(() =>
            _calculationService.EstimateWorkDuration(0,5, "Wheat", "Tractor"));
        }
        [Fact]
        public void Test_EstimateWorkDuration_CropTypeOrMachineTypeNotFound_ThrowsKeyNotFoundException()
        {
            var mockResource = new Resource
            {
                CultureType = CultureType.Wheat
            };
            var mockMachine = new Machine
            {
                Type=MachineType.Tractor
            };
            _mockDBContext.Resources.Add(mockResource);
            _databaseService.AddMachine(mockMachine);
            _mockDBContext.SaveChanges();
            Assert.Throws<KeyNotFoundException>(() =>
            _calculationService.EstimateWorkDuration(10, 10, "Plow", "Wheat"));
        }
        [Fact]
        public void Test_CalculateBonus_ReturnCorrectTotalBonus()
        {
            var mockWorker = new Worker
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                HourlyRate= 300,
                HoursWorked=50
            };
            var mockWorkerTask = new WorkerTask
            {
                WorkerId= 1,
                Description="Test",
                EstimatesEndDate= DateTime.Now,
                RealEndDate =new DateTime(2025, 11, 7, 12, 10, 30)
            };
            _databaseService.AddWorker(mockWorker);
            _databaseService.AddWorkerTask(mockWorkerTask);
            
            var result = _calculationService.CalculateBonus(1);
            Assert.Equal(900, result);
        }
        [Fact]
        public void Test_CalculateBonus_NegativeWorkerId_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _calculationService.CalculateBonus(-5));
            Assert.Throws<ArgumentException>(() => _calculationService.CalculateBonus(0));
        }
        [Fact]
        public void Test_CalculateBonus_WorkerIdNotFound_ThrowsKeyNotFoundException()
        {
            var worker = new Worker
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe"
            };
            _databaseService.AddWorker(worker);
            Assert.Throws<KeyNotFoundException>(() => _calculationService.CalculateBonus(3));
        }
    }
}
