using System;
using System.Collections.Generic;

namespace AgroindustryManagement.Models;

public class Field
{
    public Field(List<Worker> workers, List<Machine> machines, List<WorkerTask> tasks)
    {
        Workers = workers;
        Machines = machines;
        Tasks = tasks;
    }
    public Field()
    { 
    }

    public int Id { get; set; }
    public double Area { get; set; }
    public CultureType Culture { get; set; }
    public FieldStatus Status { get; set; }
    public List<Worker> Workers { get; set; } = new List<Worker>();
    public List<Machine> Machines { get; set; } = new List<Machine>();
    public List<WorkerTask> Tasks { get; set; } = new List<WorkerTask>();
    public DateTime CreatedAt { get; init; }
    
}