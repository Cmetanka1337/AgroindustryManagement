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
                break;
            case MenuOptions.MachineOptions.DisplayAllMachines:
                break;
            case MenuOptions.MachineOptions.EditMachine:
                break;
            case MenuOptions.MachineOptions.DeleteMachine:
                break;
            case MenuOptions.MachineOptions.AddMachine:
                break;
            case MenuOptions.MachineOptions.Back:
                app.SetMenuState(AGMenuState.MainMenuState);
                break;
        }
    }
}