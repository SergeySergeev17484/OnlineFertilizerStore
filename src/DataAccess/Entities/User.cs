using DataAccess.Entities.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public UserRole Role { get; set; }

        // Навигационные свойства
        public ICollection<Cart> CartItems { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
