using System;
using AgroindustryManagement.Models;

namespace AgroindustryManagement.Services.Calculations;

public interface IAGCalculationService
{
    /// <summary>
    /// Calculates the required amount of seeds for a given crop and area (in kg or tons).
    /// </summary>
    double CalculateSeedAmount(CultureType cropType, double areaInHectares);

    /// <summary>
    /// Calculates the amount of fertilizers or plant protection products needed for the area.
    /// </summary>
    double CalculateFertilizerAmount(CultureType cropType, double areaInHectares);

    /// <summary>
    /// Estimates the potential yield for a given area, depending on the crop and weather conditions.
    /// </summary>
    double EstimateYield(CultureType cropType, double areaInHectares);

    /// <summary>
    /// Determines the required number of machines to process a specific area.
    /// </summary>
    int CalculateRequiredMachineryCount(CultureType cropType, double areaInHectares);

    /// <summary>
    /// Estimates fuel consumption for field processing.
    /// </summary>
    double EstimateFuelConsumption(MachineType machineType, double areaInHectares);

    /// <summary>
    /// Determines the required number of workers to complete the task.
    /// </summary>
    int CalculateRequiredWorkers(CultureType cropType, double areaInHectares);

    /// <summary>
    /// Calculates the duration of work (in hours or days) for a specific area.
    /// </summary>
    double EstimateWorkDuration(double areaInHectares, int workersCount, MachineType machineryType, CultureType cropType);

    /// <summary>
    /// Calculates the worker's bonus for exceeding the plan.
    /// </summary>
    decimal CalculateBonus(int workerId);

    /// <summary>
    /// Calculates the worker's efficiency coefficient.
    /// </summary>
    double CalculateWorkerEfficiency(double plannedWork, double completedWork, TimeSpan actualTime);

    /// <summary>
    /// Estimates the cost of all resources for a given field.
    /// </summary>
    double CalculateFieldCost(CultureType cropType, double areaInHectares);

    /// <summary>
    /// Calculates the projected profit considering costs and yield.
    /// </summary>
    double EstimateProfit(CultureType cropType, double areaInHectares, double marketPricePerTon);
}