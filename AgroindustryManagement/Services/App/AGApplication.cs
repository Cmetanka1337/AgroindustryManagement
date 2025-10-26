using AgroindustryManagement.Views;
using AgroindustryManagement.Models;
using AgroindustryManagement.Services.Database;

namespace AgroindustryManagement.Services.App;

public class AGApplication
{
    private readonly AGMenu _menu = new ();
    private readonly AGViewService _viewService = new ();
    private bool _isRunning;
    
    // TEMP
    static Resource resource = new Resource
            {
                Id = 0,
                CultureType = CultureType.Wheat,
                SeedPerHectare = 0,
                WorkerPerHectare = 0,
                RequiredMachines = [machine ?? new Machine()]
            };
    static Machine machine = new Machine
            {
                Id = 0,
                Type = MachineType.Tractor,
                IsAvailable = false,
                AssignedToField = null,
                Field = null,
                ResourceId = 0,
                Resource = resource
            };
            static  Field field = new Field
            {
                Id = 0,
                Area = 0,
                Culture = CultureType.Wheat,
                Status = FieldStatus.Planted,
                Workers = [worker ?? new Worker()],
                Machines = [machine],
                Tasks = [task ?? new WorkerTask()],
                CreatedAt = default
            };

            static Worker worker = new Worker
            {
                Id = 0,
                FirstName = "name",
                LastName = "surname",
                Age = 0,
                HourlyRate = 0,
                IsActive = true,
                Tasks = [task ?? new WorkerTask()],
                HoursWorked = 0
            };
            static WorkerTask task = new WorkerTask
            {
                Id = 0,
                Description = "description",
                WorkerId = 0,
                Worker = worker,
                FieldId = 0,
                Field = field,
                TaskType = TaskType.Planting,
                Progress = 0,
                StartDate = default,
                EstimatesEndDate = default
            };
    
    public void Start()
    {
        _menu.OptionSelected += OnOptionSelected;
        _isRunning = true;
        
        _menu.DisplayWelcomeMessage();
        while (_isRunning)
        {
            _menu.DisplayMainMenuOptions();
            _menu.StartSelectingPhase();
        }
    }

    // PRIVATE METHODS
    
    private void Stop()
    {
        _isRunning = false;
    }

    private void DisplayField()
    {
        
        worker.Tasks = [task];
        field.Workers = [worker];
        field.Machines = [machine];
        field.Tasks = [task];
        
        _viewService.DisplayFieldDetails(field);
    }
    
    private void OnOptionSelected(int option)
    {
        switch (option)
        {
            case (int)MainMenuOptions.DisplayField:
                DisplayField();
                break;
            case (int)MainMenuOptions.DisplayAllFields:
                break;
            
            case (int)MainMenuOptions.DisplayWorker:
                break;
            case (int)MainMenuOptions.DisplayAllWorkers:
                break;
            
            case (int)MainMenuOptions.DisplayMachine:
                break;
            case (int)MainMenuOptions.DisplayAllMachines:
                break;
            
            case (int)MainMenuOptions.DisplayInventoryItem:
                break;
            case (int)MainMenuOptions.DisplayAllInventoryItems:
                break;
            
            case (int)MainMenuOptions.DisplayWorkerTask:
                break;
            case (int)MainMenuOptions.DisplayAllWorkerTasks:
                break;
            
            case (int)MainMenuOptions.Exit:
                Stop();
                break;
        }
    }
}

enum MainMenuOptions
{
    // field
    DisplayField = 1,
    DisplayAllFields = 2,
    
    // worker
    DisplayWorker = 3,
    DisplayAllWorkers = 4,
    
    // machine
    DisplayMachine = 5,
    DisplayAllMachines = 6,
    
    // inventory item
    DisplayInventoryItem = 7,
    DisplayAllInventoryItems = 8,
    
    // worker task
    DisplayWorkerTask = 9,
    DisplayAllWorkerTasks = 10,
    
    Exit = 0
}