namespace AgroindustryManagement.Services.App.Menu.MenuStateHandlers;

public class MainMenuStateHandler : IAGMenuStateHandler
{
    public void HandleOption(string option, AGApplication app)
    {
        switch (option)
        {
            case MenuOptions.MainOptions.FieldActions:
                app.SetMenuState(AGMenuState.FieldMenuState);
                break;
            case MenuOptions.MainOptions.MachineActions:
                app.SetMenuState(AGMenuState.MachineMenuState);
                break;
            case MenuOptions.MainOptions.InventoryItemActions:
                app.SetMenuState(AGMenuState.InventoryItemMenuState);
                break;
            case MenuOptions.MainOptions.WorkerActions:
                app.SetMenuState(AGMenuState.WorkerMenuState);
                break;
            case MenuOptions.MainOptions.WorkerTaskActions:
                app.SetMenuState(AGMenuState.WorkerTaskMenuState);
                break;
            case MenuOptions.MainOptions.Exit:
                app.Stop();
                break;
        }
    }
}