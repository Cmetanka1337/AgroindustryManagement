namespace AgroindustryManagement.Models;

public class Machine
{
    public int Id { get; set; }
    public MachineType Type { get; set; }
    public bool IsAvailable { get; set; }
    public int? AssignedToField { get; set; }
}

