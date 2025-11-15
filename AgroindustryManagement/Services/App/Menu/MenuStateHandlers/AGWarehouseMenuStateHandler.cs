using AgroindustryManagement.Models;

namespace AgroindustryManagement.Services.App.Menu.MenuStateHandlers;

public class AGWarehouseMenuStateHandler: IAGMenuStateHandler
{
    public AGApplication App { get; }
    
    public AGWarehouseMenuStateHandler(AGApplication app)
    {
        App = app;
    }
    public void HandleOption(string option, AGApplication app)
    {
        switch (option)
        {
            case MenuOptions.WarehouseOptions.DisplayWarehouse:
                DisplayWarehouse();
                break;
            case MenuOptions.WarehouseOptions.DisplayAllWarehouses:
                DisplayAllWarehouses();
                break;
            case MenuOptions.WarehouseOptions.EditWarehouse:
                EditWarehouse();
                break;
            case MenuOptions.WarehouseOptions.DeleteWarehouse:
                DeleteWarehouse();
                break;
            case MenuOptions.WarehouseOptions.AddWarehouse:
                AddWarehouse();
                break;
            case MenuOptions.WarehouseOptions.Back:
                app.SetMenuState(AGMenuState.MainMenuState);
                break;
        }
    }
    
    private void DisplayWarehouse()
    {
        var warehouse = App.DatabaseService.GetWarehouseById(GetWarehouseId());
        App.ViewService.DisplayWarehouseDetails(warehouse);
    }
    
    private void DisplayAllWarehouses()
    {
        App.ViewService.DisplayAllWarehouses(App.DatabaseService.GetAllWarehouses());
    }
    
    private void AddWarehouse()
    {
        var warehouse = App.DataCollector.CollectData<Warehouse>();
        App.DatabaseService.AddWarehouse(warehouse);
    }
    
    private void EditWarehouse()
    {
        DisplayAllWarehouses();
        var id = GetWarehouseId();
        var warehouse = App.DatabaseService.GetWarehouseById(id);
        App.ViewService.DisplayWarehouseDetails(warehouse);
        var updatedWarehouse = App.DataCollector.EditData(warehouse);
        App.DatabaseService.UpdateWarehouse(updatedWarehouse);
    }
    
    private void DeleteWarehouse()
    {
        DisplayAllWarehouses();
        var id = GetWarehouseId();
        App.DatabaseService.DeleteWarehouse(id);
    }
    
    private int GetWarehouseId()
    {
        var availableWarehousesIds = App.DatabaseService.GetAllWarehouses().Select(warehouse => warehouse.Id.ToString()).ToArray();
        App.ViewService.DisplayIds(availableWarehousesIds);
        
        var warehouseId = -1;
        while (!availableWarehousesIds.Contains(warehouseId.ToString()))
        {
            warehouseId = App.ViewService.GetIntegerUserInputWithMessage("Enter Warehouse Id");
        }

        return warehouseId;
    }
}