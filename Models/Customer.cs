using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Demo.Models
{
    public class Customer
    {
        public Customer()
        {
            OrderInvoices = new HashSet<OrderInvoice>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;


        [NotMapped]
        public string FullName => Firstname + " " + Lastname; 



        public virtual ICollection<OrderInvoice> OrderInvoices { get; set; }
    }
}
