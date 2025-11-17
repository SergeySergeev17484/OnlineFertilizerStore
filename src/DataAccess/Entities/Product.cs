using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }
        public string ImageUrl { get; set; }
        public int StockQuantity { get; set; }
        public decimal Rating { get; set; }

        // Навигационные свойства
        public Category Category { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public ICollection<Cart> CartItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
