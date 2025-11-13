using AgroindustryManagement.Models;

namespace AgroindustryManagement.Services.App.Menu.MenuStateHandlers;

public class AGInventoryItemMenuStateHandler: IAGMenuStateHandler
{
    public AGApplication App { get; }

    
    public AGInventoryItemMenuStateHandler(AGApplication app)
    {
        App = app;
    }
    public void HandleOption(string option, AGApplication app)
    {
        switch (option)
        {
            case MenuOptions.InventoryItemOptions.DisplayInventoryItem:
                DisplayInventoryItem();
                break;
            case MenuOptions.InventoryItemOptions.DisplayAllInventoryItems:
                DisplayAllInventoryItems();
                break;
            case MenuOptions.InventoryItemOptions.EditInventoryItem:
                EditInventoryItem();
                break;
            case MenuOptions.InventoryItemOptions.DeleteInventoryItem:
                DeleteInventoryItem();
                break;
            case MenuOptions.InventoryItemOptions.AddInventoryItem:
                AddInventoryItem();
                
                break;
            case MenuOptions.InventoryItemOptions.Back:
                app.SetMenuState(AGMenuState.MainMenuState);
                break;
        }
    }
    
    private void DisplayInventoryItem()
    {
        var itemId = App.ViewService.GetIntegerUserInputWithMessage("Enter Inventory Item Id");
        var item = App.DatabaseService.GetInventoryItemById(itemId);
        App.ViewService.DisplayInventoryItemDetails(item);
    }
    
    private void DisplayAllInventoryItems()
    {
        App.ViewService.DisplayAllInventoryItems(App.DatabaseService.GetAllInventoryItems());
    }
    
    private void EditInventoryItem()
    {
        
    }
    
    private void DeleteInventoryItem()
    {
        var itemId = App.ViewService.GetIntegerUserInputWithMessage("Enter Inventory Item Id");
        App.DatabaseService.DeleteInventoryItem(itemId);
    }
    
    private void AddInventoryItem()
    {
        var item = App.DataCollector.CollectData<InventoryItem>();
        App.DatabaseService.AddInventoryItem(item);
    }
}