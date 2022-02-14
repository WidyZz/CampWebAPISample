using System.ComponentModel.DataAnnotations;


namespace CampWebAPISample.Models
{
    public class CampModel
    {

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Moniker { get; set; }

        public DateTime EventDate { get; set; }

        //přidat prefix sources
        public string LocationAddress { get; set; }
        public string LocationCity { get; set; }
        public string LocationCountry { get; set; }
        public string LocationPostalCode { get; set; }

        public ICollection<TalkModel> Talks { get; set; }
    }
}
