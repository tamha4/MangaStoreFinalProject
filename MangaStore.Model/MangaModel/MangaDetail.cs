using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MangaStore.Model.MangaModel
{
    public class MangaDetail
    {
        [Required]
        public int Id {get; set;}
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Author { get; set; }

        [Required, MaxLength(1000)]
        public string Description { get; set; }

        public double Price { get; set; }
        public int ItemsInStock { get; set; }
        
        [Required]
        public int GenreTypeId {get; set;}

        [Required]
        public string GenreTypeName {get; set;}

        [Required]
        public int ImageId { get; set; }

        [Required]
        public byte[] ImageFile {get; set;}
    }
}