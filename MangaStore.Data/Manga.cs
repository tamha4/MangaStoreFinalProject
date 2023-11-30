using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MangaStore.Data
{
    public class Manga
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Author { get; set; }
        
        [Required, MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int ItemsInStock { get; set; }
        
        [Required]
        public int GenreTypeId { get; set; }

        [ForeignKey("GenreTypeId")]
        public virtual GenreType GenreType { get; set; }

        [Required]
        public int ImageId {get; set;}
        
        [ForeignKey("ImageId")]
        public virtual Image Image {get; set;}

        [NotMapped]
        public byte[] ImageFile {
            get { return Image?.ImageData; }
            set {}
        }

        [NotMapped]
        public string GenreTypeName {
            get {return GenreType?.GenreName; }
            set {}
        } 

        public virtual List<Store> Stores { get; set; } = new List<Store>();

    }
}
