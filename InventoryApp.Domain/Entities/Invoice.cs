using InventoryApp.Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace InventoryApp.Domain.Entities
{
    public class Invoice : Entity<int>
    {
        public DateTime Date { get; set; }
        public string IdType { get; set; }
        public string IdClient { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public bool Cancelled { get; set; }

        public virtual List<InvoiceDetail> InvoiceDetails { get; set; }
    }
}