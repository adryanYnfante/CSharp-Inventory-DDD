using InventoryApp.Domain.Entities;

namespace InventoryApp.Test.Fixture
{
    public static class ProductsFixture
    {
        public static List<Product> GetProductsTest()
        {
            return new List<Product>()
            {
                new Product {
                    Id = 1,
                    Name= "Test",
                    Enabled= true,
                    InInventory = 20,
                    MinUnits = 1,
                    MaxUnits = 100,
                    Price = 200.5M
                },
                new Product {
                    Id = 2,
                    Name= "Test2",
                    Enabled= true,
                    InInventory = 30,
                    MinUnits = 1,
                    MaxUnits = 100,
                    Price = 500.5M
                },
                new Product {
                    Id = 3,
                    Name= "Test3",
                    Enabled= true,
                    InInventory = 20,
                    MinUnits = 1,
                    MaxUnits = 20,
                    Price = 700.5M
                },
            };
        }

        public static Product GetOneProduct()
        {
            return new Product
            {
                Id = 1,
                Name = "Test",
                Enabled = true,
                InInventory = 20,
                MinUnits = 1,
                MaxUnits = 100,
                Price = 200.0M
            };
        }
    }
}