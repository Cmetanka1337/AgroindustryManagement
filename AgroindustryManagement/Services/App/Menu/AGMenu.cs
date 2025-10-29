namespace AgroindustryManagement.Services.App.Menu;

public class AGMenu
{
    public event Action<string>? OptionSelected;
    private string _state = AGMenuState.MainMenuState;
    private int _maxRecursionDepth = 30;
    private readonly Dictionary<string, Dictionary<int, string>> _menuOptionsByState = new()
    {
        { AGMenuState.MainMenuState, MenuOptions.MainOptions.NumericMenuOptions },
        { AGMenuState.FieldMenuState, MenuOptions.FieldOptions.NumericMenuOptions },
        { AGMenuState.WorkerMenuState, MenuOptions.WorkerOptions.NumericMenuOptions },
        { AGMenuState.MachineMenuState, MenuOptions.MachineOptions.NumericMenuOptions },
        { AGMenuState.InventoryItemMenuState, MenuOptions.InventoryItemOptions.NumericMenuOptions },
        { AGMenuState.WorkerTaskMenuState, MenuOptions.WorkerTaskOptions.NumericMenuOptions }
    };
    
    public string CurrentState => _state;
    
    public void DisplayWelcomeMessage()
    {
        Console.WriteLine("\nWelcome to the Agroindustry Management System!");
        Console.WriteLine("Done by Vsevolod Burtyk, Volodymyr Sribnyi, Olha Dubina");
    }
    
    public void DisplayMenuOptions()
    {
        Console.WriteLine($"\n=== {_state} Menu ===");

        if (_menuOptionsByState.TryGetValue(_state, out var options))
        {
            foreach (var option in options)
            {
                Console.WriteLine($"{option.Key}. {AGMenuOptionHelper.FormatOptionName(option.Value)}");
            }
        }
        else
        {
            Console.WriteLine("No options available.");
        }

        Console.Write("Select an option: ");
    }

    // TODO: There is a bug with error handling right now
    // Sometimes error message is not shown after incorrect input. However, when user is trying to exit only errors is shown.
    public void StartSelectingPhase()
    {
        while (_maxRecursionDepth > 0)
        {
            var input = Console.ReadLine();
    
            if (!int.TryParse(input, out var selectedNumericOption))
            {
                ShowGenericErrorMessage();
                _maxRecursionDepth--;
                continue;
            }
    
            var parsedOption = AGMenuOptionHelper.GetOptionKey(GetCurrentStateOptions(), selectedNumericOption);
            if (parsedOption == null)
            {
                ShowGenericErrorMessage();
                _maxRecursionDepth--;
                DisplayMenuOptions();
                continue;
            }
    
            OptionSelected?.Invoke(parsedOption);
            break;
        }
    
        _maxRecursionDepth = 30;
    }
    
    public void ChangeState(string newState)
    {
        _state = newState;
    }
    
    // PRIVATE METHODS
    
    private void ShowGenericErrorMessage()
    {
        Console.WriteLine("\n\nError occurred! \nPlease, try again.\n\n");
    }
    
    private Dictionary<int, string> GetCurrentStateOptions()
    {
        return _state switch
        {
            AGMenuState.FieldMenuState => MenuOptions.FieldOptions.NumericMenuOptions,
            AGMenuState.WorkerMenuState => MenuOptions.WorkerOptions.NumericMenuOptions,
            AGMenuState.MachineMenuState => MenuOptions.MachineOptions.NumericMenuOptions,
            AGMenuState.InventoryItemMenuState => MenuOptions.InventoryItemOptions.NumericMenuOptions,
            AGMenuState.WorkerTaskMenuState => MenuOptions.WorkerTaskOptions.NumericMenuOptions,
            AGMenuState.MainMenuState => MenuOptions.MainOptions.NumericMenuOptions,
            _ => MenuOptions.MainOptions.NumericMenuOptions
        };
    }
}
