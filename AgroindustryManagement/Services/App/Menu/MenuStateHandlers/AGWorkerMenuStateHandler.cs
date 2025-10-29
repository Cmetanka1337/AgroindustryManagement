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
                break;
            case MenuOptions.WorkerOptions.DisplayAllWorkers:
                break;
            case MenuOptions.WorkerOptions.EditWorker:
                break;
            case MenuOptions.WorkerOptions.DeleteWorker:
                break;
            case MenuOptions.WorkerOptions.AddWorker:
                break;
            case MenuOptions.WorkerOptions.Back:
                app.SetMenuState(AGMenuState.MainMenuState);
                break;
        }
    }
}