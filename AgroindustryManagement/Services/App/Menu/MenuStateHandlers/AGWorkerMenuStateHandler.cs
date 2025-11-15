using AgroindustryManagement.Models;

namespace AgroindustryManagement.Services.App.Menu.MenuStateHandlers;

public class AGWorkerMenuStateHandler: IAGMenuStateHandler
{
    public AGApplication App { get; }
    
    
    public AGWorkerMenuStateHandler(AGApplication app)
    {
        App = app;
    }

    public void HandleOption(string option, AGApplication app)
    {
        switch (option)
        {
            case MenuOptions.WorkerOptions.DisplayWorker:
                DisplayWorker();
                break;
            case MenuOptions.WorkerOptions.DisplayAllWorkers:
                DisplayAllWorkers();
                break;
            case MenuOptions.WorkerOptions.EditWorker:
                EditWorker();
                break;
            case MenuOptions.WorkerOptions.DeleteWorker:
                DeleteWorker();
                break;
            case MenuOptions.WorkerOptions.AddWorker:
                AddWorker();
                break;
            case MenuOptions.WorkerOptions.CalculateBonus:
                CalculateBonus();
                break;
            case MenuOptions.WorkerOptions.Back:
                app.SetMenuState(AGMenuState.MainMenuState);
                break;
        }
    }
    
    private void DisplayWorker()
    {
        var worker = App.DatabaseService.GetWorkerById(GetWorkerId());
        App.ViewService.DisplayWorkerDetails(worker);
    }
    
    private void DisplayAllWorkers()
    {
        App.ViewService.DisplayAllWorkers(App.DatabaseService.GetAllWorkers());
    }
    
    private void EditWorker()
    {
        DisplayAllWorkers();
        var id = GetWorkerId();
        var existingWorker = App.DatabaseService.GetWorkerById(id);
        var updatedWorker = App.DataCollector.EditData(existingWorker);
        App.DatabaseService.UpdateWorker(updatedWorker);
    }
    
    private void DeleteWorker()
    {
        DisplayAllWorkers();
        var workerId = App.ViewService.GetIntegerUserInputWithMessage("Enter Worker Id");
        App.DatabaseService.DeleteWorker(workerId);
    }
    
    private void AddWorker()
    {
        var worker = App.DataCollector.CollectData<Worker>();
        App.DatabaseService.AddWorker(worker);
    }
    
    private int GetWorkerId()
    {
        var availableWorkersIds = App.DatabaseService.GetAllWorkers().Select(worker => worker.Id.ToString()).ToArray();
        App.ViewService.DisplayIds(availableWorkersIds);
        
        var workerId = -1;
        while (!availableWorkersIds.Contains(workerId.ToString()))
        {
            workerId = App.ViewService.GetIntegerUserInputWithMessage("Enter Worker Id");
        }

        return workerId;
    }

    private void CalculateBonus()
    {
        DisplayAllWorkers();
        var id = GetWorkerId();
        var bonus = App.CalculationService.CalculateBonus(id);
        Console.WriteLine($"The calculated bonus for worker Id {id} is: {bonus}");
    }
}