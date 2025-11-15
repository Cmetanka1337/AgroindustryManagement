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
        // crashed here
        var machine = App.DatabaseService.GetMachineById(GetMachineId());
        App.ViewService.DisplayMachineDetails(machine);
    }
    
    private void DisplayAllMachines()
    {
        App.ViewService.DisplayAllMachines(App.DatabaseService.GetAllMachines());
    }
    
    private void EditMachine()
    {
        DisplayAllMachines();
        var id = GetMachineId();
        var existingMachine = App.DatabaseService.GetMachineById(id);
        var updatedMachine = App.DataCollector.EditData(existingMachine);
        App.DatabaseService.UpdateMachine(updatedMachine);
    }
    
    private void DeleteMachine()
    {
        DisplayAllMachines();
        var machineId = App.ViewService.GetIntegerUserInputWithMessage("Enter Machine Id");
        App.DatabaseService.DeleteMachine(machineId);
    }
    
    private void AddMachine()
    {
        var machine = App.DataCollector.CollectData<Machine>();
        App.DatabaseService.AddMachine(machine);
    }
    
    private int GetMachineId()
    {
        var availableMachinesIds = App.DatabaseService.GetAllMachines().Select(machine => machine.Id.ToString()).ToArray();
        App.ViewService.DisplayIds(availableMachinesIds);
        
        var machineId = -1;
        while (!availableMachinesIds.Contains(machineId.ToString()))
        {
            machineId = App.ViewService.GetIntegerUserInputWithMessage("Enter Machine Id");
        }

        return machineId;
    }
}