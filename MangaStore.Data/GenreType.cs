using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MangaStore.Data
{
    public class GenreType
    {
        [Key]
        public int Id {get; set;}

        [Required,MaxLength(100)]
        public string? GenreName {get; set;}  
    }
}