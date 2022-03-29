using System;

namespace Api.HttpRequests
{
    public class NewExpenseRequest
    {
        public string Description { get; set; }
        public DateTime PayDate { get; set; }
        public decimal Price { get; set; }
        public string SellerName { get; set; }
        public int UserCategoryId { get; set; }
    }
}
