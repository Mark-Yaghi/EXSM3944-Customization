

namespace MVC_Demo.Models
{
    public class InventoryProduct
    {
        public InventoryProduct()
        {
            OrderInventories = new HashSet<OrderInventory>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Qoh { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string ProductName => Name;          //Create the NotMapped string 'ProductName' here to be used in the InvoiceProductController to be rendered to screen in a select.

        public virtual ICollection<OrderInventory> OrderInventories { get; set; }
    }
}
