namespace AgroindustryManagement.Services.App.Menu.MenuStateHandlers;

public class AGFieldMenuStateHandler: IAGMenuStateHandler
{
    public void HandleOption(string option, AGApplication app)
    {
        switch (option)
        {
            case MenuOptions.FieldOptions.DisplayField:
                break;
            case MenuOptions.FieldOptions.DisplayAllFields:
                break;
            case MenuOptions.FieldOptions.EditField:
                break;
            case MenuOptions.FieldOptions.DeleteField:
                break;
            case MenuOptions.FieldOptions.AddField:
                break;
            case MenuOptions.FieldOptions.Back:
                app.SetMenuState(AGMenuState.MainMenuState);
                break;
        }
    }
    
    
}