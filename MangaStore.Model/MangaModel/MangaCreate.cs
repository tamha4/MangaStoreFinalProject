using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using MangaStore.Model.MangaModel;
using Microsoft.VisualBasic; // Make sure the correct namespace is used.

namespace MangaStore.Model.MangaModel
{
    public class MangaCreate
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Author { get; set; }

        [Required, MaxLength(1000)]
        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid price.")]
        public double Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid number of items in stock.")]
        public int ItemsInStock { get; set; }

        [Required(ErrorMessage = "GenreTypeId is required")]
        public int GenreTypeId { get; set; }
        
        [Required]
        public int GenreTypeName { get; set; }

        [Required]
        public int ImageId { get; set; }

        [Required]
        public int ImageFile {get; set;}
    }
}
