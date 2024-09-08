using LibraryControl.Core.Enums;

namespace LibraryControl.Core.Models
{
    public class Book
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Autor { get; set; }
        public double? Nota { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.Now;
        public EGenre Genre { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public string UserId { get; set; } = string.Empty;
    }
}
