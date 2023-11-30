using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MangaStore.Model.ImageModel
{
    public class ImageDetail
    {
        [Required]
        public int Id {get; set;}

        [Required]
        public byte[] ImageData {get; set;}
    }
}