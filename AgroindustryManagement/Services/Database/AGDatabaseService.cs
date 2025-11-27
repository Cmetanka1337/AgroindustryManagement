using AgroindustryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace AgroindustryManagement.Services.Database;

public class AGDatabaseService : IAGDatabaseService
{
    private readonly AGDatabaseContext _context;
    public AGDatabaseService(AGDatabaseContext context)
    {
        _context = context;
    }
    public Field? GetFieldById(int fieldId)
    {
        return fieldId <= 0 ? null : _context.Fields.FirstOrDefault(field => field.Id == fieldId);
    }

    public IEnumerable<Field> GetAllFields()
    {
        var fields = _context.Fields
            .Include(f => f.Workers)
            .Include(f => f.Machines)
            .Include(f => f.Tasks)
            .ToList();;

        return fields.Count == 0 ? Enumerable.Empty<Field>() : fields;
    }

    public void AddField(Field field)
    {
        if (_context.Fields.FirstOrDefault(dbField => dbField.Id == field.Id) != null) 
            return;
        
        _context.Fields.Add(field);
        _context.SaveChanges();
    }

    public void UpdateField(Field field)
    {
        var existingField = _context.Fields.FirstOrDefault(dbField => dbField.Id == field.Id);

        if (existingField == null)
        {
            return;
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
            return;
        }

        var field = _context.Fields.FirstOrDefault(dbField => dbField.Id == fieldId);

        if (field == null)
        {
            return;
        }

        _context.Fields.Remove(field);
        _context.SaveChanges();
    }

    public Worker? GetWorkerById(int workerId)
    {
        return workerId <= 0 ? null : _context.Workers.FirstOrDefault(dbWorker => dbWorker.Id == workerId);
    }

    public IEnumerable<Worker> GetAllWorkers()
    {
        var workers = _context.Workers.ToList();

        return workers.Count == 0 ? Enumerable.Empty<Worker>() : workers;
    }

    public void AddWorker(Worker worker)
    {
        if (_context.Workers.FirstOrDefault(dbWorker => dbWorker.Id == worker.Id) != null) 
            return;
        
        _context.Workers.Add(worker);
        _context.SaveChanges();
    }

    public void UpdateWorker(Worker worker)
    {
        var existingWorker = _context.Workers.FirstOrDefault(dbWorker => dbWorker.Id == worker.Id);
        if (existingWorker == null)
        {
            return;
        }
        
        existingWorker.HoursWorked = worker.HoursWorked;
        existingWorker.HourlyRate = worker.HourlyRate;
        existingWorker.Age = worker.Age;
        existingWorker.IsActive = worker.IsActive;
        existingWorker.Tasks = worker.Tasks;
        existingWorker.FirstName = worker.FirstName;
        existingWorker.LastName = worker.LastName;
        
        _context.SaveChanges();
    }

    public void DeleteWorker(int workerId)
    {
        if (workerId <= 0)
        {
            return;
        }
        
        var workerExist = _context.Workers.FirstOrDefault(dbWorker => dbWorker.Id == workerId);
        if (workerExist == null)
        {
            return;
        }
        
        _context.Workers.Remove(workerExist);
        _context.SaveChanges();
    }

    public Machine? GetMachineById(int machineId)
    {
        return machineId <= 0 ? null : _context.Machines.FirstOrDefault(dbMachine => dbMachine.Id == machineId);
    }

    public IEnumerable<Machine> GetAllMachines()
    {
        var machines = _context.Machines.ToList();
        return machines.Count == 0 ? Enumerable.Empty<Machine>() : machines;
    }
    public void AddMachine(Machine machine)
    {
        if (_context.Machines.FirstOrDefault(dbMachine => dbMachine.Id == machine.Id) != null) 
            return;
        
        _context.Machines.Add(machine);
        _context.SaveChanges();
    }

    public void UpdateMachine(Machine machine)
    {
        var existingMachine = _context.Machines.FirstOrDefault(dbMachine => dbMachine.Id == machine.Id);
        if(existingMachine == null)
        {
            return;
        }
        
        existingMachine.Field = machine.Field;
        existingMachine.Resource = machine.Resource;
        existingMachine.IsAvailable=machine.IsAvailable;
        existingMachine.Type= machine.Type;
        existingMachine.FuelConsumption= machine.FuelConsumption;
        existingMachine.WorkDuralityPerHectare= machine.WorkDuralityPerHectare;
        
        _context.SaveChanges();
    }

