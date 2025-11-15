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
        var item = App.DatabaseService.GetInventoryItemById(GetInventoryItemId());
        App.ViewService.DisplayInventoryItemDetails(item);
    }
    
    private void DisplayAllInventoryItems()
    {
        App.ViewService.DisplayAllInventoryItems(App.DatabaseService.GetAllInventoryItems());
    }
    
    private void EditInventoryItem()
    {
        DisplayAllInventoryItems();
        var id = GetInventoryItemId();
        var existingItem = App.DatabaseService.GetInventoryItemById(id);
        var updatedItem = App.DataCollector.EditData(existingItem);
        App.DatabaseService.UpdateInventoryItem(updatedItem);
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
    
    private int GetInventoryItemId()
    {
        var availableItemsIds = App.DatabaseService.GetAllInventoryItems().Select(item => item.Id.ToString()).ToArray();
        App.ViewService.DisplayIds(availableItemsIds);
        
        var itemId = -1;
        while (!availableItemsIds.Contains(itemId.ToString()))
        {
            itemId = App.ViewService.GetIntegerUserInputWithMessage("Enter Inventory Item Id");
        }

        return itemId;
    }
}