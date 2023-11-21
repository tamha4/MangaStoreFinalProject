using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace MangaStore.Model.StoreModel
{
    public class StoreEdit
    {
        [Required]
        public int Id {get; set;}

        [Required,MaxLength(100)]      
        public string Name { get; set; }

        [Required,MaxLength(100)]
        public string Address { get; set; }

        [Required,MaxLength(100)]
        public string PhoneNumber { get; set; }

        [Required]
        public int MangaId {get; set;}

        // public List<StoreInventory> StoreInventories { get; set; }

    }
}