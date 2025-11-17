using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Cart : BaseEntity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        // Навигационные свойства
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
