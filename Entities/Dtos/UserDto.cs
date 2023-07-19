using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public record UserDto
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage ="User Name is Required.")]
        public String? UserName { get; init; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "E-mail is Required.")]
        public String? Email { get; init; }

        [DataType(DataType.PhoneNumber)]
        public String? PhoneNumber { get; init; }
        public HashSet<String> Roles { get; set; } = new HashSet<string>();//roller tekrar etmeyecek
    }
}
