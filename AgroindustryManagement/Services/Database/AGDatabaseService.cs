using AgroindustryManagement.Models;
using System.Globalization;

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
        if (workerId <= 0)
        {
            throw new ArgumentException("Invalid id", nameof(workerId));
        }
        var worker = _context.Workers.FirstOrDefault(f => f.Id == workerId);
        if (worker == null)
        {
            throw new KeyNotFoundException("Worker with such id is not found");
        }
        return worker;
    }

    public IEnumerable<Worker> GetAllWorkers()
    {
        var workers = new List<Worker>();
        workers = _context.Workers.ToList();

        if (workers.Count == 0)
        {
            return Enumerable.Empty<Worker>();
        }

        return workers;
    }

    public void AddWorker(Worker worker)
    {
        if (worker == null)
        {
            throw new ArgumentNullException("Worker is null", nameof(worker));
        }
        _context.Workers.Add(worker);
        _context.SaveChanges();
    }

    public void UpdateWorker(Worker worker)
    {
        if (worker == null)
        {
            throw new ArgumentNullException("Worker is null", nameof(worker));
        }
        var workerExist = _context.Workers.FirstOrDefault(f => f.Id == worker.Id);
        if (workerExist == null)
        {
            throw new KeyNotFoundException("Worker is not found");
        }
        workerExist.HoursWorked = worker.HoursWorked;
        workerExist.HourlyRate = worker.HourlyRate;
        workerExist.Age = worker.Age;
        workerExist.IsActive = worker.IsActive;
        workerExist.Tasks = worker.Tasks;
        workerExist.FirstName = worker.FirstName;
        workerExist.LastName = worker.LastName;
        _context.SaveChanges();
    }

    public void DeleteWorker(int workerId)
    {
        if (workerId <= 0)
        {
            throw new ArgumentException("Worker id must be positive", nameof(workerId));
        }
        var workerExist = _context.Workers.FirstOrDefault(f => f.Id == workerId);
        if (workerExist == null)
        {
            throw new KeyNotFoundException("Such worker is not found");
        }
        _context.Workers.Remove(workerExist);
        _context.SaveChanges();
    }

    public Machine GetMachineById(int machineId)
    {
        if (machineId <= 0)
        {
            throw new ArgumentException("Id must be positive", nameof(machineId));
        }
        var machineExist = _context.Machines.FirstOrDefault(f => f.Id == machineId);
        if (machineExist == null)
        {
            throw new KeyNotFoundException("Machine is not found");
        }
        return machineExist;
    }

    public IEnumerable<Machine> GetAllMachines()
    {
        var machines = _context.Machines.ToList();
        if (machines.Count == 0)
        {
            return Enumerable.Empty<Machine>();
        }
        return machines;
    }
    public void AddMachine(Machine machine)
    {
        if(machine == null)
        {
            throw new ArgumentNullException("Machine is null", nameof(machine));
        }
        _context.Machines.Add(machine);
        _context.SaveChanges();
    }

    public void UpdateMachine(Machine machine)
    {
        if (machine == null)
        {
            throw new ArgumentNullException("Machine is null", nameof(machine));
        }
        var existMachine = _context.Machines.FirstOrDefault(m => m.Id == machine.Id);
        if(existMachine == null)
        { 
            throw new KeyNotFoundException("Such machine is not found"); 
        }
        existMachine.AssignedToField = machine.AssignedToField;
        existMachine.Field = machine.Field;
        existMachine.Resource = machine.Resource;
        existMachine.ResourceId = machine.ResourceId;
        existMachine.IsAvailable=machine.IsAvailable;
        existMachine.Type= machine.Type;
        existMachine.FuelConsumption= machine.FuelConsumption;
        existMachine.WorkDuralityPerHectare= machine.WorkDuralityPerHectare;
        _context.SaveChanges();
    }

    public void DeleteMachine(int machineId)
    {
        if (machineId <= 0)
        {
            throw new ArgumentException("Id must be positive", nameof(machineId));
        }
        var machine = _context.Machines.FirstOrDefault(m=>m.Id==machineId);
        if (machine == null)
        { throw new KeyNotFoundException("Such machine is not found"); }
        _context.Machines.Remove(machine);
        _context.SaveChanges();
    }

    public InventoryItem GetInventoryItemById(int itemId)
    {
        if(itemId <= 0)
        { 
            throw new ArgumentException("Id must be positive",nameof(itemId)); 
        }
        var item = _context.InventoryItems.FirstOrDefault(m=>m.Id==itemId);
        if(item == null)
        {
            throw new KeyNotFoundException("Such item is not found");
        }
        return item;
    }

    public IEnumerable<InventoryItem> GetAllInventoryItems()
    {
        var items=_context.InventoryItems.ToList();
        if (items.Count==0)
        {
            return Enumerable.Empty<InventoryItem>();
        }
        return items;
    }

    public void AddInventoryItem(InventoryItem item)
    {
        if(item == null)
        {
            throw new ArgumentNullException("Item is null",nameof(item));
        }
        _context.InventoryItems.Add(item);
        _context.SaveChanges();
    }

    public void UpdateInventoryItem(InventoryItem item)
    {
        if (item == null)
        {
            throw new ArgumentNullException("Item is null", nameof(item));
        }
        var itemExist=_context.InventoryItems.FirstOrDefault(m => m.Id==item.Id);
        if (itemExist == null)
        {
            throw new KeyNotFoundException("There is no such item");
        }
        itemExist.Name = item.Name;
        itemExist.Quantity = item.Quantity;
        itemExist.Unit = item.Unit;
        itemExist.Warehouse = item.Warehouse;
        itemExist.WarehouseId = item.WarehouseId;
        _context.SaveChanges();
    }

    public void DeleteInventoryItem(int itemId)
    {
        if(itemId<=0)
        {
            throw new ArgumentException("Id must be positive", nameof(itemId));
        }
        var item= _context.InventoryItems.FirstOrDefault(i => i.Id==itemId);
        if (item == null)
        {
            throw new KeyNotFoundException("There is no such item");
        }
        _context.InventoryItems.Remove(item);
        _context.SaveChanges();
    }

    public WorkerTask GetWorkerTaskById(int taskId)
    {
        if (taskId <= 0)
        {
            throw new ArgumentException("ID must be positive", nameof(taskId));
        }
        var task = _context.WorkerTasks.FirstOrDefault(i => i.Id == taskId);
        if (task == null)
        {
            throw new KeyNotFoundException("There is no such task");
        }
        return task;
    }

    public IEnumerable<WorkerTask> GetAllWorkerTasks()
    {
        var tasks = _context.WorkerTasks.ToList();
        if(tasks.Count == 0)
        {
            return Enumerable.Empty<WorkerTask>();
        }
        return tasks;
    }

    public void AddWorkerTask(WorkerTask task)
    {
        if(task == null)
        {
            throw new ArgumentNullException("Task is null",nameof(task));
        }
        _context.WorkerTasks.Add(task);
        _context.SaveChanges();
    }

    public void UpdateWorkerTask(WorkerTask task)
    {
        if (task == null)
        {
            throw new ArgumentNullException("Task is null", nameof(task));
        }
        var workerTaskExist= _context.WorkerTasks.FirstOrDefault(t=>t.Id == task.Id);
        if (workerTaskExist == null)
        {
            throw new KeyNotFoundException("There is no such task");
        }
        workerTaskExist.StartDate = task.StartDate;
        workerTaskExist.Worker=task.Worker;
        workerTaskExist.WorkerId=task.WorkerId;
        workerTaskExist.FieldId=task.FieldId;
        workerTaskExist.Field=task.Field;
        workerTaskExist.Description=task.Description;
        workerTaskExist.EstimatesEndDate=task.EstimatesEndDate;
        workerTaskExist.Progress=task.Progress;
        workerTaskExist.TaskType=task.TaskType;
        _context.SaveChanges();
    }

    public void DeleteWorkerTask(int taskId)
    {
        if (taskId <= 0)
        {
            throw new ArgumentException("ID must be positive", nameof(taskId));
        }
        var task= _context.WorkerTasks.FirstOrDefault(t=>t.Id==taskId);
        if (task == null)
        {
            throw new KeyNotFoundException("There is no such task"); 
        }
        _context.WorkerTasks.Remove(task);
        _context.SaveChanges();   
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
    public Machine GetMachineByMachineType(MachineType machineType)
    {
        if(!Enum.IsDefined(typeof(MachineType), machineType))
        {
            throw new ArgumentException("Invalid machine type.", nameof(machineType));
        }
        var machine= _context.Machines.FirstOrDefault(m=>m.Type==machineType);
        if(machine == null)
        {
            throw new KeyNotFoundException($"Machine for machine type {machineType} not found.");
        }
        return machine;
    }
    public IEnumerable<InventoryItem> GetCriticalInventoryItems()
    {
        var items=_context.InventoryItems.ToList();
        if (items.Count == 0) 
        {
            return Enumerable.Empty<InventoryItem>();
        }
        var criticalItems= new List<InventoryItem>();
        foreach (var item in items)
        {
            if(item.Quantity<5)
            {
                criticalItems.Add(item);
            }
        }
        return criticalItems;
    }

    public IEnumerable<WorkerTask> GetTasksByWorkerId(int workerId)
    {
        if (workerId <= 0)
        {
            throw new ArgumentException("Id must be positive", nameof(workerId));
        }
        var tasksById = _context.WorkerTasks.Where(i => i.WorkerId == workerId).ToList();

        if (tasksById.Count == 0)
        {
            return Enumerable.Empty<WorkerTask>();
        }
        return tasksById;
    }

    public IEnumerable<Machine> GetAvailableMachines()
    {
        var availableMachines= _context.Machines.Where(i=>i.IsAvailable==true).ToList();
        if(availableMachines.Count == 0)
        { 
            return Enumerable.Empty<Machine>(); 
        }
        return availableMachines;
    }

}