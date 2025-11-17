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
            case MenuOptions.FieldOptions.FertilizeAmount:
                FertilizerAmount();
                break;
            case MenuOptions.FieldOptions.SeedAmount:
                SeedAmount();
                break;
            case MenuOptions.FieldOptions.EstimateYield:
                EstimatedYield();
                break;
            case MenuOptions.FieldOptions.FuelConsumption:
                FuelConsumption();
                break;
            case MenuOptions.FieldOptions.MachineryCount:
                RequiredMachineryCount();
                break;
            case MenuOptions.FieldOptions.WorkDuration:
                EstimateWorkDuration();
                break;
            case MenuOptions.FieldOptions.Back:
                app.SetMenuState(AGMenuState.MainMenuState);
                break;
        }
    }
    
    private void DisplayField()
    {
            var field = App.DatabaseService.GetFieldById(GetFieldId());
            if (field != null)
            {
                App.ViewService.DisplayFieldDetails(field);
                return;
            }
            Console.WriteLine("Field not found.");
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
        DisplayAllFields();
        var id = GetFieldId();
        var existingField = App.DatabaseService.GetFieldById(id);
        if (existingField == null)
        {
            Console.WriteLine("Field not found.");
            return;
        }
        var updatedField = App.DataCollector.EditData(existingField);
        App.DatabaseService.UpdateField(updatedField);
    }
    
    private void DeleteField()
    {
        DisplayAllFields();
        var fieldId = App.ViewService.GetIntegerUserInputWithMessage("Enter field Id");
        App.DatabaseService.DeleteField(fieldId);
    }

    private int GetFieldId()
    {
        var availableFieldsIds = App.DatabaseService.GetAllFields().Select(field => field.Id.ToString()).ToArray();
        App.ViewService.DisplayIds(availableFieldsIds);
        int fieldId = -1;
        
        while (!availableFieldsIds.Contains(fieldId.ToString()))
        {
            fieldId = App.ViewService.GetIntegerUserInputWithMessage("Enter field Id");
        }

        return fieldId;
    }

    private void FertilizerAmount()
    {
        DisplayAllFields();
        var id = GetFieldId();
        var field = App.DatabaseService.GetFieldById(id);
        if (field == null)
        {
            Console.WriteLine("Field not found.");
            return;
        }
        var fertilizerAmount = App.CalculationService.CalculateFertilizerAmount(field.Culture, field.Area);
        var message = fertilizerAmount >= 0 ? $"Recommended fertilizer amount: {fertilizerAmount} kg" : "Error occured during calculation. Check your data";
        Console.WriteLine(message);
    }

    private void SeedAmount()
    {
        DisplayAllFields();
        var id = GetFieldId();
        var field = App.DatabaseService.GetFieldById(id);
        if (field == null)
        {
            Console.WriteLine("Field not found");
            return;
        }
        var seedAmount = App.CalculationService.CalculateSeedAmount(field.Culture, field.Area);
        var message = seedAmount >= 0 ? $"Recommended seed amount: {seedAmount} kg" : "Error occured during calculation. Check your data";
        Console.WriteLine(message);
    }
    
    private void EstimatedYield()
    {
        DisplayAllFields();
        var id = GetFieldId();
        var field = App.DatabaseService.GetFieldById(id);
        if (field == null)
        {
            Console.WriteLine("Field not found");
            return;
        }
        var estimatedYield = App.CalculationService.EstimateYield(field.Culture, field.Area);
        var message = estimatedYield >= 0 ? $"Estimated yield: {estimatedYield} tons" : "Error occured during calculation. Check your data";
        Console.WriteLine(message);
    }

    private void FuelConsumption()
    {
        DisplayAllFields();
        var id = GetFieldId();
        var field = App.DatabaseService.GetFieldById(id);
        if (field == null)
        {
            Console.WriteLine("Field not found");
            return;
        }
        field.Machines.ForEach(machine =>
        {
            var fuelConsumption = App.CalculationService.EstimateFuelConsumption(machine.Type, field.Area);
            var message = fuelConsumption >= 0 ? $"Estimated fuel consumption for {machine.Type.ToString()}: {fuelConsumption} liters" : "Error occured during calculation. Check your data";
            Console.WriteLine(message);
        });
    }

    private void RequiredMachineryCount()
    {
        DisplayAllFields();
        var id = GetFieldId();
        var field = App.DatabaseService.GetFieldById(id);
        if (field == null)
        {
            Console.WriteLine("Field not found");
            return;
        }
        var machineryCount = App.CalculationService.CalculateRequiredMachineryCount(field.Culture, field.Area);
        var message = machineryCount >= 0 ? $"Required machinery count: {machineryCount}" : "Error occured during calculation. Check your data";
        Console.WriteLine(message);
    }
    
    private void EstimateWorkDuration()
    {
        DisplayAllFields();
        var id = GetFieldId();
        var field = App.DatabaseService.GetFieldById(id);
        if (field == null)
        {
            Console.WriteLine("Field not found.");
            return;
        }
        
        var workersCount = App.CalculationService.EstimateWorkDuration(field.Area, field.Machines.Count, field.Machines[0].Type, field.Culture);
        var message = workersCount >= 0 ? $"Estimated work duration (in hours): {workersCount}" : "Error occured during calculation. Check your data";
        Console.WriteLine(message);
    }
}