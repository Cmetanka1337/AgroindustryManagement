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
            case MenuOptions.WorkerTaskOptions.RequiredWorkers:
                RequiredWorkers();
                break;
            case MenuOptions.WorkerTaskOptions.Back:
                app.SetMenuState(AGMenuState.MainMenuState);
                break;
        }
    }
    
    private void DisplayWorkerTask()
    {
        var workerTask = App.DatabaseService.GetWorkerTaskById(GetWorkerTaskId());
        App.ViewService.DisplayWorkerTaskDetails(workerTask);
    }
    
    private void DisplayAllWorkerTasks()
    {
        App.ViewService.DisplayAllWorkerTasks(App.DatabaseService.GetAllWorkerTasks());
    }
    
    private void EditWorkerTask()
    {
        DisplayAllWorkerTasks();
        var id = GetWorkerTaskId();
        var existingWorkerTask = App.DatabaseService.GetWorkerTaskById(id);
        var updatedWorkerTask = App.DataCollector.EditData(existingWorkerTask);
        App.DatabaseService.UpdateWorkerTask(updatedWorkerTask);
    }
    
    private void DeleteWorkerTask()
    {
        var workerTaskId = App.ViewService.GetIntegerUserInputWithMessage("Enter Worker Task Id");
        App.DatabaseService.DeleteWorkerTask(workerTaskId);
    }
    
    private void AddWorkerTask()
    {
        var workerTask = App.DataCollector.CollectData<WorkerTask>();
        workerTask.StartDate = DateTime.Now;
        workerTask.EstimatesEndDate = DateTime.MaxValue;
        workerTask.RealEndDate = DateTime.MaxValue;
        
        App.DatabaseService.AddWorkerTask(workerTask);
    }
    
    private int GetWorkerTaskId()
    {
        var availableWorkerTasksIds = App.DatabaseService.GetAllWorkerTasks().Select(task => task.Id.ToString()).ToArray();
        App.ViewService.DisplayIds(availableWorkerTasksIds);
        
        var workerTaskId = -1;
        while (!availableWorkerTasksIds.Contains(workerTaskId.ToString()))
        {
            workerTaskId = App.ViewService.GetIntegerUserInputWithMessage("Enter Worker Task Id");
        }

        return workerTaskId;
    }
    
    private void RequiredWorkers()
    {
        DisplayAllWorkerTasks();
        var id = GetWorkerTaskId();
        var workerTask = App.DatabaseService.GetWorkerTaskById(id);
        var requiredWorkers = App.CalculationService.CalculateRequiredWorkers(workerTask.Field.Culture, workerTask.Field.Area);
        Console.WriteLine("Recommended number of workers: " + requiredWorkers);
    } 
}