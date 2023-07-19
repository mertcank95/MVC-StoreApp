using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public record ProductDto
    {
        public int ProductId { get; init; }
        [Required(ErrorMessage = "Product Name is Required")]
        public string? ProductName { get; init; } = string.Empty;
        [Required(ErrorMessage = "Price is Required")]
        public decimal Price { get; init; }
        public String? Summary { get; init; } = String.Empty;
        public String? ImageUrl { get; set; } //önce dosya yolu yüklenecek sonra dosya o yüzden set
        public int? CategoryId { get; init; }
     
    }
}
