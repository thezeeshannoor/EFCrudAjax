using System.ComponentModel.DataAnnotations;

namespace Entity_Framework_with_Ajax.Models
{
    public class Employe
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Depaertment { get; set; }
        public decimal Sallary { get; set; }


    }
}
