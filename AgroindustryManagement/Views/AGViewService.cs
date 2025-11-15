using AgroindustryManagement.Models;
    
    namespace AgroindustryManagement.Views;
    
    /// <summary>
    /// Service for displaying information in the console application.
    /// </summary>
    public class AGViewService
    {
        // FIELDS

        /// <summary>
        /// Displays detailed information about a specific field.
        /// </summary>
        /// <param name="field">The field to display details for.</param>
        public void DisplayFieldDetails(Field field)
        {
            Console.WriteLine("===================================");
            Console.WriteLine("Field Details");
            Console.WriteLine("===================================");
            Console.WriteLine($"ID:                {field.Id}");
            Console.WriteLine($"Area:              {field.Area} hectares");
            Console.WriteLine($"Culture:           {field.Culture}");
            Console.WriteLine($"Status:            {field.Status}");
            Console.WriteLine($"Created At:        {field.CreatedAt:yyyy-MM-dd}");
            Console.WriteLine("===================================");
            Console.WriteLine("Workers Assigned:");
            if (field.Workers.Count > 0)
            {
                foreach (var worker in field.Workers)
                {
                    Console.WriteLine($"- {worker.FirstName} (ID: {worker.Id})");
                }
            }
            else
            {
                Console.WriteLine("No workers assigned.");
            }
        
            Console.WriteLine("===================================");
            Console.WriteLine("Machines Assigned:");
            if (field.Machines.Count > 0)
            {
                foreach (var machine in field.Machines)
                {
                    Console.WriteLine($"- {machine.Type} (ID: {machine.Id})");
                }
            }
            else
            {
                Console.WriteLine("No machines assigned.");
            }
        
            Console.WriteLine("===================================");
            Console.WriteLine("Tasks:");
            if (field.Tasks.Count > 0)
            {
                foreach (var task in field.Tasks)
                {
                    Console.WriteLine($"- {task.Description} (ID: {task.Id}, Status: {task.Progress}%)");
                }
            }
            else
            {
                Console.WriteLine("No tasks assigned.");
            }
            Console.WriteLine("===================================");
        }
    
        /// <summary>
        /// Displays a list of all fields.
        /// </summary>
        /// <param name="fields">The collection of fields to display.</param>
        public void DisplayAllFields(IEnumerable<Field> fields)
        {
            Console.WriteLine("==================================================================");
            Console.WriteLine("| ID   | Area (ha) | Culture       | Status      | Created At    |");
            Console.WriteLine("==================================================================");
        
            foreach (var field in fields)
            {
                Console.WriteLine($"| {field.Id,-5} | {field.Area,-9} | {field.Culture,-12} | {field.Status,-11} | {field.CreatedAt:yyyy-MM-dd} |");
            }
        
            Console.WriteLine("==================================================================");
        }
    
        // WORKERS
    
        /// <summary>
        /// Displays detailed information about a specific worker.
        /// </summary>
        /// <param name="worker">The worker to display details for.</param>
        public void DisplayWorkerDetails(Worker worker)
        {
            Console.WriteLine("===================================");
            Console.WriteLine("Worker Details");
            Console.WriteLine("===================================");
            Console.WriteLine($"ID:                {worker.Id}");
            Console.WriteLine($"Name:              {worker.FirstName} {worker.LastName}");
            Console.WriteLine($"Age:               {worker.Age}");
            Console.WriteLine($"Hourly Rate:       {worker.HourlyRate:C}");
            Console.WriteLine($"Hours Worked:      {worker.HoursWorked}");
            Console.WriteLine($"Active:            {(worker.IsActive ? "Yes" : "No")}");
            Console.WriteLine("===================================");
            Console.WriteLine("Tasks:");
            if (worker.Tasks.Count > 0)
            {
                foreach (var task in worker.Tasks)
                {
                    Console.WriteLine($"- {task.Description} (ID: {task.Id}, Progress: {task.Progress}%)");
                }
            }
            else
            {
                Console.WriteLine("No tasks assigned.");
            }
            Console.WriteLine("===================================");
        }
    
        /// <summary>
        /// Displays a list of all workers.
        /// </summary>
        /// <param name="workers">The collection of workers to display.</param>
        public void DisplayAllWorkers(IEnumerable<Worker> workers)
        {
            Console.WriteLine("==========================================================================");
            Console.WriteLine("| ID   | Name                 | Age  | Hourly Rate | Hours Worked | Active |");
            Console.WriteLine("==========================================================================");

            foreach (var worker in workers)
            {
                Console.WriteLine($"| {worker.Id,-5} | {worker.FirstName} {worker.LastName,-15} | {worker.Age,-4} | {worker.HourlyRate,-11:C} | {worker.HoursWorked,-12} | {(worker.IsActive ? "Yes" : "No"),-6} |");
            }

            Console.WriteLine("==========================================================================");
        }
    
        // MACHINES
    
        /// <summary>
        /// Displays detailed information about a specific machine.
        /// </summary>
        /// <param name="machine">The machine to display details for.</param>
        public void DisplayMachineDetails(Machine machine)
        {
            Console.WriteLine("===================================");
            Console.WriteLine("Machine Details");
            Console.WriteLine("===================================");
            Console.WriteLine($"ID:                {machine.Id}");
            Console.WriteLine($"Type:              {machine.Type}");
            Console.WriteLine($"Available:         {(machine.IsAvailable ? "Yes" : "No")}");
            Console.WriteLine($"Assigned to Field: {(machine.AssignedToField.HasValue ? machine.AssignedToField.Value.ToString() : "None")}");
            Console.WriteLine($"Resource ID:       {machine.ResourceId}");
            Console.WriteLine("===================================");
        }
    
        /// <summary>
        /// Displays a list of all machines.
        /// </summary>
        /// <param name="machines">The collection of machines to display.</param>
        public void DisplayAllMachines(IEnumerable<Machine> machines)
        {
            Console.WriteLine("==================================================================");
            Console.WriteLine("| ID   | Type         | Available | Assigned to Field | Resource ID |");
            Console.WriteLine("==================================================================");

            foreach (var machine in machines)
            {
                Console.WriteLine($"| {machine.Id,-5} | {machine.Type,-12} | {(machine.IsAvailable ? "Yes" : "No"),-9} | {(machine.AssignedToField.HasValue ? machine.AssignedToField.Value.ToString() : "None"),-17} | {machine.ResourceId,-11} |");
            }

            Console.WriteLine("==================================================================");
        }
    
        // INVENTORY ITEMS
    
        /// <summary>
        /// Displays detailed information about a specific inventory item.
        /// </summary>
        /// <param name="item">The inventory item to display details for.</param>
        public void DisplayInventoryItemDetails(InventoryItem item)
        {
            Console.WriteLine("===================================");
            Console.WriteLine("Inventory Item Details");
            Console.WriteLine("===================================");
            Console.WriteLine($"ID:                {item.Id}");
            Console.WriteLine($"Name:              {item.Name}");
            Console.WriteLine($"Quantity:          {item.Quantity} {item.Unit}");
            Console.WriteLine($"Warehouse ID:      {item.WarehouseId}");
            Console.WriteLine("===================================");
        }
    
        /// <summary>
        /// Displays a list of all inventory items.
        /// </summary>
        /// <param name="items">The collection of inventory items to display.</param>
        public void DisplayAllInventoryItems(IEnumerable<InventoryItem> items)
        {
            Console.WriteLine("===============================================================");
            Console.WriteLine("| ID   | Name                 | Quantity       | Warehouse ID |");
            Console.WriteLine("===============================================================");

            foreach (var item in items)
            {
                Console.WriteLine($"| {item.Id,-5} | {item.Name,-20} | {item.Quantity,-14} {item.Unit,-5} | {item.WarehouseId,-12} |");
            }

            Console.WriteLine("===============================================================");
        }
    
        // WORKER TASKS
    
        /// <summary>
        /// Displays detailed information about a specific worker task.
        /// </summary>
        /// <param name="task">The worker task to display details for.</param>
        public void DisplayWorkerTaskDetails(WorkerTask task)
        {
            Console.WriteLine("===================================");
            Console.WriteLine("Worker Task Details");
            Console.WriteLine("===================================");
            Console.WriteLine($"ID:                {task.Id}");
            Console.WriteLine($"Description:       {task.Description}");
            Console.WriteLine($"Worker ID:         {task.Worker.Id}");
            Console.WriteLine($"Worker Name:       {task.Worker.FirstName}");
            Console.WriteLine($"Field ID:          {task.Field.Id}");
            Console.WriteLine($"Task Type:         {task.TaskType}");
            Console.WriteLine($"Progress:          {task.Progress}%");
            Console.WriteLine($"Start Date:        {task.StartDate:yyyy-MM-dd}");
            Console.WriteLine($"Estimated End Date:{task.EstimatesEndDate:yyyy-MM-dd}");
            Console.WriteLine("===================================");
        }
    
        /// <summary>
        /// Displays a list of all worker tasks.
        /// </summary>
        /// <param name="tasks">The collection of worker tasks to display.</param>
        public void DisplayAllWorkerTasks(IEnumerable<WorkerTask> tasks)
        {
            Console.WriteLine("==================================================================================================");
            Console.WriteLine("| ID   | Description          | Worker ID | Field ID | Task Type      | Progress | Start Date | Estimated End Date |");
            Console.WriteLine("==================================================================================================");

            foreach (var task in tasks)
            {
                Console.WriteLine($"| {task.Id,-5} | {task.Description,-20} | {task.Worker.Id,-9} | {task.Field.Id,-8} | {task.TaskType,-13} | {task.Progress,-8}% | {task.StartDate:yyyy-MM-dd} | {task.EstimatesEndDate:yyyy-MM-dd} |");
            }

            Console.WriteLine("==================================================================================================");
        }
    
        // REPORTS
    
        /// <summary>
        /// Displays a list of inventory items that are critically low in stock.
        /// </summary>
        /// <param name="criticalItems">The collection of critical inventory items to display.</param>
        public void DisplayCriticalInventoryItems(IEnumerable<InventoryItem> criticalItems) { }
    
        /// <summary>
        /// Displays a list of machines that are currently available for use.
        /// </summary>
        /// <param name="availableMachines">The collection of available machines to display.</param>
        public void DisplayAvailableMachines(IEnumerable<Machine> availableMachines) { }
    
        /// <summary>
        /// Displays tasks assigned to a specific worker.
        /// </summary>
        /// <param name="tasks">The collection of tasks to display.</param>
        /// <param name="workerId">The unique identifier of the worker.</param>
        public void DisplayWorkerTasksByWorkerId(IEnumerable<WorkerTask> tasks, int workerId) { }
    
        /// <summary>
        /// Displays a summary report.
        /// </summary>
        /// <param name="report">The summary report to display.</param>
        public void DisplaySummaryReport(string report) { }

        public void DisplayIds(string[] ids)
        {
            foreach (var id in ids)
            {
                Console.Write($"{id}; ");
            }
            Console.WriteLine();
        }
        
        // USER INPUT

        public int GetIntegerUserInputWithMessage(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                var input = Console.ReadLine();

                if (int.TryParse(input, out var parsedInput))
                {
                    return parsedInput;
                }
        
                Console.WriteLine("Invalid input. Please enter a valid decimal number.");
            }
        }
    }