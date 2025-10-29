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
                break;
            case MenuOptions.WorkerTaskOptions.DisplayAllWorkerTasks:
                break;
            case MenuOptions.WorkerTaskOptions.EditWorkerTask:
                break;
            case MenuOptions.WorkerTaskOptions.DeleteWorkerTask:
                break;
            case MenuOptions.WorkerTaskOptions.AddWorkerTask:
                break;
            case MenuOptions.WorkerTaskOptions.Back:
                app.SetMenuState(AGMenuState.MainMenuState);
                break;
        }
    }
}