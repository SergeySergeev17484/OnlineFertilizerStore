using DataAccess.Entities.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public string OrderNumber { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string DeliveryAddress { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }

        // Навигационные свойства
        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
