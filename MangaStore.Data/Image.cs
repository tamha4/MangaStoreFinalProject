using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MangaStore.Data
{
    public class Image
    {
        [Key]
        public int Id {get; set;}

        [Required]
        public byte[] ImageData {get; set;}
    }
}