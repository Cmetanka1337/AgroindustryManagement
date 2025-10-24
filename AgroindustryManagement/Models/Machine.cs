namespace AgroindustryManagement.Models;

public class Machine
{
    public int Id { get; set; }
    public MachineType Type { get; set; }
    public bool IsAvailable { get; set; }
    public int? AssignedToField { get; set; }
    public virtual Field? Field { get; set; }
    public int ResourceId { get; set; }
    public virtual Resource Resource { get; set; }
}

