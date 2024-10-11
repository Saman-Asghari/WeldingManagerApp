using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeldingManagerApp.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Description { get; set; } = null;
        public string PieceName { get; set; }
        public int Level { get; set; }
        public int BasePrice { get; set; }
        public int lastPrice { get; set; }
        public DateTime EstimatedTime { get; set; }

        public DateTime EndTime { get; set; }

        //foreign key to the customer
        public int CustomerId { get; set; }
        // Navigation property for the customer
        public Customer Customer { get; set; }

    }
}
