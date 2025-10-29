using AgroindustryManagement.Models;

namespace AgroindustryManagement.Services.App.Menu.MenuStateHandlers;

public class AGFieldMenuStateHandler: IAGMenuStateHandler
{
    public AGApplication App { get; }
    
    
    public AGFieldMenuStateHandler(AGApplication app)
    {
        App = app;
    }
    // TEMP VARIABLES
        static Resource resource = new Resource
            {
                Id = 0,
                CultureType = CultureType.Wheat,
                SeedPerHectare = 0,
                WorkerPerHectare = 0,
                RequiredMachines = null
            };
        static Machine machine = new Machine
            {
                Id = 0,
                Type = MachineType.Tractor,
                IsAvailable = false,
                AssignedToField = null,
                Field = null,
                ResourceId = 0,
                Resource = resource
            };
            static  Field field = new Field
            {
                Id = 0,
                Area = 0,
                Culture = CultureType.Wheat,
                Status = FieldStatus.Planted,
                Workers = null,
                Machines = null,
                Tasks = null,
                CreatedAt = default
            };
    
            static Worker worker = new Worker
            {
                Id = 0,
                FirstName = "name",
                LastName = "surname",
                Age = 0,
                HourlyRate = 0,
                IsActive = true,
                Tasks = null,
                HoursWorked = 0
            };
            static WorkerTask task = new WorkerTask
            {
                Id = 0,
                Description = "description",
                WorkerId = 0,
                Worker = worker,
                FieldId = 0,
                Field = field,
                TaskType = TaskType.Planting,
                Progress = 0,
                StartDate = default,
                EstimatesEndDate = default
            };

    public void HandleOption(string option, AGApplication app)
    {
        switch (option)
        {
            case MenuOptions.FieldOptions.DisplayField:
                DisplayField();
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
    
    private void DisplayField()
    {
        resource.RequiredMachines = [machine];
        worker.Tasks = [task];
        field.Workers = [worker];
        field.Machines = [machine];
        field.Tasks = [task];
        
        App.ViewService.DisplayFieldDetails(field);
    }
}