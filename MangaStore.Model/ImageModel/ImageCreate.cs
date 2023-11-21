using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MangaStore.Model.ImageModel
{
    public class ImageCreate
    {
        [Required]
        public int Id {get; set;}

        [Required]
        public byte[] ImageData {get; set;}
    }
}