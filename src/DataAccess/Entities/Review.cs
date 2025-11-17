using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Review : BaseEntity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        // Навигационные свойства
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
