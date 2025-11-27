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
            return -1;
        }
        
        var resource = _databaseService.GetResourceByCultureType(cropType);

        if (resource == null)
        {
            return -1;
        }

        var seedAmount = resource.SeedPerHectare * areaInHectares;

        return seedAmount;
    }
    
    public double CalculateFertilizerAmount(CultureType cropType, double areaInHectares)
    {
        if (areaInHectares <= 0)
        {
            return -1;
        }
        var resource = _databaseService.GetResourceByCultureType(cropType);
        if (resource == null)
        {
            return -1;
        }
        return resource.FertilizerPerHectare*areaInHectares;
    }

    public double EstimateYield(CultureType cropType, double areaInHectares)
    {
        if (areaInHectares <= 0)
        {
            return -1;
        }

        var resource = _databaseService.GetResourceByCultureType(cropType);
        if(resource == null)
        {
            return -1;
        }
        return resource.Yield * areaInHectares;
    }

    public int CalculateRequiredMachineryCount(CultureType cropType, double areaInHectares)
    {
        if (areaInHectares <= 0)
        {
            return -1;
        }

        var resource = _databaseService.GetResourceByCultureType(cropType);

        if (resource == null)
        {
            return -1;
        }

        return resource.RequiredMachines.Count;
    }
     
    public double EstimateFuelConsumption(MachineType machineType, double areaInHectares)
    {
        if (areaInHectares <= 0)
        {
            return -1;
        }
        var concreteMachine=_databaseService.GetMachineByMachineType(machineType);
        
        if (concreteMachine == null) 
        {
            return -1;
        }

        return concreteMachine.FuelConsumption * areaInHectares;
    }

    public int CalculateRequiredWorkers(CultureType cropType, double areaInHectares)
    {
        if (areaInHectares <= 0)
        {
            return -1;
        }

        var resource = _databaseService.GetResourceByCultureType(cropType);

        if (resource == null)
        {
            return -1;
        }

        return (int)Math.Ceiling(resource.WorkerPerHectare * areaInHectares);
    }

    public double EstimateWorkDuration(double areaInHectares, int workersCount, MachineType machineryType, CultureType cropType)
    {
        if (areaInHectares <= 0 || workersCount<=0)
        { 
            return -1;
        }
        var resource = _databaseService.GetResourceByCultureType(cropType);
        var concreteMachine = _databaseService.GetMachineByMachineType(machineryType);
        if(resource == null || concreteMachine==null)
        {
            return -1;
        }
        double durationOfWorkerWork = resource.WorkerWorkDuralityPerHectare / workersCount * areaInHectares;
        double durationOfMachineWork = concreteMachine.WorkDuralityPerHectare * areaInHectares;
        return durationOfWorkerWork + durationOfMachineWork;
    }

    public decimal CalculateBonus(int workerId)
    {
        if (workerId <= 0)
        { 
            return -1; 
        }
        var worker = _databaseService.GetWorkerById(workerId);
        if (worker==null)
        {
            return -1;
        }
        decimal salary;
        salary=worker.HourlyRate*worker.HoursWorked;
        var tasks = _databaseService.GetTasksByWorkerId(workerId);
        decimal bonusPerDay = 0.02m;
        decimal sumOfBonuses = (decimal)0.1;
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
        worker = _databaseService.GetWorkerById(workerId);
        if(worker==null)
        {
            return -1;
        }

        salary = worker.HourlyRate * worker.HoursWorked;
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