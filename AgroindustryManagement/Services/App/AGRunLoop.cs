namespace AgroindustryManagement.Services.App;

public class AGRunLoop
{
    private readonly AGMenu _menu = new ();
    private bool _isRunning;
    
    public void Start()
    {
        _menu.OptionSelected += OnOptionSelected;
        _isRunning = true;
        while (_isRunning)
        {
            _menu.DisplayWelcomeMessage();
            _menu.DisplayMainMenuOptions();
            _menu.StartSelectingPhase();
        }
    }

    private void Stop()
    {
        _isRunning = false;
    }
    
    private void OnOptionSelected(int option)
    {
        switch (option)
        {
            case 0:
                Stop();
                break;
        }
    }
}