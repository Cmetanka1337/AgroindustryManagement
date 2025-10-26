using System;
using System.Collections.Generic;
using AgroindustryManagement.Models;

namespace AgroindustryManagement.Services.Database;

public class AGDatabaseService : IAGDatabaseService
{
    private readonly AGDatabaseContext _context;
    public AGDatabaseService(AGDatabaseContext context)
    {
        _context = context;
    }
    public Field GetFieldById(int fieldId)
    {
        if (fieldId <= 0)
        {
            throw new ArgumentException("Field ID must be a positive integer.", nameof(fieldId));
        }
        var field = _context.Fields.FirstOrDefault(f => f.Id == fieldId);

        if (field == null)
        {
            throw new KeyNotFoundException($"Field with ID {fieldId} not found.");
        }

        return field;
    }

    public IEnumerable<Field> GetAllFields()
    {
        var fields = _context.Fields.ToList();

        if (fields.Count == 0)
        {
            return Enumerable.Empty<Field>();
        }

        return fields;
    }

    public void AddField(Field field)
    {
        if (field == null)
        {
            throw new ArgumentNullException(nameof(field), "Field cannot be null.");
        }

        _context.Fields.Add(field);
        _context.SaveChanges();
    }

    public void UpdateField(Field field)
    {
        if (field == null)
        {
            throw new ArgumentNullException(nameof(field), "Field cannot be null.");
        }

        var existingField = _context.Fields.FirstOrDefault(f => f.Id == field.Id);

        if (existingField == null)
        {
            throw new KeyNotFoundException($"Field with ID {field.Id} not found.");
        }
        existingField.Culture = field.Culture;
        existingField.Area = field.Area;
        existingField.Status = field.Status;
        existingField.Workers = field.Workers;
        existingField.Machines = field.Machines;
        existingField.Tasks = field.Tasks;

        _context.SaveChanges();
    }

    public void DeleteField(int fieldId)
    {
        if (fieldId <= 0)
        {
            throw new ArgumentException("Field ID must be a positive integer.", nameof(fieldId));
        }

        var field = _context.Fields.FirstOrDefault(f => f.Id == fieldId);

        if (field == null)
        {
            throw new KeyNotFoundException($"Field with ID {fieldId} not found.");
        }

        _context.Fields.Remove(field);
        _context.SaveChanges();
    }

    public Worker GetWorkerById(int workerId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Worker> GetAllWorkers()
    {
        throw new NotImplementedException();
    }

    public void AddWorker(Worker worker)
    {
        throw new NotImplementedException();
    }

    public void UpdateWorker(Worker worker)
    {
        throw new NotImplementedException();
    }

    public void DeleteWorker(int workerId)
    {
        throw new NotImplementedException();
    }

    public Machine GetMachineById(int machineId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Machine> GetAllMachines()
    {
        throw new NotImplementedException();
    }

    public void AddMachine(Machine machine)
    {
        throw new NotImplementedException();
    }

    public void UpdateMachine(Machine machine)
    {
        throw new NotImplementedException();
    }

    public void DeleteMachine(int machineId)
    {
        throw new NotImplementedException();
    }

    public InventoryItem GetInventoryItemById(int itemId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<InventoryItem> GetAllInventoryItems()
    {
        throw new NotImplementedException();
    }

    public void AddInventoryItem(InventoryItem item)
    {
        throw new NotImplementedException();
    }

    public void UpdateInventoryItem(InventoryItem item)
    {
        throw new NotImplementedException();
    }

    public void DeleteInventoryItem(int itemId)
    {
        throw new NotImplementedException();
    }

    public WorkerTask GetWorkerTaskById(int taskId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<WorkerTask> GetAllWorkerTasks()
    {
        throw new NotImplementedException();
    }

    public void AddWorkerTask(WorkerTask task)
    {
        throw new NotImplementedException();
    }

    public void UpdateWorkerTask(WorkerTask task)
    {
        throw new NotImplementedException();
    }

    public void DeleteWorkerTask(int taskId)
    {
        throw new NotImplementedException();
    }
    public Resource GetResourceByCultureType(CultureType cultureType)
    {
        if (!Enum.IsDefined(typeof(CultureType), cultureType))
        {
            throw new ArgumentException("Invalid culture type.", nameof(cultureType));
        }
        var resource = _context.Resources
            .FirstOrDefault(r => r.CultureType == cultureType);

        if (resource == null)
        {
            throw new KeyNotFoundException($"Resource for culture type {cultureType} not found.");
        }
        return resource;
    }
    public IEnumerable<InventoryItem> GetCriticalInventoryItems()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<WorkerTask> GetTasksByWorkerId(int workerId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Machine> GetAvailableMachines()
    {
        throw new NotImplementedException();
    }
}