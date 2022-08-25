﻿namespace MVC_Demo.Models
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

        public virtual ICollection<OrderInventory> OrderInventories { get; set; }
    }
}
