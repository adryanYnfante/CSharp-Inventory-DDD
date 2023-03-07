using InventoryApp.Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace InventoryApp.Domain.Entities
{
    public class InvoiceDetail : Entity<int>
    {
        public int ProductId { get; set; }
        public int InvoiceId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }

        [JsonIgnore]
        public virtual Invoice? Invoice { get; set; }

        public virtual Product? Product { get; set; }

        public void CalculateTax(decimal tax) => Tax = Subtotal * tax;

        public void CalculateSubtotal() => Subtotal = UnitPrice * Quantity;

        public void CalculateTotal() => Total = Subtotal + Tax;
    }
}