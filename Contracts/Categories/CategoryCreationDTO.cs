using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Categories
{
    public class CategoryCreationDTO : ICreationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
