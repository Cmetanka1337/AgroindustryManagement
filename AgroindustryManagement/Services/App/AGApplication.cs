using AgroindustryManagement.Views;
using AgroindustryManagement.Services.App.Menu;
using AgroindustryManagement.Services.App.Menu.MenuStateHandlers;
using AgroindustryManagement.Services.Database;

namespace AgroindustryManagement.Services.App;

public class AGApplication
{
    private readonly AGMenu _menu = new ();
    public readonly AGViewService ViewService = new ();
    public readonly AGDatabaseService DatabaseService;
    private bool _isRunning;
    private readonly Dictionary<string, IAGMenuStateHandler> _stateHandlers;

    public AGApplication(AGDatabaseService dbService)
    {
        DatabaseService = dbService;
        _stateHandlers = new Dictionary<string, IAGMenuStateHandler>
        {
            { AGMenuState.MainMenuState, new MainMenuStateHandler(this) },
            { AGMenuState.FieldMenuState, new AGFieldMenuStateHandler(this) },
            { AGMenuState.MachineMenuState, new AGMachineMenuStateHandler(this) },
            { AGMenuState.InventoryItemMenuState, new AGInventoryItemMenuStateHandler(this) },
            { AGMenuState.WorkerMenuState, new AGWorkerMenuStateHandler(this) },
            { AGMenuState.WorkerTaskMenuState, new AGWorkerTaskMenuStateHandler(this) }
        };
    }

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
}
