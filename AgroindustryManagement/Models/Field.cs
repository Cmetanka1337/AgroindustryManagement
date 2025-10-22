namespace AgroindustryManagement.Models;

public class Field
{
    public Field(List<Worker> workers, List<Machine> machines, List<WorkerTask> tasks)
    {
        Workers = workers;
        Machines = machines;
        Tasks = tasks;
    }

    public int Id { get; set; }
    public double Area { get; set; }
    public CultureType Culture { get; set; }
    public FieldStatus Status { get; set; }
    public List<Worker> Workers { get; set; }
    public List<Machine> Machines { get; set; }
    public List<WorkerTask> Tasks { get; set; }
    public DateTime CreatedAt { get; set; }
    
}

public enum CultureType
{
    Wheat,
    Corn,
    Soybean,
    Rice,
    Cotton
}

public enum  FieldStatus 
{
    Planted,
    Harvested,
    Fallow
}