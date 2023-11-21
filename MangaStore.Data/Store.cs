using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MangaStore.Data
{
    public class Store
    {
        [Key]
        public int Id {get; set;}

        [Required,MaxLength(100)]      
        public string Name { get; set; }

        [Required,MaxLength(100)]
        public string Address { get; set; }

        [Required,MaxLength(100)]
        public string PhoneNumber { get; set; }

        public int MangaId {get; set;}
        [ForeignKey("MangaId")]
        public virtual Manga Mangas {get; set;}

        [NotMapped]
        public string MangaName => Mangas?.Name;
        // public virtual List<Manga> Mangas {get; set;} = new List<Manga>();

        public List<StoreInventory> StoreInventories { get; set; }

    }
}