    public void DeleteMachine(int machineId)
    {
        if (machineId <= 0)
        {
            return;
        }
        
        var machine = _context.Machines.FirstOrDefault(dbMachine => dbMachine.Id == machineId);
        if (machine == null)
        {
            return;
        }
        
        _context.Machines.Remove(machine);
        _context.SaveChanges();
    }

    public InventoryItem? GetInventoryItemById(int itemId)
    {
        return itemId <= 0 ? null : _context.InventoryItems.FirstOrDefault(dbInventoryItem => dbInventoryItem.Id == itemId);
    }

    public IEnumerable<InventoryItem> GetAllInventoryItems()
    {
        var items=_context.InventoryItems.ToList();
        return items.Count==0 ? Enumerable.Empty<InventoryItem>() : items;
    }

    public void AddInventoryItem(InventoryItem item)
    {
        if (_context.InventoryItems.FirstOrDefault(dbInventoryItem => dbInventoryItem.Id == item.Id) != null) 
            return;
        
        _context.InventoryItems.Add(item);
        _context.SaveChanges();
    }

    public void UpdateInventoryItem(InventoryItem item)
    {
        var existingItem=_context.InventoryItems.FirstOrDefault(dbInventoryItem => dbInventoryItem.Id == item.Id);
        if (existingItem == null)
        {
            return;
        }
        
        existingItem.Name = item.Name;
        existingItem.Quantity = item.Quantity;
        existingItem.Unit = item.Unit;
        existingItem.Warehouse = item.Warehouse;
        
        _context.SaveChanges();
    }

    public void DeleteInventoryItem(int itemId)
    {
        if(itemId<=0)
        {
            throw new ArgumentException("Id must be positive", nameof(itemId));
        }
        
        var item= _context.InventoryItems.FirstOrDefault(dbInventoryItem => dbInventoryItem.Id == itemId);
        if (item == null)
        {
            return;
        }
        
        _context.InventoryItems.Remove(item);
        _context.SaveChanges();
    }

    public WorkerTask? GetWorkerTaskById(int taskId)
    {
        return taskId <= 0 ? null : _context.WorkerTasks.FirstOrDefault(dbTask => dbTask.Id == taskId);;
    }

    public IEnumerable<WorkerTask> GetAllWorkerTasks()
    {
        var tasks = _context.WorkerTasks.ToList();
        return tasks.Count == 0 ? Enumerable.Empty<WorkerTask>() : tasks;
    }

    public void AddWorkerTask(WorkerTask task)
    {
        if (_context.WorkerTasks.FirstOrDefault(dbTask => dbTask.Id == task.Id) != null) 
            return;
        
        _context.WorkerTasks.Add(task);
        _context.SaveChanges();
    }

    public void UpdateWorkerTask(WorkerTask task)
    {
        var existingWorkerTask = _context.WorkerTasks.FirstOrDefault(dbTask => dbTask.Id == task.Id);
        if (existingWorkerTask == null)
        {
            return;
        }
        
        existingWorkerTask.Worker = task.Worker;
        existingWorkerTask.Field = task.Field;
        existingWorkerTask.Description = task.Description;
        existingWorkerTask.EstimatesEndDate = task.EstimatesEndDate;
        existingWorkerTask.Progress = task.Progress;
        existingWorkerTask.TaskType = task.TaskType;
        
        _context.SaveChanges();
    }

    public void DeleteWorkerTask(int taskId)
    {
        if (taskId <= 0)
        {
            throw new ArgumentException("ID must be positive", nameof(taskId));
        }
        
        var task= _context.WorkerTasks.FirstOrDefault(dbTask => dbTask.Id == taskId);
        if (task == null)
        {
            return;
        }
        
        _context.WorkerTasks.Remove(task);
        _context.SaveChanges();   
    }
    
    public Resource? GetResourceById(int resourceId)
    {
        return resourceId <= 0 ? null : _context.Resources.FirstOrDefault(dbResource => dbResource.Id == resourceId);
    }
    
    public void AddResource(Resource resource)
    {
        if (_context.Resources.FirstOrDefault(dbResource => dbResource.Id == resource.Id) != null) 
            return;
        
        _context.Resources.Add(resource);
        _context.SaveChanges();
    }
    
