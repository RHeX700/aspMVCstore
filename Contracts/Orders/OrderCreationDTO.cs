using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Orders
{
    public class OrderCreationDTO : ICreationDto
    {
        public string OwnerID { get; set; }
        public string Address { get; set; }
    }
}
