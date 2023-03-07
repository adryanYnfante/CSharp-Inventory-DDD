using InventoryApp.Domain.Entities;

namespace InventoryApp.Test.Fixture
{
    public static class InvoiceFixture
    {
        public static List<Invoice> GetInvoicesTest()
        {
            return new List<Invoice>()
            {
                new Invoice {
                    Id = 1,
                    Date = DateTime.Now,
                    Cancelled = false,
                    IdClient = "1117963214",
                    IdType = "CC",
                    Subtotal = 100,
                    Tax = 19,
                    Total = 119,
                    InvoiceDetails = GenerateInvoiceDetail(),
                },
                new Invoice {
                    Id = 2,
                    Date = DateTime.Now,
                    Cancelled = false,
                    IdClient = "1117963214",
                    IdType = "CC",
                    Subtotal = 100,
                    Tax = 19,
                    Total = 119,
                    InvoiceDetails = GenerateInvoiceDetail()
                }
            };
        }

        public static Invoice GetOneInvoice()
        {
            return new Invoice
            {
                Id = 1,
                Date = DateTime.Now,
                Cancelled = false,
                IdClient = "1117963214",
                IdType = "CC",
                Subtotal = 100,
                Tax = 19,
                Total = 119,
                InvoiceDetails = GenerateInvoiceDetail(),
            };
        }

        public static Invoice DataToCreateInvoice()
        {
            return new Invoice
            {
                Id = 1,
                IdClient = "1117963214",
                IdType = "CC",
                InvoiceDetails = GenerateInvoiceDetailToCreateInvoice(),
            };
        }

        private static List<InvoiceDetail> GenerateInvoiceDetail()
        {
            return new List<InvoiceDetail>()
            {
                new InvoiceDetail {
                    InvoiceId = 1,
                    ProductId = 1,
                    Quantity = 2,
                    Subtotal = 100,
                    Tax = 19,
                    Total= 200,
                    UnitPrice = 50
                },
                new InvoiceDetail {
                    InvoiceId = 1,
                    ProductId = 2,
                    Quantity = 2,
                    Subtotal = 100,
                    Tax = 19,
                    Total= 200,
                    UnitPrice = 50
                },
            };
        }

        private static List<InvoiceDetail> GenerateInvoiceDetailToCreateInvoice()
        {
            return new List<InvoiceDetail>()
            {
                new InvoiceDetail {
                    ProductId = 1,
                    Quantity = 2,
                },
                new InvoiceDetail {
                    ProductId = 1,
                    Quantity = 5,
                }
            };
        }
    }
}