using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryControl.Core.Requests.Categories
{
    public class CreateCategoryRequest : Request
    {
        [Required(ErrorMessage = "Título Inválido")]
        [MaxLength(80, ErrorMessage = "O Título deve conter até 80 carateres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descrição Inválido")]
        public string Description { get; set; } = string.Empty;
    }
}
