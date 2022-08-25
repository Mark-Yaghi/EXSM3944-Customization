namespace MVC_Demo.Models
{
    public class OrderInvoice
    {
        public OrderInvoice()
        {
            OrderInventories = new HashSet<OrderInventory>();
        }

        public int Id { get; set; }
        public int Customerid { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<OrderInventory> OrderInventories { get; set; }
    }
}
