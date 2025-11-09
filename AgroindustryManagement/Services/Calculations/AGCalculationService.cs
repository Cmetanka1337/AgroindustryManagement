using AgroindustryManagement.Models;
using AgroindustryManagement.Services.Database;
using System;

namespace AgroindustryManagement.Services.Calculations;

public class AGCalculationService : IAGCalculationService
{
    private readonly IAGDatabaseService _databaseService;
    public AGCalculationService(IAGDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }
    public double CalculateSeedAmount(string cropType, double areaInHectares)
    {
        if (string.IsNullOrEmpty(cropType))
        {
            throw new ArgumentException("Crop type cannot be null or empty.", nameof(cropType));
        }
        if (areaInHectares <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(areaInHectares), "Area in hectares must be greater than zero.");
        }

        var culture = Enum.Parse<Models.CultureType>(cropType, true);

        var resource = _databaseService.GetResourceByCultureType(culture);

        if (resource == null)
        {
            throw new InvalidOperationException($"No resource found for crop type: {cropType}");
        }

        return resource.SeedPerHectare * areaInHectares;
    }
    //TODO if we want this be implemented, we need to have fertilizer data in Resource model
    public double CalculateFertilizerAmount(string cropType, double areaInHectares)
    {
        if (string.IsNullOrEmpty(cropType))
        {
            throw new ArgumentException("Crop type cannot be null or empty.", nameof(cropType));
        }
        if (areaInHectares <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(areaInHectares), "Area in hectares must be greater than zero.");
        }
        var culture = Enum.Parse<Models.CultureType>(cropType, true);
        var resource = _databaseService.GetResourceByCultureType(culture);
        if (resource == null)
        {
            throw new InvalidOperationException($"No resource found for crop type: {cropType}");
        }
        return resource.FertilizerPerHectare*areaInHectares;
    }

    public double EstimateYield(string cropType, double areaInHectares)
    {
        if (string.IsNullOrEmpty(cropType))
        {
            throw new ArgumentException("Crop type cannot be null or empty.", nameof(cropType));
        }
        if (areaInHectares <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(areaInHectares), "Area in hectares must be greater than zero.");
        }

        var culture = Enum.Parse<Models.CultureType>(cropType, true);
        var resource = _databaseService.GetResourceByCultureType(culture);
        if(resource == null)
        {
            throw new InvalidOperationException($"No resource found for crop type: {cropType}");
        }
        return resource.Yield*areaInHectares;
    }

    public int CalculateRequiredMachineryCount(string cropType, double areaInHectares)
    {
        if (string.IsNullOrEmpty(cropType))
        {
            throw new ArgumentException("Crop type cannot be null or empty.", nameof(cropType));
        }
        if (areaInHectares <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(areaInHectares), "Area in hectares must be greater than zero.");
        }

        var culture = Enum.Parse<Models.CultureType>(cropType, true);

        var resource = _databaseService.GetResourceByCultureType(culture);

        if (resource == null)
        {
            throw new InvalidOperationException($"No resource found for crop type: {cropType}");
        }

        return resource.RequiredMachines.Count;
    }
    //TODO if we want this be implemented, we need to have fuel consumption data in Machine model 
    public double EstimateFuelConsumption(string machineType, double areaInHectares)
    {
        if(string.IsNullOrEmpty(machineType))
        {
            throw new ArgumentException(nameof(machineType), "Machine type cannot be null or empty.");
        }
        if (areaInHectares <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(areaInHectares), "Area in hectares must be greater than zero.");
        }
        var machine=Enum.Parse<Models.MachineType>(machineType, true);
        var concreteMachine=_databaseService.GetMachineByMachineType(machine);
        return concreteMachine.FuelConsumption*areaInHectares;

    }

    public int CalculateRequiredWorkers(string cropType, double areaInHectares)
    {
        if (string.IsNullOrEmpty(cropType))
        {
            throw new ArgumentException("Crop type cannot be null or empty.", nameof(cropType));
        }
        if (areaInHectares <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(areaInHectares), "Area in hectares must be greater than zero.");
        }

        var culture = Enum.Parse<Models.CultureType>(cropType, true);

        var resource = _databaseService.GetResourceByCultureType(culture);

        if (resource == null)
        {
            throw new InvalidOperationException($"No resource found for crop type: {cropType}");
        }

        return (int)Math.Ceiling(resource.WorkerPerHectare * areaInHectares);
    }

    public double EstimateWorkDuration(double areaInHectares, int workersCount, string machineryType, string cropType)
    {
        if (string.IsNullOrEmpty(cropType) || string.IsNullOrEmpty(machineryType))
        {
            throw new ArgumentException("Crop type and machine type cannot be null or empty.");
        }
        if (areaInHectares <= 0 || workersCount<=0)
        { 
            throw new ArgumentOutOfRangeException("Area in hectares and workers count must be greater than zero.");
        }
        var machine=Enum.Parse<Models.MachineType>(machineryType, true);
        var culture=Enum.Parse<Models.CultureType>(cropType, true);
        var resource=_databaseService.GetResourceByCultureType(culture);
        var concreteMachine=_databaseService.GetMachineByMachineType(machine);
        if(resource==null || concreteMachine==null)
        {
            throw new InvalidOperationException($"No resource or machine are found");
        }
        double duralityOfWorkerWork=(resource.WorkerWorkDuralityPerHectare/workersCount)*areaInHectares;
        double duralityOfMachineWork=concreteMachine.WorkDuralityPerHectare*areaInHectares;
        return duralityOfWorkerWork+duralityOfMachineWork;
    }

    public decimal CalculateBonus(int workerId)
    {
        if(workerId<=0)
        { 
            throw new ArgumentOutOfRangeException("Worker id must be greater than zero. "); 
        }
        var tasks = _databaseService.GetTasksByWorkerId(workerId);
        decimal bonusPerDay = 0.02m;
        decimal sumOfBonuses = 0;
        decimal bonus;
        foreach ( var task in tasks)
        {
            var differenceInDays=(task.EstimatesEndDate - task.RealEndDate).Days;
            if (differenceInDays<=0)
            {
                bonus=0;
            }
            else
            {
                bonus=differenceInDays*bonusPerDay;
            }
            if(bonus>0.06m)
                bonus=0.06m;
            sumOfBonuses+=bonus;
        }
        var worker=_databaseService.GetWorkerById(workerId);
        if(worker==null)
        {
            throw new InvalidOperationException("Worker with such Id is not found");
        }
        decimal salary;
        salary=worker.HourlyRate*worker.HoursWorked;
        decimal finalBonus = salary*sumOfBonuses;
        return finalBonus;
    }

    public double CalculateWorkerEfficiency(double plannedWork, double completedWork, TimeSpan actualTime)
    {
        throw new NotImplementedException();
    }
    //TODO if we want this be implemented, we need to have cost data in Resource and Machine models
    public double CalculateFieldCost(string cropType, double areaInHectares)
    {
        throw new NotImplementedException();
    }
    //TODO if we want this be implemented, we need to have enumeration data for market prices
    public double EstimateProfit(string cropType, double areaInHectares, double marketPricePerTon)
    {
        throw new NotImplementedException();
    }
}