using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Manufacturer : BaseEntity
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }

        // Навигационные свойства
        public ICollection<Product> Products { get; set; }
    }
}
