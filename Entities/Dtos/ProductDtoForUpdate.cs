using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public record ProductDtoForUpdate : ProductDto
    {
        public bool Showchase { get; set; }
    }
}
