using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CraftCellar.Models
{
    public class Beverage
    {

        //idris zahir assigment 1 server side
        // i first named myPrimary Key
        public int BeverageId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; }

        public double AlcoholContent { get; set; }

        // this is the Foreign Key
        public int BreweryId { get; set; }

        // Navigation Property
        // so now i said each beverage belongs to 1 brewer
        public Brewery? Brewery { get; set; }
    }
}