    public void EditResource(Resource resource)
    {
        var existingResource = _context.Resources.FirstOrDefault(dbResource => dbResource.Id == resource.Id);
        if (existingResource == null)
        {
            return;
        }
        
        existingResource.CultureType = resource.CultureType;
        existingResource.SeedPerHectare = resource.SeedPerHectare;
        existingResource.FertilizerPerHectare = resource.FertilizerPerHectare;
        existingResource.WorkerPerHectare = resource.WorkerPerHectare;
        existingResource.WorkerWorkDuralityPerHectare = resource.WorkerWorkDuralityPerHectare;
        existingResource.Yield = resource.Yield;
        existingResource.RequiredMachines = resource.RequiredMachines;
        
        _context.SaveChanges();
    }
    
    public void DeleteResource(int resourceId)
    {
        if (resourceId <= 0)
        {
            return;
        }
        
        var resource = _context.Resources.FirstOrDefault(dbResource => dbResource.Id == resourceId);
        if (resource == null)
        {
            return;
        }
        
        _context.Resources.Remove(resource);
        _context.SaveChanges();
    }
    
    public IEnumerable<Resource> GetAllResources()
    {
        var resources = _context.Resources.ToList();
        return resources.Count == 0 ? Enumerable.Empty<Resource>() : resources;
    }
    
    public Warehouse? GetWarehouseById(int warehouseId)
    {
        return warehouseId <= 0 ? null : _context.Warehouses.FirstOrDefault(dbWarehouse => dbWarehouse.Id == warehouseId);
    }
    
    public IEnumerable<Warehouse> GetAllWarehouses()
    {
        var warehouses = _context.Warehouses.ToList();
        return warehouses.Count == 0 ? Enumerable.Empty<Warehouse>() : warehouses;
    }
    
    public void AddWarehouse(Warehouse warehouse)
    {
        if (_context.Warehouses.FirstOrDefault(dbWarehouse => dbWarehouse.Id == warehouse.Id) != null) 
            return;
        
        _context.Warehouses.Add(warehouse);
        _context.SaveChanges();
    }
    
    public void UpdateWarehouse(Warehouse warehouse)
    {
        var existingWarehouse = _context.Warehouses.FirstOrDefault(dbWarehouse => dbWarehouse.Id == warehouse.Id);
        if (existingWarehouse == null)
        {
            return;
        }
        
        existingWarehouse.InventoryItems = warehouse.InventoryItems;
        
        _context.SaveChanges();
    }
    
    public void DeleteWarehouse(int warehouseId)
    {
        if (warehouseId <= 0)
        {
            throw new ArgumentException("Id must be positive", nameof(warehouseId));
        }
        
        var warehouse = _context.Warehouses.FirstOrDefault(dbWarehouse => dbWarehouse.Id == warehouseId);
        if (warehouse == null)
        {
            return;
        }
        
        _context.Warehouses.Remove(warehouse);
        _context.SaveChanges();
    }
    
    public Resource? GetResourceByCultureType(CultureType cultureType)
    {
        return _context.Resources.FirstOrDefault(resource => resource.CultureType == cultureType);
    }
    public Machine? GetMachineByMachineType(MachineType machineType)
    {
        return _context.Machines.FirstOrDefault(machine => machine.Type == machineType);
    }
    public IEnumerable<InventoryItem> GetCriticalInventoryItems()
    {
        var items=_context.InventoryItems.ToList();
        return items.Count == 0 ? Enumerable.Empty<InventoryItem>() : items.Where(item => item.Quantity < 5).ToList();
    }

    public IEnumerable<WorkerTask> GetTasksByWorkerId(int workerId)
    {
        if (workerId <= 0)
        {
            return Enumerable.Empty<WorkerTask>();
        }
        var tasksById = _context.WorkerTasks.Where(workerTask => workerTask.Worker.Id == workerId).ToList();

        return tasksById.Count == 0 ? Enumerable.Empty<WorkerTask>() : tasksById;
    }

    public IEnumerable<Machine> GetAvailableMachines()
    {
        var availableMachines= _context.Machines.Where(machine => machine.IsAvailable).ToList();
        return availableMachines.Count == 0 ? Enumerable.Empty<Machine>() : availableMachines;
    }
}