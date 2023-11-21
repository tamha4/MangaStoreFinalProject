using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MangaStore.Model.GenreTypeModel
{
    public class GenreTypeDetail
    {
        [Required]
        public int Id {get; set;}
        
        [Required]
        public string GenreName {get; set;}  
    }
}