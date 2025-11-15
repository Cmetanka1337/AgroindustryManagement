using AgroindustryManagement.Models;
using AgroindustryManagement.Services.Database;

namespace AgroindustryManagement.Services.Calculations;

public class AGCalculationService : IAGCalculationService
{
    // Why CalculationService should have access to database? 
    // CalculationService performs calculations rather than accessing the database
    // All data required for calculations should be passed as parameters to its methods.
    private readonly IAGDatabaseService _databaseService;  
    public AGCalculationService(IAGDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }
    public double CalculateSeedAmount(CultureType cropType, double areaInHectares)
    {
        if (areaInHectares <= 0)
        {
            throw new ArgumentException("Area in hectares must be greater than zero.");
        }
        
        var resource = _databaseService.GetResourceByCultureType(cropType);

        if (resource == null)
        {
            throw new KeyNotFoundException($"No resource found for crop type: {cropType}");
        }

        var seedAmount = resource.SeedPerHectare * areaInHectares;

        return seedAmount;
    }
    
    public double CalculateFertilizerAmount(CultureType cropType, double areaInHectares)
    {
        if (areaInHectares <= 0)
        {
            throw new ArgumentException(nameof(areaInHectares), "Area in hectares must be greater than zero.");
        }
        var resource = _databaseService.GetResourceByCultureType(cropType);
        if (resource == null)
        {
            throw new KeyNotFoundException($"No resource found for crop type: {cropType}");
        }
        return resource.FertilizerPerHectare*areaInHectares;
    }

    public double EstimateYield(CultureType cropType, double areaInHectares)
    {
        if (areaInHectares <= 0)
        {
            throw new ArgumentException(nameof(areaInHectares), "Area in hectares must be greater than zero.");
        }

        var resource = _databaseService.GetResourceByCultureType(cropType);
        if(resource == null)
        {
            throw new KeyNotFoundException($"No resource found for crop type: {cropType}");
        }
        return resource.Yield * areaInHectares;
    }

    public int CalculateRequiredMachineryCount(CultureType cropType, double areaInHectares)
    {
        if (areaInHectares <= 0)
        {
            throw new ArgumentException(nameof(areaInHectares), "Area in hectares must be greater than zero.");
        }

        var resource = _databaseService.GetResourceByCultureType(cropType);

        if (resource == null)
        {
            throw new KeyNotFoundException($"No resource found for crop type: {cropType}");
        }

        return resource.RequiredMachines.Count;
    }
     
    public double EstimateFuelConsumption(MachineType machineType, double areaInHectares)
    {
        if (areaInHectares <= 0)
        {
            throw new ArgumentException(nameof(areaInHectares), "Area in hectares must be greater than zero.");
        }
        var concreteMachine=_databaseService.GetMachineByMachineType(machineType);
        return concreteMachine.FuelConsumption * areaInHectares;

    }

    public int CalculateRequiredWorkers(CultureType cropType, double areaInHectares)
    {
        if (areaInHectares <= 0)
        {
            throw new ArgumentException(nameof(areaInHectares), "Area in hectares must be greater than zero.");
        }

        var resource = _databaseService.GetResourceByCultureType(cropType);

        if (resource == null)
        {
            throw new KeyNotFoundException($"No resource found for crop type: {cropType}");
        }

        return (int)Math.Ceiling(resource.WorkerPerHectare * areaInHectares);
    }

    public double EstimateWorkDuration(double areaInHectares, int workersCount, MachineType machineryType, CultureType cropType)
    {
        if (areaInHectares <= 0 || workersCount<=0)
        { 
            throw new ArgumentException("Area in hectares and workers count must be greater than zero.");
        }
        var resource = _databaseService.GetResourceByCultureType(cropType);
        var concreteMachine = _databaseService.GetMachineByMachineType(machineryType);
        if(resource == null || concreteMachine==null)
        {
            throw new KeyNotFoundException($"No machine are found");
        }
        double durationOfWorkerWork = resource.WorkerWorkDuralityPerHectare / workersCount * areaInHectares;
        double durationOfMachineWork = concreteMachine.WorkDuralityPerHectare * areaInHectares;
        return durationOfWorkerWork + durationOfMachineWork;
    }

    public decimal CalculateBonus(int workerId)
    {
        if (workerId <= 0)
        { 
            throw new ArgumentException("Worker id must be greater than zero. "); 
        }
        var worker = _databaseService.GetWorkerById(workerId);
        if (worker==null)
        {
            throw new KeyNotFoundException("Worker with such Id is not found");
        }
        decimal salary;
        salary=worker.HourlyRate*worker.HoursWorked;
        var tasks = _databaseService.GetTasksByWorkerId(workerId);
        decimal bonusPerDay = 0.02m;
        decimal sumOfBonuses = 0;
        foreach ( var task in tasks)
        {
            var differenceInDays=(task.EstimatesEndDate - task.RealEndDate).Days;
            decimal bonus;
            if (differenceInDays<=0)
            {
                bonus = 0;
            }
            else
            {
                bonus=differenceInDays*bonusPerDay;
                if (bonus>0.06m)
                    bonus=0.06m;
                sumOfBonuses+=bonus;
            }
            if(bonus > 0.06m)
                bonus = 0.06m;
            sumOfBonuses += bonus;
        }
        var worker = _databaseService.GetWorkerById(workerId);
        if(worker==null)
        {
            throw new InvalidOperationException("Worker with such Id is not found");
        }

        var salary = worker.HourlyRate * worker.HoursWorked;
        return salary * sumOfBonuses;
    }

    // UNIMPLEMENTED METHODS
    
    // public double CalculateWorkerEfficiency(double plannedWork, double completedWork, TimeSpan actualTime)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public double CalculateFieldCost(CultureType cropType, double areaInHectares)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public double EstimateProfit(CultureType cropType, double areaInHectares, double marketPricePerTon)
    // {
    //     throw new NotImplementedException();
    // }
}