namespace AgroindustryManagement.Services.App.Menu;

public class AGMenu
{
    public event Action<string> OptionSelected;
    private string _state = AGMenuState.MainMenuState;
    private int _maxRecursionDepth = 30;
    
    public void DisplayWelcomeMessage()
    {
        Console.WriteLine("Welcome to the Agroindustry Management System!");
        Console.WriteLine("Done by Vsevolod Burtyk, Volodymyr Sribnyi, Olha Dubina");
    }
    
    // TODO: Refactor to use state pattern
    public void DisplayMainMenuOptions()
    {
        Console.WriteLine("=== Agroindustry Management System ===");
        Console.WriteLine("1. Show Field");
        Console.WriteLine("0. Exit");
        Console.Write("Select an option: ");
    }

    public void StartSelectingPhase()
    {
        while (_maxRecursionDepth != 0)
        {
            var selectedNumericOption = int.Parse(Console.ReadLine() ?? string.Empty);
            var parsedOption = AGMenuOptionParser.GetOptionKey(GetCurrentStateOptions(), selectedNumericOption);
            if (parsedOption != null)
            {
                OptionSelected.Invoke(parsedOption);
            }
            else
            {
                _maxRecursionDepth--;
                ShowGenericErrorMessage();
                continue;
            }

            break;
        }
        _maxRecursionDepth = 30;
    }

    private void ShowGenericErrorMessage()
    {
        Console.WriteLine("Error occured! \n Please, try again.");
    }
    
    public void ChangeState(string newState)
    {
        _state = newState;
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
