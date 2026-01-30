
using HelloApi;
using Microsoft.EntityFrameworkCore;

namespace HelloApi
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public  string Description { get; set; } = "";
    }
}

