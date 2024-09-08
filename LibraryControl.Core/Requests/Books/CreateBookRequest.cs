using LibraryControl.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryControl.Core.Requests.Books
{
    public class CreateBookRequest : Request
    {
        [Required(ErrorMessage = "Título Inválido")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Gênero Inválido")]
        public EGenre Genre { get; set; } = EGenre.Ficcao;

        [Required(ErrorMessage = "Categoria Inválida")]
        public long CategoryId { get; set; }

        public string? Autor { get; set; }
        public double? Nota { get; set; }

    }
}
