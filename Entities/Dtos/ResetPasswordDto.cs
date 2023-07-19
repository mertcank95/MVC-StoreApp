using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public record ResetPasswordDto
    {
        public String? UserName { get; init; }
        [DataType(DataType.Password)]//görünümlerde direkt input tipini vermiş oluyoruz
        [Required(ErrorMessage = "Password is required.")]
        public String? Password { get; init; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Comfirm Password is required.")]
        [Compare("Password",ErrorMessage ="Password and comfirm password must")]
        public String? ComfirmPassword { get; init; }
    }
}
