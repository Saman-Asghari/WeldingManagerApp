using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeldingManagerApp.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } = null;
        public int Budget { get; set; }
        public Order[] Orders { get; set; } = null;


    }
}
