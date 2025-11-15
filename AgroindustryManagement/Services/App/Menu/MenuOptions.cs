namespace AgroindustryManagement.Services.App.Menu;

public static class MenuOptions
{
    public static class MainOptions
    {
        public const string FieldActions = "FieldActions";
        public const string WorkerActions = "WorkerActions";
        public const string MachineActions = "MachineActions";
        public const string InventoryItemActions = "InventoryItemActions";
        public const string WorkerTaskActions = "WorkerTaskActions";
        public const string WarehouseActions = "WarehouseActions";
        public const string ResourceActions = "ResourceActions";
        public const string Exit = "Exit";

        public static Dictionary<int, string> NumericMenuOptions { get; } = new()
        {
            { 1, FieldActions },
            { 2, WorkerActions },
            { 3, MachineActions },
            { 4, InventoryItemActions },
            { 5, WorkerTaskActions },
            { 6, WarehouseActions},
            { 7, ResourceActions},
            { 0, Exit }
        };
    }

    public static class FieldOptions
    {
        public const string DisplayField = "DisplayField";
        public const string DisplayAllFields = "DisplayAllFields";
        public const string EditField = "EditField";
        public const string DeleteField = "DeleteField";
        public const string AddField = "AddField";
        public const string Back = "Back";
        
        public static Dictionary<int, string> NumericMenuOptions { get; } = new()
        {
            { 1, DisplayField },
            { 2, DisplayAllFields },
            { 3, EditField },
            { 4, DeleteField },
            { 5, AddField },
            { 0, Back }
        };
    }

    public static class WorkerOptions
    {
        public const string DisplayWorker = "DisplayWorker";
        public const string DisplayAllWorkers = "DisplayAllWorkers";
        public const string EditWorker = "EditWorker";
        public const string DeleteWorker = "DeleteWorker";
        public const string AddWorker = "AddWorker";
        public const string Back = "Back";

        public static Dictionary<int, string> NumericMenuOptions { get; } = new()
        {
            { 1, DisplayWorker },
            { 2, DisplayAllWorkers },
            { 3, EditWorker },
            { 4, DeleteWorker },
            { 5, AddWorker },
            { 0, Back }
        };
    }

    public static class MachineOptions
    {
        public const string DisplayMachine = "DisplayMachine";
        public const string DisplayAllMachines = "DisplayAllMachines";
        public const string EditMachine = "EditMachine";
        public const string DeleteMachine = "DeleteMachine";
        public const string AddMachine = "AddMachine";
        public const string Back = "Back";

        public static Dictionary<int, string> NumericMenuOptions { get; } = new()
        {
            { 1, DisplayMachine },
            { 2, DisplayAllMachines },
            { 3, EditMachine },
            { 4, DeleteMachine },
            { 5, AddMachine },
            { 0, Back }
        };
    }

    public static class InventoryItemOptions
    {
        public const string DisplayInventoryItem = "DisplayInventoryItem";
        public const string DisplayAllInventoryItems = "DisplayAllInventoryItems";
        public const string EditInventoryItem = "EditInventoryItem";
        public const string DeleteInventoryItem = "DeleteInventoryItem";
        public const string AddInventoryItem = "AddInventoryItem";
        public const string Back = "Back";

        public static Dictionary<int, string> NumericMenuOptions { get; } = new()
        {
            { 1, DisplayInventoryItem },
            { 2, DisplayAllInventoryItems },
            { 3, EditInventoryItem },
            { 4, DeleteInventoryItem },
            { 5, AddInventoryItem },
            { 0, Back }
        };
    }

    public static class WorkerTaskOptions
    {
        public const string DisplayWorkerTask = "DisplayWorkerTask";
        public const string DisplayAllWorkerTasks = "DisplayAllWorkerTasks";
        public const string EditWorkerTask = "EditWorkerTask";
        public const string DeleteWorkerTask = "DeleteWorkerTask";
        public const string AddWorkerTask = "AddWorkerTask";
        public const string Back = "Back";

        public static Dictionary<int, string> NumericMenuOptions { get; } = new()
        {
            { 1, DisplayWorkerTask },
            { 2, DisplayAllWorkerTasks },
            { 3, EditWorkerTask },
            { 4, DeleteWorkerTask },
            { 5, AddWorkerTask },
            { 0, Back }
        };
    }
    
    public static class WarehouseOptions
    {
        public const string DisplayWarehouse = "DisplayWarehouse";
        public const string DisplayAllWarehouses = "DisplayAllWarehouses";
        public const string EditWarehouse = "EditWarehouse";
        public const string DeleteWarehouse = "DeleteWarehouse";
        public const string AddWarehouse = "AddWarehouse";
        public const string Back = "Back";

        public static Dictionary<int, string> NumericMenuOptions { get; } = new()
        {
            { 1, DisplayWarehouse },
            { 2, DisplayAllWarehouses },
            { 3, EditWarehouse },
            { 4, DeleteWarehouse },
            { 5, AddWarehouse },
            { 0, Back }
        };
    }
    
    public static class ResourceOptions
    {
        public const string DisplayResource = "DisplayResource";
        public const string DisplayAllResources = "DisplayAllResources";
        public const string EditResource = "EditResource";
        public const string DeleteResource = "DeleteResource";
        public const string AddResource = "AddResource";
        public const string Back = "Back";

        public static Dictionary<int, string> NumericMenuOptions { get; } = new()
        {
            { 1, DisplayResource },
            { 2, DisplayAllResources },
            { 3, EditResource },
            { 4, DeleteResource },
            { 5, AddResource },
            { 0, Back }
        };
    }
}

public static class AGMenuOptionHelper
{
    public static string? GetOptionKey(Dictionary<int, string> options, int userInput)
    {
        return options.TryGetValue(userInput, out var optionKey) ? optionKey : null;
    }
    
    public static string FormatOptionName(string option)
    {
        if (string.IsNullOrWhiteSpace(option))
            return string.Empty;

        return string.Concat(option.Select((ch, index) =>
            index > 0 && char.IsUpper(ch) ? $" {ch}" : ch.ToString()));
    }
}