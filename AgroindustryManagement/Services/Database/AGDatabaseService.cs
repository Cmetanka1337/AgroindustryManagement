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
            throw new ArgumentException("Worker is null", nameof(worker));
        }
        _context.Workers.Add(worker);
        _context.SaveChanges();
    }

    public void UpdateWorker(Worker worker)
    {
        if (worker == null)
        {
            throw new ArgumentException("Worker is null", nameof(worker));
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
            throw new ArgumentException("Machine is null", nameof(machine));
        }
        _context.Machines.Add(machine);
        _context.SaveChanges();
    }

    public void UpdateMachine(Machine machine)
    {
        if (machine == null)
        {
            throw new ArgumentException("Machine is null", nameof(machine));
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