using AgroindustryManagement.Models;

namespace AgroindustryManagement.Services.App.Menu.MenuStateHandlers;

public class AGFieldMenuStateHandler: IAGMenuStateHandler
{
    public AGApplication App { get; }
    
    
    public AGFieldMenuStateHandler(AGApplication app)
    {
        App = app;
    }
    
    public void HandleOption(string option, AGApplication app)
    {
        switch (option)
        {
            case MenuOptions.FieldOptions.DisplayField:
                DisplayField();
                break;
            case MenuOptions.FieldOptions.DisplayAllFields:
                DisplayAllFields();
                break;
            case MenuOptions.FieldOptions.EditField:
                EditField();
                break;
            case MenuOptions.FieldOptions.DeleteField:
                DeleteField();
                break;
            case MenuOptions.FieldOptions.AddField:
                AddField();
                break;
            case MenuOptions.FieldOptions.Back:
                app.SetMenuState(AGMenuState.MainMenuState);
                break;
        }
    }
    
    private void DisplayField()
    {
        var fieldId = App.ViewService.GetIntegerUserInputWithMessage("Enter field Id");
        var field = App.DatabaseService.GetFieldById(fieldId);
        App.ViewService.DisplayFieldDetails(field);
    }

    private void DisplayAllFields()
    {
        App.ViewService.DisplayAllFields(App.DatabaseService.GetAllFields());
    }

    private void AddField()
    {
        var field = App.DataCollector.CollectData<Field>();
        field.CreatedAt = DateTime.Now;
        App.DatabaseService.AddField(field);
    }
    
    private void EditField()
    {
        
    }
    
    private void DeleteField()
    {
        var fieldId = App.ViewService.GetIntegerUserInputWithMessage("Enter field Id");
        App.DatabaseService.DeleteField(fieldId);
    }
}