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
            case MenuOptions.WorkerOptions.Back:
                app.SetMenuState(AGMenuState.MainMenuState);
                break;
        }
    }
    
    private void DisplayWorker()
    {
        var availableWorkersIds = App.DatabaseService.GetAllWorkers().Select(worker => worker.Id.ToString()).ToArray();
        App.ViewService.DisplayIds(availableWorkersIds);
        
        var workerId = -1;
        while (!availableWorkersIds.Contains(workerId.ToString()))
        {
            workerId = App.ViewService.GetIntegerUserInputWithMessage("Enter Worker Id");
        }
        
        var worker = App.DatabaseService.GetWorkerById(workerId);
        App.ViewService.DisplayWorkerDetails(worker);
    }
    
    private void DisplayAllWorkers()
    {
        App.ViewService.DisplayAllWorkers(App.DatabaseService.GetAllWorkers());
    }
    
    private void EditWorker()
    {
        
    }
    
    private void DeleteWorker()
    {
        var workerId = App.ViewService.GetIntegerUserInputWithMessage("Enter Worker Id");
        App.DatabaseService.DeleteWorker(workerId);
    }
    
    private void AddWorker()
    {
        var worker = App.DataCollector.CollectData<Worker>();
        App.DatabaseService.AddWorker(worker);
    }
}