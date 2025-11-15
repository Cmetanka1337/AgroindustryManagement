using AgroindustryManagement.Models;

namespace AgroindustryManagement.Services.App.Menu.MenuStateHandlers;

public class AGResourceStateMenuHandler: IAGMenuStateHandler
{
    public AGApplication App { get; }
    
    public AGResourceStateMenuHandler(AGApplication app)
    {
        App = app;
    }
    public void HandleOption(string option, AGApplication app)
    {
        switch (option)
        {
            case MenuOptions.ResourceOptions.DisplayResource:
                DisplayResource();
                break;
            case MenuOptions.ResourceOptions.DisplayAllResources:
                DisplayAllResources();
                break;
            case MenuOptions.ResourceOptions.EditResource:
                EditResource();
                break;
            case MenuOptions.ResourceOptions.DeleteResource:
                DeleteResource();
                break;
            case MenuOptions.ResourceOptions.AddResource:
                AddResource();
                break;
            case MenuOptions.ResourceOptions.Back:
                app.SetMenuState(AGMenuState.MainMenuState);
                break;
        }
    }
    
    private void DisplayResource()
    {
        var resource = App.DatabaseService.GetResourceById(GetResourceId());
        App.ViewService.DisplayResourceDetails(resource);
    }
    
    private void DisplayAllResources()
    {
        App.ViewService.DisplayAllResources(App.DatabaseService.GetAllResources());
    }
    
    private void AddResource()
    {
        var resource = App.DataCollector.CollectData<Resource>();
        App.DatabaseService.AddResource(resource);
    }
    
    private void EditResource()
    {
        DisplayAllResources();
        var id = GetResourceId();
        var resource = App.DatabaseService.GetResourceById(id);
        App.ViewService.DisplayResourceDetails(resource);
        var updatedResource = App.DataCollector.EditData(resource);
        App.DatabaseService.EditResource(updatedResource);
    }
    
    private void DeleteResource()
    {
        DisplayAllResources();
        var id = GetResourceId();
        App.DatabaseService.DeleteResource(id);
    }
    
    private int GetResourceId()
    {
        var availableResourceIds = App.DatabaseService.GetAllResources().Select(resource => resource.Id.ToString()).ToArray();
        App.ViewService.DisplayIds(availableResourceIds);
        
        var resourceId = -1;
        while (!availableResourceIds.Contains(resourceId.ToString()))
        {
            resourceId = App.ViewService.GetIntegerUserInputWithMessage("Enter Warehouse Id");
        }

        return resourceId;
    }
}