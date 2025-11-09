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
            throw new ArgumentException("Area in hectares must be greater than zero.");
        }

        var culture = Enum.Parse<Models.CultureType>(cropType, true);

        var resource = _databaseService.GetResourceByCultureType(culture);

        if (resource == null)
        {
            throw new InvalidOperationException($"No resource found for crop type: {cropType}");
        }

        var seedAmount = resource.SeedPerHectare * areaInHectares;

        return seedAmount;
    }
    //TODO if we want this be implemented, we need to have fertilizer data in Resource model
    public double CalculateFertilizerAmount(string fertilizerType, double areaInHectares)
    {
        throw new NotImplementedException();
    }

    public double EstimateYield(string cropType, double areaInHectares)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
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

    public double EstimateWorkDuration(double areaInHectares, int workersCount, string machineryType)
    {
        throw new NotImplementedException();
    }

    public double CalculateBonus(double plannedArea, double actualArea, double baseSalary)
    {
        throw new NotImplementedException();
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