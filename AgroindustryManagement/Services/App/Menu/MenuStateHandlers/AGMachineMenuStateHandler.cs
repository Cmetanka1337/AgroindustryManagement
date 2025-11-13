using AgroindustryManagement.Models;

namespace AgroindustryManagement.Services.App.Menu.MenuStateHandlers;

public class AGMachineMenuStateHandler: IAGMenuStateHandler
{
    public AGApplication App { get; }

    
    public AGMachineMenuStateHandler(AGApplication app)
    {
        App = app;
    }
    public void HandleOption(string option, AGApplication app)
    {
        switch (option)
        {
            case MenuOptions.MachineOptions.DisplayMachine:
                DisplayMachine();
                break;
            case MenuOptions.MachineOptions.DisplayAllMachines:
                DisplayAllMachines();
                break;
            case MenuOptions.MachineOptions.EditMachine:
                EditMachine();
                break;
            case MenuOptions.MachineOptions.DeleteMachine:
                DeleteMachine();
                break;
            case MenuOptions.MachineOptions.AddMachine:
                AddMachine();
                break;
            case MenuOptions.MachineOptions.Back:
                app.SetMenuState(AGMenuState.MainMenuState);
                break;
        }
    }
    
    private void DisplayMachine()
    {
        var machineId = App.ViewService.GetIntegerUserInputWithMessage("Enter Machine Id");
        var machine = App.DatabaseService.GetMachineById(machineId);
        App.ViewService.DisplayMachineDetails(machine);
    }
    
    private void DisplayAllMachines()
    {
        App.ViewService.DisplayAllMachines(App.DatabaseService.GetAllMachines());
    }
    
    private void EditMachine()
    {
        
    }
    
    private void DeleteMachine()
    {
        var machineId = App.ViewService.GetIntegerUserInputWithMessage("Enter Machine Id");
        App.DatabaseService.DeleteMachine(machineId);
    }
    
    private void AddMachine()
    {
        var machine = App.DataCollector.CollectData<Machine>();
        App.DatabaseService.AddMachine(machine);
    }
}