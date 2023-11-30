using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MangaStore.Model.MangaModel
{
    public class MangaEdit
{
    [Required(ErrorMessage = "Please enter the ID.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter the manga name.")]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Please enter the author.")]
    [MaxLength(100)]
    public string Author { get; set; }

    [Required(ErrorMessage = "Please enter the description.")]
    [MaxLength(1000)]
    public string Description { get; set; }

    [Required(ErrorMessage = "Please enter the price.")]
    public double Price { get; set; }

    [Required(ErrorMessage = "Please enter the items in stock.")]
    public int ItemsInStock { get; set; }

    [Required(ErrorMessage = "Please select a genre.")]
    public int GenreTypeId { get; set; }

    [Required(ErrorMessage = "Please enter the image ID.")]
    public int ImageId { get; set; }

    // [Required(ErrorMessage = "Please enter the GenreTypeName.")]
    // public string GenreTypeName { get; set; }

    // [Required(ErrorMessage = "Please select an image file.")]
    // public byte[] ImageFile { get; set; }
  }
}
