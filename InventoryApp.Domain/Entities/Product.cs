using InventoryApp.Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace InventoryApp.Domain.Entities
{
    public class Product : Entity<int>
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public int InInventory { get; set; }
        public int MinUnits { get; set; }
        public int MaxUnits { get; set; }
        public decimal Price { get; set; }

        [JsonIgnore]
        public virtual List<InvoiceDetail>? InvoiceDetails { get; set; }

        public bool IsEnable() => Enabled;

        public bool InStock(int quantity) => quantity <= InInventory;

        public void DecreaseInventory(int quantity) => InInventory = InInventory - quantity;
    }
}