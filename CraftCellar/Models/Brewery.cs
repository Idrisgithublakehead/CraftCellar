using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CraftCellar.Models
{
    public class Brewery
    {
        // ok so first we need to name a Primary Key
        public int BreweryId { get; set; }

        // i set this as required so user must ytypr it
        [Required]
        public string Name { get; set; }

        public int YearFounded { get; set; }

        public string Location { get; set; }

        // this is where the relationship actually is stated,
        //here we are saying beveagres is one to many meaning it can have mutliple childs
        public ICollection<Beverage>? Beverages { get; set; }
    }
}