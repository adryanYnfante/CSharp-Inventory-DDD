namespace InventoryApp.Domain.Entities
{
    public class ProductParameters
    {
        private const int MAX_PAGE_SIZE = 30;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 20;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
            }
        }
    }
}