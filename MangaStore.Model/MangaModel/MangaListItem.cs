using System.ComponentModel.DataAnnotations;

namespace MangaStore.Model.MangaModel
{
    public class MangaListItem
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Required(ErrorMessage = "ItemsInStock is required")]
        public int ItemsInStock { get; set; }

        [Required(ErrorMessage = "GenreTypeId is required")]
        public int GenreTypeId { get; set; }

        [Required(ErrorMessage = "GenreTypeName is required")]
        public string GenreTypeName { get; set; }

        [Required]
        public int ImageId {get; set;}

        // [Required(ErrorMessage = "ImageData is required")]
        // public byte[] ImageData { get; set; }
    }
}
