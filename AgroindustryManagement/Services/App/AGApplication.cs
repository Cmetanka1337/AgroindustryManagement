using AgroindustryManagement.Views;
using AgroindustryManagement.Models;
using AgroindustryManagement.Services.App.Menu;
using AgroindustryManagement.Services.App.Menu.MenuStateHandlers;
using AgroindustryManagement.Services.Database;

namespace AgroindustryManagement.Services.App;

public class AGApplication
{
    private readonly AGMenu _menu = new ();
    private readonly AGViewService _viewService = new ();
    private readonly AGDatabaseService _databaseService = new (context: Context);
    private static readonly AGDatabaseContext Context = new (); // Temporarily located here. Should be moved later.
    private bool _isRunning;
    private readonly Dictionary<string, IAGMenuStateHandler> _stateHandlers = new()
    {
        { AGMenuState.MainMenuState, new MainMenuStateHandler() },
        { AGMenuState.FieldMenuState, new AGFieldMenuStateHandler() },
        { AGMenuState.MachineMenuState, new AGMachineMenuStateHandler() },
        { AGMenuState.InventoryItemMenuState, new AGInventoryItemMenuStateHandler() },
        { AGMenuState.WorkerMenuState, new AGWorkerMenuStateHandler() },
        { AGMenuState.WorkerTaskMenuState, new AGWorkerTaskMenuStateHandler() }
    };
    
    // TEMP VARIABLES
    static Resource resource = new Resource
        {
            Id = 0,
            CultureType = CultureType.Wheat,
            SeedPerHectare = 0,
            WorkerPerHectare = 0,
            RequiredMachines = null
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
            Workers = null,
            Machines = null,
            Tasks = null,
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
            Tasks = null,
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
    
    public void SetMenuState(string newState)
    {
        _menu.ChangeState(newState);
    }    
        
    public void Start()
    {
        _menu.OptionSelected += OnOptionSelected;
        _isRunning = true;
        
        _menu.DisplayWelcomeMessage();
        while (_isRunning)
        {
            _menu.DisplayMenuOptions();
            _menu.StartSelectingPhase();
        }
    }
    
    public void Stop()
    {
        _isRunning = false;
    }

    // PRIVATE METHODS
    
    private void OnOptionSelected(string option)
    {
        if (_stateHandlers.TryGetValue(_menu.CurrentState, out var handler))
        {
            handler.HandleOption(option, this);
        }
    }
    
    private void DisplayField()
    {
        resource.RequiredMachines = [machine];
        worker.Tasks = [task];
        field.Workers = [worker];
        field.Machines = [machine];
        field.Tasks = [task];
        
        _viewService.DisplayFieldDetails(field);
    }
}
