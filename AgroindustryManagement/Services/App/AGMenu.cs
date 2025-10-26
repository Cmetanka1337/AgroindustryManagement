namespace AgroindustryManagement.Services.App;

public class AGMenu
{
    public event Action<int> OptionSelected;
    
    public void DisplayWelcomeMessage()
    {
        Console.WriteLine("Welcome to the Agroindustry Management System!");
        Console.WriteLine("Done by Vsevolod Burtyk, Volodymyr Sribnyi, Olha Dubina");
    }
    
    public void DisplayMainMenuOptions()
    {
        Console.WriteLine("=== Agroindustry Management System ===");
        Console.WriteLine("1. Show Field");
        Console.WriteLine("0. Exit");
        Console.Write("Select an option: ");
    }

    public void StartSelectingPhase()
    {
        var selectedOption = int.Parse(Console.ReadLine() ?? string.Empty);
        OptionSelected.Invoke(selectedOption);
    }
}