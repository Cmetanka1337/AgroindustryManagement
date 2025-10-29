namespace AgroindustryManagement.Services.App.Menu.MenuStateHandlers;

public class AGInventoryItemMenuStateHandler: IAGMenuStateHandler
{
    public void HandleOption(string option, AGApplication app)
    {
        switch (option)
        {
            case MenuOptions.InventoryItemOptions.DisplayInventoryItem:
                break;
            case MenuOptions.InventoryItemOptions.DisplayAllInventoryItems:
                break;
            case MenuOptions.InventoryItemOptions.EditInventoryItem:
                break;
            case MenuOptions.InventoryItemOptions.DeleteInventoryItem:
                break;
            case MenuOptions.InventoryItemOptions.AddInventoryItem:
                break;
            case MenuOptions.InventoryItemOptions.Back:
                app.SetMenuState(AGMenuState.MainMenuState);
                break;
        }
    }
}