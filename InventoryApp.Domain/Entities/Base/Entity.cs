namespace InventoryApp.Domain.Entities.Base
{
    public class Entity<TType>
        where TType : struct
    {
        public TType Id { get; set; }
    }
}