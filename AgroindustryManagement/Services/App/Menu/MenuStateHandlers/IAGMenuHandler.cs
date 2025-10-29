namespace AgroindustryManagement.Services.App.Menu.MenuStateHandlers;

public interface IAGMenuStateHandler
{
    AGApplication App { get; }
    void HandleOption(string option, AGApplication app);
}