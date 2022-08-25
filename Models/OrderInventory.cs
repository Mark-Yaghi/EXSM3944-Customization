namespace MVC_Demo.Models
{
    public class OrderInventory
    {
        public int Id { get; set; }
        public int Orderid { get; set; }
        public int Inventoryid { get; set; }
        public int Quantity { get; set; }

        public virtual InventoryProduct Product { get; set; } = null!;
        public virtual OrderInvoice Order { get; set; } = null!;
    }
}
