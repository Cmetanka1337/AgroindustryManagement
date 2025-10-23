namespace AgroindustryManagement.Models;

public class Warehouse
{
    public Warehouse(List<InventoryItem> inventoryItems)
    {
        InventoryItems = inventoryItems;
    }

    public int Id { get; set; }
    public List<InventoryItem> InventoryItems { get; set; }   
}