using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MangaStore.Model.ImageModel
{
    public class ImageEdit
    {
        [Required]
        public int Id {get; set;}

        [Required]
        public byte[] ImageData {get; set;}
    }
}