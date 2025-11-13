using AgroindustryManagement.Models;

namespace AgroindustryManagement.Services.App.Menu.MenuStateHandlers;

public class AGWorkerTaskMenuStateHandler: IAGMenuStateHandler
{
    public AGApplication App { get; }

    
    public AGWorkerTaskMenuStateHandler(AGApplication app)
    {
        App = app;
    }
    public void HandleOption(string option, AGApplication app)
    {
        switch (option)
        {
            case MenuOptions.WorkerTaskOptions.DisplayWorkerTask:
                DisplayWorkerTask();
                break;
            case MenuOptions.WorkerTaskOptions.DisplayAllWorkerTasks:
                DisplayAllWorkerTasks();
                break;
            case MenuOptions.WorkerTaskOptions.EditWorkerTask:
                EditWorkerTask();
                break;
            case MenuOptions.WorkerTaskOptions.DeleteWorkerTask:
                DeleteWorkerTask();
                break;
            case MenuOptions.WorkerTaskOptions.AddWorkerTask:
                AddWorkerTask();
                break;
            case MenuOptions.WorkerTaskOptions.Back:
                app.SetMenuState(AGMenuState.MainMenuState);
                break;
        }
    }
    
    private void DisplayWorkerTask()
    {
        var workerTaskId = App.ViewService.GetIntegerUserInputWithMessage("Enter Worker Task Id");
        var workerTask = App.DatabaseService.GetWorkerTaskById(workerTaskId);
        App.ViewService.DisplayWorkerTaskDetails(workerTask);
    }
    
    private void DisplayAllWorkerTasks()
    {
        App.ViewService.DisplayAllWorkerTasks(App.DatabaseService.GetAllWorkerTasks());
    }
    
    private void EditWorkerTask()
    {
        
    }
    
    private void DeleteWorkerTask()
    {
        var workerTaskId = App.ViewService.GetIntegerUserInputWithMessage("Enter Worker Task Id");
        App.DatabaseService.DeleteWorkerTask(workerTaskId);
    }
    
    private void AddWorkerTask()
    {
        var workerTask = App.DataCollector.CollectData<WorkerTask>();
        App.DatabaseService.AddWorkerTask(workerTask);
    }
}