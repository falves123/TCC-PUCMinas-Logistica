using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Delivery.Model
{
    public class PaymentCreatedEventDTO
    {
        public Guid ID { get; set; }
        public string CustomerName { get; set; }
        public Guid OrderID { get; set; }
        public double Value { get; set; }
    }
}
