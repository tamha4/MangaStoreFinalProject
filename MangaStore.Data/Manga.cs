using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MangaStore.Data
{
    // Represents a Manga entity in the database
    public class Manga
    {
        [Key]
        public int Id { get; set; } // Primary key for the Manga entity

        [Required, MaxLength(100)]
        public string Name { get; set; } // Name of the manga, required and limited to 100 characters

        [Required, MaxLength(100)]
        public string Author { get; set; } // Author of the manga, required and limited to 100 characters

        [Required, MaxLength(1000)]
        public string Description { get; set; } // Description of the manga, required and limited to 1000 characters

        [Required]
        public double Price { get; set; } // Price of the manga, required

        [Required]
        public int ItemsInStock { get; set; } // Number of items in stock, required

        [Required]
        public int GenreTypeId { get; set; } // Foreign key to GenreType

        [ForeignKey("GenreTypeId")]
        public virtual GenreType GenreType { get; set; } // Navigation property to the associated GenreType

        [Required]
        public int? ImageId { get; set; } // Foreign key to Image

        [ForeignKey("ImageId")]
        public virtual Image Image { get; set; } // Navigation property to the associated Image

        [NotMapped]
        public byte[] ImageFile
        {
            get { return Image?.ImageData; } // Not mapped to the database, used to get/set the ImageData from the Image entity
            set { }
        }

        [NotMapped]
        public string GenreTypeName
        {
            get { return GenreType?.GenreName; } // Not mapped to the database, used to get the GenreName from the associated GenreType
            set { }
        }

        // Navigation property to the associated Store entities
        public virtual List<Store> Stores { get; set; } = new List<Store>();
    }
}
