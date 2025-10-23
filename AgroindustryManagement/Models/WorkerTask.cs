namespace AgroindustryManagement.Models;

public class WorkerTask
{
    public WorkerTask(string description)
    {
        Description = description;
    }

    public int Id { get; set; }
    public string Description { get; set; }
    public int WorkerId { get; set; }
    public int FieldId { get; set; }
    public TaskType TaskType { get; set; }
    public double Progress { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EstimatesEndDate { get; set; }
    
}

